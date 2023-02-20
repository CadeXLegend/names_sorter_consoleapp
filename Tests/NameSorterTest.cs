using ConsoleClient.Core;
using ConsoleClient.Helpers;

namespace Tests;

public class NameSorterTest
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
    public void NameSorter_DoesSortAlphabetically()
    {
        string[] listOfNames =
            {
                "Issac Rivera",
                "Lillian Lamb",
                "Kaysen Richardson",
                "Allison Hines",
                "Uriel Ray Randolph",
                "Kailey Mata",
                "Ray Acosta",
                "Kaia Ayala",
                "Tanner Barton",
                "Danna Don Padilla",
                "Jaden Diaz",
                "Elena Massey",
                "Donald Valdez",
                "Diana Strong",
                "Axl Davidson"
            };
        string[] expectedOutput =
            {
                "Ray Acosta",
                "Kaia Ayala",
                "Tanner Barton",
                "Axl Davidson",
                "Jaden Diaz",
                "Allison Hines",
                "Lillian Lamb",
                "Elena Massey",
                "Kailey Mata",
                "Danna Don Padilla",
                "Uriel Ray Randolph",
                "Kaysen Richardson",
                "Issac Rivera",
                "Diana Strong",
                "Donald Valdez",
            };

        MockOutputSender sender = new();
        NamesSorter sorter = new(sender);
        string[]? sortedNames = sorter.SortAlphabetically(listOfNames);
        Assert.True(sortedNames != null);
        int sortedNamesLength = sortedNames.Length;
        Assert.True(sortedNamesLength == expectedOutput.Length);
        for (int i = 0; i < sortedNamesLength; ++i)
            Assert.True(sortedNames[i] == expectedOutput[i]);
    }

    [Fact]
    public void NameSorter_DoesWarningOnNoNames()
    {
        string[] listOfNames = { };
        MockOutputSender sender = new();
        NamesSorter sorter = new(sender);
        string expectedOutput = $"{sorter.GetType().Name}: This list of names is empty.";
        sorter.SortAlphabetically(listOfNames);
        Assert.True(sender.mockData == expectedOutput);
    }

        [Fact]
    public void NameSorter_DoesWarningOnEmptyElements()
    {
        string[] listOfNames = { "", "", "", "" };
        MockOutputSender sender = new();
        NamesSorter sorter = new(sender);
        string expectedOutput = $"{sorter.GetType().Name}: Please ensure that there are no empty lines in the array provided.";
        sorter.SortAlphabetically(listOfNames);
        Assert.True(sender.mockData == expectedOutput);
    }
}
