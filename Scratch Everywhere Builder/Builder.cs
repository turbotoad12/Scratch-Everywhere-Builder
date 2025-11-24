using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Scratch_Everywhere_Builder.Version;

namespace Scratch_Everywhere_Builder
{
    internal class Builder
    {
        private Sebx.SebxProject project;
        private DirectoryInfo projectPath;
        internal FileInfo? dockerfile;

        internal Builder(Sebx.SebxProject project, DirectoryInfo projectPath)
        {
            this.project = project ?? throw new ArgumentNullException(nameof(project));
            this.projectPath = projectPath ?? throw new ArgumentNullException(nameof(projectPath));
        }

        internal void PrepareFS()
        {
            // Define files and paths
            DirectoryInfo FinalIconPath = new(Path.Combine(Utils.TempDirectory.FullName, "gfx"));
            DirectoryInfo FinalBannerPath = new(Path.Combine(Utils.TempDirectory.FullName, "gfx", "3ds"));
            DirectoryInfo FinalSb3Path = new(Path.Combine(Utils.TempDirectory.FullName, "romfs"));
            FinalIconPath.Create();
            FinalBannerPath.Create();
            FinalSb3Path.Create();

            // Copy base files from the versions directory (use major.minor folder name)
            string versionFolderName = $"{project.TargetVersion.Major}.{project.TargetVersion.Minor}";
            string sourceVersionPath = Path.Combine(VersionsDirectory.FullName, versionFolderName);
            if (!Directory.Exists(sourceVersionPath))
            {
                throw new DirectoryNotFoundException($"Version folder not found: {sourceVersionPath}");
            }
            Utils.CopyDirectoryRecursive(sourceVersionPath, Utils.TempDirectory.FullName, true);

            // Copy assets (icon/banner) if present
            CopyAssets(FinalIconPath, project.IconFile, FinalBannerPath, project.BannerFile);

            // Copy sb3 folder
            if (project.Sb3Folder == null || !project.Sb3Folder.Exists)
            {
                throw new DirectoryNotFoundException("Sb3 folder not found in project.");
            }
            Utils.CopyDirectoryRecursive(project.Sb3Folder.FullName, FinalSb3Path.FullName, true);

            // find dockerfile
            FileInfo Dockerfile = new(Path.Combine(Utils.TempDirectory.FullName, "docker", "Dockerfile.3ds"));
            if (!Dockerfile.Exists)
            {
                throw new FileNotFoundException("Dockerfile not found in temporary directory.", Dockerfile.FullName);
            }

            // store the found dockerfile for later use
            dockerfile = Dockerfile;
        }

        internal static void CopyAssets(DirectoryInfo finaliconpath, FileInfo? iconpath, DirectoryInfo finalbannerpath, FileInfo? bannerpath)
        {
            // Copy icon if available
            if (iconpath != null && File.Exists(iconpath.FullName))
            {
                File.Copy(iconpath.FullName, Path.Combine(finaliconpath.FullName, "icon.png"), true);
            }

            // Copy banner if available
            if (bannerpath != null && File.Exists(bannerpath.FullName))
            {
                File.Copy(bannerpath.FullName, Path.Combine(finalbannerpath.FullName, "banner.png"), true);
            }
        }

        internal string GenerateBuildCommand(FileInfo dockerfile)
        {
            // Quote all paths to handle spaces and special folders
            string outputDir = Path.Combine(projectPath.FullName, "build");
            return $"docker build -f \"{dockerfile.FullName}\" --target exporter -o \"{outputDir}\" \"{Utils.TempDirectory.FullName}\"";
        }

        async internal Task Build(ToolStripProgressBar progressbar)
        {
            if (progressbar == null) throw new ArgumentNullException(nameof(progressbar));
            progressbar.Value = 10;
            // Prepare FS
            PrepareFS();
            progressbar.Value = 30;
            // Ensure dockerfile was found
            if (dockerfile == null)
                throw new InvalidOperationException("Dockerfile was not located in the temporary directory.");

            // Generate build command
            string command = GenerateBuildCommand(dockerfile);
            progressbar.Value = 45;
            // Check that the docker daemon is running
            if (!Utils.IsDockerRunning())
            {
                throw new Exception("Docker daemon is not running. Please start Docker and try again.");
            }
            // Execute build command
            var processInfo = new System.Diagnostics.ProcessStartInfo("cmd.exe", $"/c {command}")
            {
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                WorkingDirectory = Utils.TempDirectory.FullName // ensure docker resolves paths relative to temp dir if needed
            };
            using (var process = System.Diagnostics.Process.Start(processInfo))
            {
                progressbar.Value = 60;
                if (process != null)
                {
                    var stdout = new StringBuilder();
                    var stderr = new StringBuilder();

                    process.OutputDataReceived += (sender, args) => { if (args.Data != null) { stdout.AppendLine(args.Data); } };
                    process.ErrorDataReceived += (sender, args) => { if (args.Data != null) { stderr.AppendLine(args.Data); } };
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();
                    await Task.Run(() => process.WaitForExit());

                    string outStr = stdout.ToString();
                    string errStr = stderr.ToString();
                    progressbar.Value = 80;
                    if (process.ExitCode != 0)
                    {
                        string msg = $"Build process exited with code {process.ExitCode}\n\nOutput:\n{outStr}\n\nError:\n{errStr}";
                        // TODO: relay message to caller/UI instead of throwing if desired
                        throw new Exception(msg);
                    }
                    else
                    {
                        // success
                        progressbar.Value = 90;
                    }
                }
                else
                {
                    throw new Exception("Failed to start build process.");
                }
            }
            // Clean up temp directory
            try { Utils.TempDirectory.Delete(true); } catch { /* ignore */ }
            progressbar.Value = 100;
        }
    }
}
