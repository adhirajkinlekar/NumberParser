using NumberParser.classes;
using NumberParser.interfaces;
using NumberParser.misc; 

namespace NumberParser
{
    class Program
    {
        public static void Main(string[] args)
        {

            try
            {

                App app = new(args);

                app.Execute();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }

    class App
    {

        private string NumberArg { get; }

        private string ExtentionArg { get; }


        public App(string[] args)
        {

            if (args.Length != AppConstants.expectedAppArgumentsLength) throw new ArgumentException("The program accepts two arguments: a list of numbers and a file extention.");

            NumberArg = args[0];

            ExtentionArg = args[1];
        }

        public void Execute()
        {
            string[] content = FormatInputForWriting(NumberArg);

            IFileWriter fileWriter = FileExtentionFactory(ExtentionArg, content);

            fileWriter.WriteToFile();

            Console.WriteLine("Input has been successfully parsed and written to a file in the assets folder in the root directory.");
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

        private static IFileWriter FileExtentionFactory(string fileExtention, string[] content)

        {
            return fileExtention.ToLower() switch
            {
                AppConstants.TXTExention => new TXTWriter(content),

                AppConstants.XMLExention => new XMLWriter(content),

                AppConstants.JSONExention => new JSONWriter(content),

                _ => throw new ArgumentException($"The program only supports the following extentions: txt, xml and json.")
            };
        }
    }
}