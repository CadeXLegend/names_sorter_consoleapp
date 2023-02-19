namespace ConsoleClient.Helpers;

public class TextFileReader : ITextFileReader
{
    public string ReadFromFile(string filePath)
    {
        return File.Exists(filePath) ? File.ReadAllText(filePath) : string.Empty;
    }
}
