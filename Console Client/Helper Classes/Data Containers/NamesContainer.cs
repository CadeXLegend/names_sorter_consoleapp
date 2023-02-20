namespace ConsoleClient.Helpers.DataContainers;

public struct NamesContainer
{
    public string FirstName { get; private set; }

    public string MiddleName { get; private set; }

    public string LastName { get; private set; }

    public string FullName { get; private set; }

    public NamesContainer(string FirstName, string MiddleName, string LastName)
    {
        this.FirstName = FirstName;
        this.MiddleName = MiddleName;
        this.LastName = LastName;
        FullName =
            MiddleName != string.Empty
                ? $"{FirstName} {MiddleName} {LastName}"
                : $"{FirstName} {LastName}";
    }
}
