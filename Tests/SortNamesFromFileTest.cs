using ConsoleClient.Core;
using ConsoleClient.Modules;
using ConsoleClient.Helpers;

namespace Tests;

public class SortNamesFromFileTest
{
    public class MockOutputSender : ICommandLineOutputSender
    {
        public string mockData = "";
        public void SendErrorMessage(object caller, string errorMessage)
        {
            mockData = $"{caller.GetType().Name}: {errorMessage}";
        }

        public void SendMessage(string message, bool noNewLine = false)
        {
            mockData = message;
        }
    }

    [Fact]
    public void SortNamesFromFile_DetectsInvalidPath()
    {
        MockOutputSender outputSender = new();
        SortNamesFromFile namesSorterModule = new(outputSender);
        string userInput = "mockpath";
        string formattedPath = ProjectDirectoryHelper.GetLocalPathTo(userInput);
        string expectedOutput = $"{namesSorterModule.GetType().Name}: \"{formattedPath}\" is not a valid path or the file is empty.";
        namesSorterModule.Execute(userInput);
        Assert.True(expectedOutput == outputSender.mockData);
    }

    [Fact]
    public void SortNamesFromFile_DetectsEmptyFile()
    {
        MockOutputSender outputSender = new();
        SortNamesFromFile namesSorterModule = new(outputSender);
        string userInput = "mock-names-list-empty.txt";
        string formattedPath = ProjectDirectoryHelper.GetLocalPathTo(userInput);
        string expectedOutput = $"{namesSorterModule.GetType().Name}: \"{formattedPath}\" is not a valid path or the file is empty.";
        namesSorterModule.Execute(userInput);
        Assert.True(expectedOutput == outputSender.mockData);
    }

    [Fact]
    public void SortNamesFromFile_RunsSuccessfullyAndSavesFile()
    {
        MockOutputSender outputSender = new();
        SortNamesFromFile namesSorterModule = new(outputSender);
        string userInput = "mock-names-list-1000.txt";
        namesSorterModule.Execute(userInput);
        string formattedPath = ProjectDirectoryHelper.GetLocalPathTo(userInput);
        int indexFromLastTrailingSlash = formattedPath.LastIndexOf('/') + 1;
        string trimmedPath = formattedPath.Substring(0, indexFromLastTrailingSlash);
        string finalPath = $"{trimmedPath}sorted-names-list.txt";
        string expectedOutput = $"--> {finalPath}\n";
        Assert.True(expectedOutput == outputSender.mockData);
    }
}
