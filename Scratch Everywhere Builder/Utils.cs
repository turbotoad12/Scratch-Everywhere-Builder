using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Scratch_Everywhere_Builder
{
    internal class Utils
    {
        /// <summary>
        /// Recursively copies all files and subdirectories from <paramref name="sourceDir"/> to <paramref name="destinationDir"/>.
        /// </summary>
        /// <param name="sourceDir">The path of the directory to copy from. Must exist.</param>
        /// <param name="destinationDir">The path of the directory to copy to. Will be created if it does not exist.</param>
        /// <param name="overwrite">If <c>true</c>, existing files in the destination will be overwritten; otherwise an <see cref="IOException"/> may be thrown when a file already exists.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="sourceDir"/> or <paramref name="destinationDir"/> is null, empty, or whitespace.</exception>
        /// <exception cref="DirectoryNotFoundException">Thrown when the source directory does not exist.</exception>
        /// <exception cref="IOException">Thrown when an I/O error occurs during copying.</exception>
        internal static void CopyDirectoryRecursive(string sourceDir, string destinationDir, bool overwrite = false)
        {
            if (string.IsNullOrWhiteSpace(sourceDir))
                throw new ArgumentException("Source directory must be a non-empty path.", nameof(sourceDir));

            if (string.IsNullOrWhiteSpace(destinationDir))
                throw new ArgumentException("Destination directory must be a non-empty path.", nameof(destinationDir));

            var srcFull = Path.GetFullPath(sourceDir);
            var dstFull = Path.GetFullPath(destinationDir);

            if (!Directory.Exists(srcFull))
                throw new DirectoryNotFoundException($"Source directory not found: {srcFull}");

            Directory.CreateDirectory(dstFull);

            foreach (var filePath in Directory.EnumerateFiles(srcFull, "*", SearchOption.TopDirectoryOnly))
            {
                var destFilePath = Path.Combine(dstFull, Path.GetFileName(filePath));
                File.Copy(filePath, destFilePath, overwrite);
            }

            foreach (var dirPath in Directory.EnumerateDirectories(srcFull, "*", SearchOption.TopDirectoryOnly))
            {
                var dirName = Path.GetFileName(dirPath.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar));
                var destSubDir = Path.Combine(dstFull, dirName ?? Path.GetFileName(dirPath));
                CopyDirectoryRecursive(dirPath, destSubDir, overwrite);
            }
        }
        static internal readonly DirectoryInfo TempDirectory =
            Directory.CreateDirectory(
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                             "ScratchEverywhereBuilder", "temp"));
        internal static string ProgramDir => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty;
        internal static string DefaultBanner => Path.Combine(ProgramDir, "Resources/SE! Builder banner.png");
        internal static string DefaultIcon => Path.Combine(ProgramDir, "Resources/SE! Builder icon.png");
        internal static string DefaultProject => Path.Combine(ProgramDir, "Resources/project.sb3");
        internal static bool IsDockerRunning()
        {
            try
            {
                var processInfo = new System.Diagnostics.ProcessStartInfo("cmd.exe", "/c docker info")
                {
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
                using (var process = System.Diagnostics.Process.Start(processInfo))
                {
                    if (process != null)
                    {
                        process.WaitForExit();
                        return process.ExitCode == 0;
                    }
                }
            }
            catch
            {
                // Ignore exceptions and assume Docker is not running
            }
            return false;
        }
    }
}
