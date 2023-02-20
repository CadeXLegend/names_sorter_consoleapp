using System.Text;
using ConsoleClient.Helpers;
using ConsoleClient.Modules.DataContainers;

namespace ConsoleClient.Modules;

public sealed class SortNamesFromFile : CommandModule
{
    private readonly string sortedFileName = "sorted-names-list.txt";
    private readonly ITextFileReader fileReader;
    private readonly ITextFileWriter fileWriter;
    private readonly INamesSorter namesSorter;

    public SortNamesFromFile(IClient client) : base(client)
    {
        commandParameters = new CommandModuleParameters
        (
            CommandName: "sort-names",
            CommandNameAbbreviation: "sn",
            CommandDescription: "Sorts names from a text file in order of: last name -> first name -> middle name.",
            CommandParameters: "Provide a given path to the text file. "
                             + "It uses the application's root path by default. "
                             + "\n\nUsage Example: sn unsorted-names-list.txt\n"
        );
        fileReader = new TextFileReader();
        fileWriter = new TextFileWriter();
        //here we don't parse in the entire client class,
        //we only inject the outputsender interface from the client
        namesSorter = new NamesSorter(client);
    }

    public override void Execute(string taskParameters)
    {
        string path = ProjectDirectoryHelper.GetLocalPathTo(taskParameters);
        string namesFromFile = fileReader.ReadFromFile(path);
        if(namesFromFile == string.Empty)
        {
            client.SendErrorMessage(this, $"\"{path}\" is not a valid path.");
            return;
        }
        string[] namesToSort = namesFromFile.Split(Environment.NewLine);
        string[]? sortedNames = namesSorter.SortAlphabetically(namesToSort);
        if (sortedNames == null)
        {
            client.SendErrorMessage(this, "The source of names provided is null.");
            return;
        }
        StringBuilder fullNamesList = new();
        foreach (string fullName in sortedNames)
        {
            fullNamesList.AppendLine(fullName);
            client.SendMessage(fullName);
        }
        int indexFromLastTrailingSlash = path.LastIndexOf('/') + 1;
        string trimmedPath = path.Substring(0, indexFromLastTrailingSlash);
        string finalPath = $"{trimmedPath}{sortedFileName}";
        client.SendMessage($"\nSorting Completed");
        fileWriter.WriteToFile(finalPath, fullNamesList.ToString().TrimEnd(), FileMode.Create);
        client.SendMessage($"\nSave Completed");
        client.SendMessage($"\nThe File Was Saved To:");
        client.SendMessage($"--> {finalPath}\n");
    }
}
