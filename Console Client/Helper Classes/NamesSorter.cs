using ConsoleClient.Helpers.DataContainers;

namespace ConsoleClient.Helpers;

public class NamesSorter : INamesSorter
{
    public NamesSorter(IClient client) => this.client = client;
    //ensuring that the client is never modified
    //after it is injected
    private readonly IClient client;
    public string[]? SortAlphabetically(string[] listOfNames)
    {
        string className = GetType().Name;
        if (listOfNames.Length <= 0)
        {
            client.SendErrorMessage(this, "Please provide a valid list of names.");
            client.SendErrorMessage(this, "This list of names is empty.");
            return null;
        }
        int namesLength = listOfNames.Length;
        NamesContainer[] people = new NamesContainer[namesLength];
        string[] individualNames;
        for (int i = 0; i < namesLength; ++i)
        {
            if (listOfNames[i] == string.Empty)
            {
                client.SendErrorMessage(this, "The element at {i} in the list of names provided is empty.");
                client.SendErrorMessage(this, "Please ensure that there are no empty lines in the array provided.");
                return null;
            }
            individualNames = listOfNames[i].Split(' ');
            if (individualNames.Length <= 2)
                people[i] = new NamesContainer(individualNames[0], "", individualNames[1]);
            if (individualNames.Length > 2)
                people[i] = new NamesContainer(individualNames[0], individualNames[1], individualNames[2]);
        }

        string[]? sortedNames = people.OrderBy(
            person => person.lastName)
            .ThenBy(person => person.firstName)
            .ThenBy(person => person.middleName)
            .Select(person => person.fullName).ToArray();
        return sortedNames;
    }
}
