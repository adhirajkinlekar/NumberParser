# NumberParser

Write a C# console app called NumberParser which accepts a comma delimited list of integers as the first parameter, and a file format as the second parameter. The app would be invoked via the command line like this - NumberParser 4,5,1,9,10,58,34,12,0 XML. 

The app should parse the first parameter into an array of integers, sort it into descending order and then persist it in the specified format. The app should support text files, XML and JSON formats.

As well as delivering the requirements, the solution should demonstrate how to use an interface and how to use the factory pattern.


-- Steps to execute the program

1. Open root directory
2. Go to bin => Debug => net8.0
3. Run any of the following commands (Change the numbers as required)
     a) dotnet NumberParser.dll 1,2,3,4,5 txt
     b) dotnet NumberParser.dll 100,300,200,800,100 xml
     3) dotnet NumberParser.dll 6,4,8,12,22,74 json
4. Open the assets folder in the root directory to see the output
