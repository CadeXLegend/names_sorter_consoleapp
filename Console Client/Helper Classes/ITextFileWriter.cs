namespace ConsoleClient.Helpers;

public interface ITextFileWriter
{
    void WriteToFile(string filePath, string data, FileMode writeToFileMode);
}
