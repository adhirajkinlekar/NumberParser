using Microsoft.VisualBasic.FileIO;
using NumberParser.classes;
using NumberParser.interfaces;
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

                if (args.Length != expectedArgumentsLength) throw new ArgumentException("The program accepts two arguments: a list of numbers and a file extention.");

                string numberArg = args[0];

                string formatArg = args[1];

                string[] content = FormatInputForWriting(numberArg);

                IFileWriter fileWriter = FileExtentionFactory(formatArg, content);

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

        private static IFileWriter FileExtentionFactory(string fileFormat, string[] content)
        {

            switch (fileFormat.ToLower())
            {
                case AppConstants.TXTExention:

                    return new TXTWriter(content);

                case AppConstants.XMLExention:

                    return new XMLWriter(content);

                case AppConstants.JSONExention:

                    return new JSONWriter(content);

                default:

                    throw new ArgumentException($"The program only supports the following formats: txt, xml and json.");
            }
        }
    }
}