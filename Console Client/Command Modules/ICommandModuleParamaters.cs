namespace ConsoleClient.Modules;

public interface ICommandModuleParamaters
{
    string? CommandName { get; }
    string? CommandNameAbbreviation { get; }
    string? CommandDescription { get; }
    string? CommandParameters { get; }
}
