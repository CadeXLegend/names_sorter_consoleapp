using ConsoleClient.Core;
using ConsoleClient.Helpers.DataContainers;

namespace ConsoleClient.Helpers;

public class NamesSorter : INamesSorter
{
    public NamesSorter(ICommandLineOutputSender outputSender) => this.outputSender = outputSender;
    //ensuring that the client is never modified
    //after it is injected
    private readonly ICommandLineOutputSender outputSender;
    public string[]? SortAlphabetically(string[] listOfNames)
    {
        string className = GetType().Name;
        if (listOfNames.Length <= 0)
        {
            outputSender.SendErrorMessage(this, "Please provide a valid list of names.");
            outputSender.SendErrorMessage(this, "This list of names is empty.");
            return null;
        }
        int namesLength = listOfNames.Length;
        NamesContainer[] people = new NamesContainer[namesLength];
        string[] individualNames;
        for (int i = 0; i < namesLength; ++i)
        {
            if (listOfNames[i] == string.Empty)
            {
                outputSender.SendErrorMessage(this, "The element at {i} in the list of names provided is empty.");
                outputSender.SendErrorMessage(this, "Please ensure that there are no empty lines in the array provided.");
                return null;
            }
            individualNames = listOfNames[i].Split(' ');
            if (individualNames.Length <= 2)
                people[i] = new NamesContainer(individualNames[0], "", individualNames[1]);
            if (individualNames.Length > 2)
                people[i] = new NamesContainer(individualNames[0], individualNames[1], individualNames[2]);
        }

        string[]? sortedNames = people.OrderBy(
            person => person.LastName)
            .ThenBy(person => person.FirstName)
            .ThenBy(person => person.MiddleName)
            .Select(person => person.FullName).ToArray();
        return sortedNames;
    }
}
