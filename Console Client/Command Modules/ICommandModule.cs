namespace ConsoleClient.Modules;

public interface ICommandModule
{
    ICommandModuleParamaters CommandParameters { get; }
    void Execute(string taskParameters);
}
