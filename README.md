 
## NumberParser

### Description
The **NumberParser** is a C# console application that:

- Accepts a comma-delimited list of integers as the first parameter.
- Accepts a file format as the second parameter.
- Sorts the integers in descending order.
- Persists the sorted integers in the specified format.

The application supports text files, XML, and JSON formats. It also demonstrates the use of an interface and the factory pattern.

### Steps to Execute the Program
1. Open the root directory.
2. Navigate to `bin => Debug => net8.0`.
3. Run any of the following commands (change the numbers as required):
    ```bash
    dotnet NumberParser.dll 1,2,3,4,5 txt
    dotnet NumberParser.dll 100,300,200,800,100 xml
    dotnet NumberParser.dll 6,4,8,12,22,74 json
    ```
4. Open the `assets` folder in the root directory to see the output.  
