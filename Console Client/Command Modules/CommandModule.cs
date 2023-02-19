using ConsoleClient.Modules.DataContainers;

namespace ConsoleClient.Modules;

/// <summary>
/// This is the CommandModule template class.
/// </summary>
abstract class CommandModule : ICommandModule
{
    protected ICommandModuleParamaters commandParameters;
    public ICommandModuleParamaters CommandParameters { get => commandParameters; }
    protected IClient client;

    public CommandModule(IClient client)
    {
        commandParameters = new CommandModuleParameters();
        this.client = client;
    }

    public abstract void Execute(string taskParameters);
}
