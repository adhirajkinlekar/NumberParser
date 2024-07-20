# NumberParser

## Description

The **NumberParser** is a C# console application that accepts a comma-delimited list of integers as the first parameter and a file format as the second parameter. The app sorts the integers in descending order and persists them in the specified format. The app supports text files, XML, and JSON formats. The solution also demonstrates the use of an interface and the factory pattern.

## Steps to Execute the Program

1. Open the root directory.
2. Navigate to `bin => Debug => net8.0`.
3. Run any of the following commands (change the numbers as required):
    - `dotnet NumberParser.dll 1,2,3,4,5 txt`
    - `dotnet NumberParser.dll 100,300,200,800,100 xml`
    - `dotnet NumberParser.dll 6,4,8,12,22,74 json`
4. Open the `assets` folder in the root directory to see the output.
