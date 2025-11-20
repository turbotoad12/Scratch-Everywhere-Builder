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

        [XmlElement("IconFile")]
        public FileInfo IconFile { get; set; } = new FileInfo(string.Empty);

        [XmlElement("BannerFile")]
        public FileInfo BannerFile { get; set; } = new FileInfo(string.Empty);

        [XmlElement("Sb3Folder")]
        public DirectoryInfo Sb3Folder { get; set; } = new DirectoryInfo(string.Empty);
        [XmlElement("OutputFolder")]
        internal Version.VersionInfo TargetVersion { get; set; } = new Version.VersionInfo();
    }

    public class FolderReference
    {
        [XmlElement("Name")]
        public string Name { get; set; } = string.Empty;

        [XmlElement("Path")]
        public string Path { get; set; } = string.Empty;
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
