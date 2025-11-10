namespace Scratch_Everywhere_Builder
{
    internal class Version
    {
        internal struct VersionInfo
        {
            internal int Major;
            internal int Minor;
            internal VersionInfo(int major = 0, int minor = 0)
            {
                Major = major;
                Minor = minor;
                Version = float.Parse($"{Major}.{Minor}");
            }
            internal float Version;
        }
        static internal readonly DirectoryInfo VersionsDirectory =
            Directory.CreateDirectory(
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                             "Scratch Everywhere Builder", "versions"));
        
        static internal string[] GetAvailableVersions(bool returnnames = false)
        {

            string[] versionFolders = System.IO.Directory.GetDirectories(VersionsDirectory.FullName);
            // Extract just the folder names (versions)
            if (returnnames)
            {
                string[] versions = versionFolders.Select(folder => System.IO.Path.GetFileName(folder)).ToArray();
                return versions;
            }
            return versionFolders;
        }
        static internal void InstallVersionFromGitHub(VersionInfo version)
        {
            // Get the URL for the version
            string url = $"https://github.com/ScratchEverywhere/ScratchEverywhere/archive/refs/tags/{version.Version}.zip";
            // Create a temp file
            FileInfo TempVersionFile = new(Path.GetTempFileName());
            // Download the file to a temp file
            using (HttpClient client = new())
            {
                byte[] zipfilebytes = client.GetByteArrayAsync(url).GetAwaiter().GetResult();
                using (FileStream file = TempVersionFile.Open(FileMode.Create))
                {
                    file.Write(zipfilebytes, 0, zipfilebytes.Length); // sry for the nesting btw
                } // FileStream
            } // Httpclient
            // Extract the zip file to the versions directory
            System.IO.Compression.ZipFile.ExtractToDirectory(TempVersionFile.FullName, VersionsDirectory.FullName);

            // find the extracted folders and normalize them to "major.minor"
            string[] extractedDirs = System.IO.Directory.GetDirectories(VersionsDirectory.FullName);
            string expectedName = $"{version.Major}.{version.Minor}";
            #region this is ai code all it does is rename the folder to major.minor.
            foreach (string dir in extractedDirs)
            {
                // get just the folder name (not the full path)
                string folderName = System.IO.Path.GetFileName(dir.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar));

                // match folders that contain the major.minor substring (case-insensitive)
                if (folderName.Contains(expectedName, StringComparison.OrdinalIgnoreCase))
                {
                    string desiredPath = System.IO.Path.Combine(VersionsDirectory.FullName, expectedName);

                    // if already correct, skip
                    if (string.Equals(dir, desiredPath, StringComparison.OrdinalIgnoreCase))
                    {
                        break;
                    }

                    // remove an existing target folder before moving
                    if (System.IO.Directory.Exists(desiredPath))
                    {
                        System.IO.Directory.Delete(desiredPath, true);
                    }

                    System.IO.Directory.Move(dir, desiredPath);
                    break; // done once we've found and moved the matching folder
                }
            }

            // cleanup temp zip file
            try { TempVersionFile.Delete(); } catch { /* ignore */ }
            #endregion 
        } // InstallVersionFromGitHub
        static internal async Task InstallVersionFromGitHub(VersionInfo version, ProgressBar progressBar)
        {
            // Get the URL for the version
            string url = $"https://github.com/ScratchEverywhere/ScratchEverywhere/archive/refs/tags/{version.Version}.zip";
            // Create a temp file
            FileInfo TempVersionFile = new(Path.GetTempFileName());
            progressBar.Value = 10;
            // Download the file to a temp file
            using (HttpClient client = new())
            {
                byte[] zipfilebytes = client.GetByteArrayAsync(url).GetAwaiter().GetResult();
                progressBar.Value = 20;
                using (FileStream file = TempVersionFile.Open(FileMode.Create))
                {
                    file.Write(zipfilebytes, 0, zipfilebytes.Length); // sry for the nesting btw
                    progressBar.Value = 30;
                } // FileStream
            } // Httpclient
            // Extract the zip file to the versions directory
            System.IO.Compression.ZipFile.ExtractToDirectory(TempVersionFile.FullName, VersionsDirectory.FullName);
            progressBar.Value = 40;
            // find the extracted folders and normalize them to "major.minor"
            string[] extractedDirs = System.IO.Directory.GetDirectories(VersionsDirectory.FullName);
            string expectedName = $"{version.Major}.{version.Minor}";
            progressBar.Value = 50;
            #region this is ai code all it does is rename the folder to major.minor.
            foreach (string dir in extractedDirs)
            {
                // get just the folder name (not the full path)
                string folderName = System.IO.Path.GetFileName(dir.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar));

                // match folders that contain the major.minor substring (case-insensitive)
                if (folderName.Contains(expectedName, StringComparison.OrdinalIgnoreCase))
                {
                    string desiredPath = System.IO.Path.Combine(VersionsDirectory.FullName, expectedName);
                    progressBar.Value = 70;
                    // if already correct, skip
                    if (string.Equals(dir, desiredPath, StringComparison.OrdinalIgnoreCase))
                    {
                        break;
                    }

                    // remove an existing target folder before moving
                    if (System.IO.Directory.Exists(desiredPath))
                    {
                        System.IO.Directory.Delete(desiredPath, true);
                        progressBar.Value = 80;
                    }

                    System.IO.Directory.Move(dir, desiredPath);
                    progressBar.Value = 90;
                    break; // done once we've found and moved the matching folder
                }
            }

            // cleanup temp zip file
            try { TempVersionFile.Delete(); progressBar.Value = 100; } catch { /* ignore */ }
            #endregion 
        } // InstallVersionFromGitHub
        static internal VersionInfo Parse(string version)
        {
            char[] separators = { '.' };
            string[] result = version.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            int major = int.Parse(result[0]);
            int minor = int.Parse(result[1]);
            VersionInfo info = new(major, minor);
            return info;
        }
        
    }
}

