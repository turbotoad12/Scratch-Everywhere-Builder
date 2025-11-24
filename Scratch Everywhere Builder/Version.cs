using System.Net.Http.Headers;
using System.Text.Json;

namespace Scratch_Everywhere_Builder
{
    internal class Version
    {
        internal struct VersionInfo
        {
            internal int Major;
            internal int Minor;
            internal float Version;

            internal VersionInfo(int major = 0, int minor = 0)
            {
                Major = major;
                Minor = minor;
                Version = float.Parse($"{Major}.{Minor}");
            }

            public override string ToString()
            {
                return $"{Major}.{Minor}";
            }
        }

        static internal readonly DirectoryInfo VersionsDirectory =
            Directory.CreateDirectory(
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                             "SE! Builder", "versions"));
        static internal async Task<VersionInfo[]> GetVersionsFromGitHubAsync()
        {
            string url = "https://api.github.com/repos/ScratchEverywhere/ScratchEverywhere/tags";

            using var client = new HttpClient();
            client.DefaultRequestHeaders.UserAgent.Add(
                new ProductInfoHeaderValue("SE-Builder", "1.0"));

            string json = await client.GetStringAsync(url);

            using JsonDocument doc = JsonDocument.Parse(json);
            JsonElement root = doc.RootElement;

            List<VersionInfo> list = new();

            foreach (JsonElement tag in root.EnumerateArray())
            {
                if (!tag.TryGetProperty("name", out JsonElement nameElem))
                    continue;

                string name = nameElem.GetString() ?? "";

                // Expected only major.minor (ex: "0.29")
                string[] parts = name.Split('.');
                if (parts.Length != 2)
                    continue;

                if (int.TryParse(parts[0], out int major) &&
                    int.TryParse(parts[1], out int minor))
                {
                    list.Add(new VersionInfo(major, minor));
                }
            }
            return list
                .OrderByDescending(v => v.Major)
                .ThenByDescending(v => v.Minor)
                .ToArray();
        }


        static internal string[] GetAvailableVersions(bool returnnames = false)
        {

            // Search recursively for folders named in the form "major.minor" (e.g. 0.29).
            string[] allDirs = System.IO.Directory.GetDirectories(VersionsDirectory.FullName, "*", SearchOption.AllDirectories);

            // Find folder names that look like a version (two integer parts separated by a dot)
            var matchingNames = allDirs
                .Select(folder => System.IO.Path.GetFileName(folder))
                .Where(name =>
                {
                    if (string.IsNullOrEmpty(name)) return false;
                    var parts = name.Split('.');
                    if (parts.Length != 2) return false;
                    return int.TryParse(parts[0], out _) && int.TryParse(parts[1], out _);
                })
                .Distinct()
                .ToArray();

            if (returnnames)
            {
                return matchingNames;
            }

            // Return the full paths to matching folders. Prefer folders directly under the versions root when present.
            var matchingPaths = new List<string>();
            foreach (var name in matchingNames)
            {
                // look for a folder directly under VersionsDirectory with this name
                string directPath = System.IO.Path.Combine(VersionsDirectory.FullName, name);
                if (System.IO.Directory.Exists(directPath))
                {
                    matchingPaths.Add(directPath);
                    continue;
                }

                // otherwise pick the first found in the recursive search
                var firstFound = allDirs.FirstOrDefault(d => string.Equals(System.IO.Path.GetFileName(d), name, StringComparison.OrdinalIgnoreCase));
                if (firstFound != null)
                    matchingPaths.Add(firstFound);
            }

            return matchingPaths.ToArray();
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

