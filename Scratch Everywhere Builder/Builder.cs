using System.Text;
using static Scratch_Everywhere_Builder.Version;
namespace Scratch_Everywhere_Builder
{
    internal class Builder(Sebx.SebxProject project)
    {
        internal FileInfo? dockerfile;
        internal void PrepareFS()
        {
            // Define files and paths
            DirectoryInfo FinalIconPath = new(Path.Combine(Utils.TempDirectory.FullName, "gfx"));
            DirectoryInfo FinalBannerPath = new(Path.Combine(Utils.TempDirectory.FullName, "gfx/3ds"));
            DirectoryInfo FinalSb3Path = new(Path.Combine(Utils.TempDirectory.FullName, "romfs"));
            FinalIconPath.Create();
            FinalBannerPath.Create();
            FinalSb3Path.Create();
            // Copy base files
            Utils.CopyDirectoryRecursive(Path.Combine(VersionsDirectory.FullName, $"{project.Tar.Version}"), Utils.TempDirectory.FullName, true);
            // Copy assets
            CopyAssets(FinalIconPath, iconpath, FinalBannerPath, bannerpath);
            // Copy sb3
            File.Copy(sb3path.FullName, Path.Combine(FinalSb3Path.FullName, "project.sb3"), true);
            // find dockerfile
            FileInfo Dockerfile = new(Path.Combine(Utils.TempDirectory.FullName, "docker", "Dockerfile.3ds"));
            if (!Dockerfile.Exists)
            {
                throw new FileNotFoundException("Dockerfile not found in temporary directory.", Dockerfile.FullName);
            }

            // store the found dockerfile for later use
            dockerfile = Dockerfile;
        }
        internal static void CopyAssets(DirectoryInfo finaliconpath, FileInfo iconpath, DirectoryInfo finalbannerpath, FileInfo bannerpath)
        {
            // Copy icon
            File.Copy(iconpath.FullName, Path.Combine(finaliconpath.FullName, "icon.png"), true);
            // Copy banner
            File.Copy(bannerpath.FullName, Path.Combine(finalbannerpath.FullName, "banner.png"), true);
        }
        internal string GenerateBuildCommand(FileInfo dockerfile)
        {
            // Quote all paths to handle spaces and special folders
            return $"docker build -f \"{dockerfile.FullName}\" --target exporter -o \"{outputdirectory.FullName}\" \"{Utils.TempDirectory.FullName}\"";
        }
        internal bool Build()
        {
            // Prepare FS
            PrepareFS();
            // Ensure dockerfile was found
            if (dockerfile == null)
                throw new InvalidOperationException("Dockerfile was not located in the temporary directory.");

            // Generate build command
            string command = GenerateBuildCommand(dockerfile);
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
                if (process != null)
                {
                    var stdout = new StringBuilder();
                    var stderr = new StringBuilder();

                    process.OutputDataReceived += (sender, args) => { if (args.Data != null) { stdout.AppendLine(args.Data); } };
                    process.ErrorDataReceived += (sender, args) => { if (args.Data != null) { stderr.AppendLine(args.Data); } };
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();
                    process.WaitForExit();

                    string outStr = stdout.ToString();
                    string errStr = stderr.ToString();

                    if (process.ExitCode != 0)
                    {
                        string msg = $"Build process exited with code {process.ExitCode}\n\nOutput:\n{outStr}\n\nError:\n{errStr}";
                        // TODO: relay message to caller/UI instead of throwing if desired
                        throw new Exception(msg);
                    }
                    else
                    {
                        // success
                    }
                }
                else
                {
                    throw new Exception("Failed to start build process.");
                }
            }
            // Clean up temp directory
            Utils.TempDirectory.Delete(true);
            return true;
        }
    }
}
