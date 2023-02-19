namespace ConsoleClient.Modules.DataContainers;

public struct CommandModuleParameters : ICommandModuleParamaters
{
    public string? CommandName { get; set; }

    public string? CommandNameAbbreviation { get; set; }

    public string? CommandDescription { get; set; }

    public string? CommandParameters { get; set; }

    public CommandModuleParameters(string CommandName, string CommandNameAbbreviation, string CommandDescription, string CommandParameters)
    {
        this.CommandName = CommandName;
        this.CommandNameAbbreviation = CommandNameAbbreviation;
        this.CommandDescription = CommandDescription;
        this.CommandParameters = CommandParameters;
    }
}
