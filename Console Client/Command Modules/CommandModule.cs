using ConsoleClient.Core;
using ConsoleClient.Modules.DataContainers;

namespace ConsoleClient.Modules;

/// <summary>
/// This is the CommandModule template class.
/// </summary>
public abstract class CommandModule : ICommandModule
{
    protected ICommandModuleParamaters commandParameters;
    public ICommandModuleParamaters CommandParameters { get => commandParameters; }
    protected ICommandLineOutputSender outputSender;

    public CommandModule(ICommandLineOutputSender outputSender)
    {
        commandParameters = new CommandModuleParameters();
        this.outputSender = outputSender;
    }

    public abstract void Execute(string taskParameters);
}
