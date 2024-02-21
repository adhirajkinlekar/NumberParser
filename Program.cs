using Microsoft.VisualBasic.FileIO;
using NumberParser.misc;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Text.Json;
using System.Xml;

namespace NumberParser
{
    class App
    {
        public static void Main(string[] args)
        {

            try
            {
                const int expectedArgumentsLength = 2;

                if (args.Length != expectedArgumentsLength) throw new ArgumentException("The program accepts two arguments: a list of numbers and a file format.");

                string numberArg = args[0];

                string formatArg = args[1];

                string[] content = FormatInputForWriting(numberArg);

                IFileWriter fileWriter = FileFormatFactory(formatArg, content);

                fileWriter.WriteToFile();

                Console.WriteLine("Input has been successfully parsed and written to a file in the assets folder in the root directory.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        private static string[] FormatInputForWriting(string numbersString)
        {
            try
            {
                return numbersString.Split(',')
                    .Select(str => int.Parse(str.Replace(" ", "")))
                    .OrderByDescending(num => num)
                    .Select(x => x.ToString())
                    .ToArray();
            }
            catch (FormatException)
            {
                throw new Exception("Invalid input, the program only accepts numbers.");
            }
        }

        private static IFileWriter FileFormatFactory(string fileFormat, string[] content)
        {

            switch (fileFormat.ToLower())
            {
                case AppConstants.TXTFileFormat:

                    return new TXTWriter(content);

                case AppConstants.XMLFileFormat:

                    return new XMLWriter(content);

                case AppConstants.JSONFileFormat:

                    return new JSONWriter(content);

                default:

                    throw new ArgumentException($"The program only supports the following formats: txt, xml and json.");
            }
        }
    }

    interface IFileWriter
    {
        string Extension { get; }

        string[] Content { get; } 

        void WriteToFile();
    }

    public class Writer
    {
        // Logic to get the file path and create the path if needed.

        protected static string GetFilePath()
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

    public class TXTWriter : Writer, IFileWriter
    {
        public string Extension { get; } = $".{AppConstants.TXTFileFormat}";

        public string[] Content { get; }
   
        public TXTWriter(string[] content)
        {
            Content = content;
        }

        public void WriteToFile()
        {
            string fileName = Path.Combine(GetFilePath(), $"my_file{Extension}");

            using (StreamWriter writer = new(fileName))
            {

                foreach(string line in Content)
                {
                    writer.WriteLine(line);
                }

                writer.Close();
            }

        }
    }

    public class XMLWriter : Writer, IFileWriter
    {
        public string Extension { get; } = $".{AppConstants.XMLFileFormat}";

        public string[] Content { get; }

        public XMLWriter(string[] content)
        {

            Content = content;
        }

        public void WriteToFile()
        {
          
            string fileName = Path.Combine(GetFilePath(), $"my_file{Extension}");

            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings
            {
                Indent = true,  // Set Indent to true to format the XML with indentation
                NewLineChars = "\n",  // Set NewLineChars to '\n' for new lines
                NewLineHandling = NewLineHandling.Replace  // Set NewLineHandling to Replace to ensure consistent newline characters
            };

            using (XmlWriter writer = XmlWriter.Create(fileName, xmlWriterSettings))
            {

                writer.WriteStartDocument();

                writer.WriteStartElement("Root");

                foreach(string line in Content)
                {
                    writer.WriteElementString("h3", line);
                }

                writer.WriteEndElement();

                writer.WriteEndDocument();

                writer.Flush();
            }
        }
    }

    public class JSONWriter : Writer, IFileWriter
    {
        public string Extension { get; } = $".{AppConstants.JSONFileFormat}";

        public string[] Content { get; }

        public JSONWriter(string[] content)
        {

            Content = content;
        }

        public void WriteToFile()
        {

            string fileName = Path.Combine(GetFilePath(), $"my_file{Extension}");

            string jsonString = JsonSerializer.Serialize(Content);

            File.WriteAllText(fileName, jsonString);
        }
    }
}