using System;
using System.Diagnostics;
using System.Xml.Serialization;
using System.IO;

namespace Scratch_Everywhere_Builder.Sebx
{
    /*
    Plan (pseudocode, detailed):
    - Problem: CS0053 because property exposes a type (Version.VersionInfo) that is less accessible than the property.
      Fix: make the property no more accessible than the type by changing the property's accessibility to 'internal'.
    - Problem: CS8618 for several non-nullable properties not initialized.
      Fix: initialize those properties with safe defaults so they are non-null after construction:
        - strings -> string.Empty
        - reference-type properties -> new instances of their types
        - struct property -> default value (structs are non-nullable by default)
    - Keep XML attributes in place (note: XmlSerializer only serializes public members; if serialization of TargetVersion is required,
      you'll need to make the underlying type public or provide a public wrapper).
    - Ensure required using directives are present (added System.IO because Save/Load use StreamReader/Writer and File).
    - Implement changes in-place in this file.
    */

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

        [XmlElement("TargetVersion")]
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
