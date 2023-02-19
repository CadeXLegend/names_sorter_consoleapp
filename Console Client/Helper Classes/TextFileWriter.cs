namespace ConsoleClient.Helpers;

public class TextFileWriter : ITextFileWriter
{
    public void WriteToFile(string filePath, string data, FileMode writeToFileMode)
    {
        FileStream fs = File.Open(filePath, writeToFileMode);
        StreamWriter sw = new(fs);
        sw.Write(data);
        sw.Close();
    }
}
