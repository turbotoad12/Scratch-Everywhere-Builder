using System;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Scratch_Everywhere_Builder.Sebx
{
    public class SebxProject
    {
        [XmlElement("ProjectName")]
        public string ProjectName { get; set; }

        [XmlElement("ProjectDescription")]
        public string ProjectDescription { get; set; }

        [XmlElement("IconFile")]
        public FileReference IconFile { get; set; }

        [XmlElement("BannerFile")]
        public FileReference BannerFile { get; set; }

        [XmlElement("Sb3Folder")]
        public FolderReference Sb3Folder { get; set; }

        [XmlElement("TargetVersion")]
        public Version.VersionInfo TargetVersion { get; set; }
    }

    public class FileReference
    {
        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("Path")]
        public string Path { get; set; }
    }

    public class FolderReference
    {
        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("Path")]
        public string Path { get; set; }
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
