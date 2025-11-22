using System;
using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;

namespace Scratch_Everywhere_Builder.Sebx
{
    public class SebxProject
    {
        [XmlElement("ProjectName")]
        public string ProjectName { get; set; } = string.Empty;

        [XmlElement("ProjectDescription")]
        public string ProjectDescription { get; set; } = string.Empty;

        [XmlElement("IconFile", IsNullable = true)]
        public FileInfo? IconFile { get; set; } = null;

        [XmlElement("BannerFile", IsNullable = true)]
        public FileInfo? BannerFile { get; set; } = null;

        [XmlElement("Sb3Folder", IsNullable = true)]
        public DirectoryInfo? Sb3Folder { get; set; } = null;

        [XmlElement("TargetVersion")]
        internal Version.VersionInfo TargetVersion { get; set; } = new Version.VersionInfo();
    }

    public static class SebxProjectIO
    {
        /// <summary>
        /// Saves a SebxProject object to an XML file.
        /// </summary>
        public static void Save(SebxProject project, string filePath)
        {
            var serializer = new XmlSerializer(typeof(SebxProject));
            using (var writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, project);
            }
        }

        /// <summary>
        /// Loads a SebxProject object from an XML file.
        /// </summary>
        public static SebxProject Load(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Project file not found", filePath);

            var serializer = new XmlSerializer(typeof(SebxProject));
            using (var reader = new StreamReader(filePath))
            {
                return (SebxProject)serializer.Deserialize(reader);
            }
        }
    }
}
