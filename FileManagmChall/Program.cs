using FileManagmChall;

string source = @"C:\Temp\Source";
string destination = @"C:\Temp\Destination";


FileProcessor.CopyFiles(source, destination, true, true);

Console.WriteLine("File Copy Complete");