﻿using NumberParser.interfaces;
using NumberParser.misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Xml;

namespace NumberParser.classes
{
    public abstract class Writer
    {
        // Logic to get the file path and create the path if needed.

        public static string GetFilePath()
        {
            // directory - \bin\Debug\net8.0\
            string workingDirectory = Environment.CurrentDirectory;

            // Navigate up three directories to reach the root project folder
            DirectoryInfo projectDirectoryInfo = Directory.GetParent(workingDirectory)!.Parent!.Parent!;

            string projectDirectory = projectDirectoryInfo.FullName;

            string assetsDirectory = Path.Combine(projectDirectory, "assets");

            // Ensure the "assets" directory exists or create it
            if (!Directory.Exists(assetsDirectory))
            {
                Directory.CreateDirectory(assetsDirectory);
            }

            return assetsDirectory;
        }
    }

    public class TXTWriter(IEnumerable<string> content) : Writer, IFileWriter
    {
        public string Extension { get; } = $".{AppConstants.TXTExention}";

        public IEnumerable<string> Content { get; } = content;

        public void WriteToFile()
        {
            string fileName = Path.Combine(GetFilePath(), $"my_file{Extension}");

            using (StreamWriter writer = new(fileName))
            {

                foreach (string line in Content)
                {
                    writer.WriteLine(line);
                }

                writer.Close();
            }

        }
    }

    public class XMLWriter(IEnumerable<string> content) : Writer, IFileWriter
    {
        public string Extension { get; } = $".{AppConstants.XMLExention}";

        public IEnumerable<string> Content { get; } = content;

        public void WriteToFile()
        {

            string fileName = Path.Combine(GetFilePath(), $"my_file{Extension}");

            XmlWriterSettings xmlWriterSettings = new()
            {
                Indent = true,  // Set Indent to true to format the XML with indentation
                NewLineChars = "\n",  // Set NewLineChars to '\n' for new lines
                NewLineHandling = NewLineHandling.Replace  // Set NewLineHandling to Replace to ensure consistent newline characters
            };

            using (XmlWriter writer = XmlWriter.Create(fileName, xmlWriterSettings))
            {

                writer.WriteStartDocument();

                writer.WriteStartElement("Root");

                foreach (string line in Content)
                {
                    writer.WriteElementString("h3", line);
                }

                writer.WriteEndElement();

                writer.WriteEndDocument();

                writer.Flush();
            }
        }
    }

    public class JSONWriter(IEnumerable<string> content) : Writer, IFileWriter
    {
        public string Extension { get; } = $".{AppConstants.JSONExention}";

        public IEnumerable<string> Content { get; } = content;

        public void WriteToFile()
        {

            string fileName = Path.Combine(GetFilePath(), $"my_file{Extension}");

            string jsonString = JsonSerializer.Serialize(Content);

            File.WriteAllText(fileName, jsonString);
        }
    }
}
