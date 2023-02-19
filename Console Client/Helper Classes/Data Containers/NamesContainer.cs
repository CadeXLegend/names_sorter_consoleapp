namespace ConsoleClient.Helpers.DataContainers;

public struct NamesContainer
{
    public string firstName { get; private set; }

    public string middleName { get; private set; }

    public string lastName { get; private set; }

    public string fullName { get; private set; }

    public NamesContainer(string FirstName, string MiddleName, string LastName)
    {
        firstName = FirstName;
        middleName = MiddleName;
        lastName = LastName;
        fullName =
            middleName != string.Empty
                ? $"{firstName} {middleName} {lastName}"
                : $"{firstName} {lastName}";
    }
}
