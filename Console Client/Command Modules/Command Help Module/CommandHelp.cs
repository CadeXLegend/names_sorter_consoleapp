using ConsoleClient.Modules.DataContainers;

namespace ConsoleClient.Modules;

public sealed class CommandHelp : CommandModule
{
    public CommandHelp(IClient outputSender) : base(outputSender)
    {
        commandParameters = new CommandModuleParameters
        (
            CommandName: "--help",
            CommandNameAbbreviation: "--h",
            CommandDescription: "Shows a list of all available commands with their descriptions and usage examples.",
            CommandParameters: "None.\n\nUsage Example: --help\n"
        );
    }

    public override void Execute(string taskParameters)
    {
        outputSender.SendMessage("You can type the \"--help\" command at any time to view these commands.");
        outputSender.SendMessage("\nAvailable Commands: ");
        Client concreteClient = (Client)outputSender;
        foreach (ICommandModule module in concreteClient.Modules)
        {
            outputSender.SendMessage($"- {module.CommandParameters.CommandName} ({module.CommandParameters.CommandNameAbbreviation})");
            outputSender.SendMessage($"     - Description: {module.CommandParameters.CommandDescription}");
            outputSender.SendMessage($"     - Parameters: {module.CommandParameters.CommandParameters}");
        }
    }
}
