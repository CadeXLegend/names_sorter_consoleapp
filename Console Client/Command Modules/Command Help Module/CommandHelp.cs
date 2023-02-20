using ConsoleClient.Modules.DataContainers;

namespace ConsoleClient.Modules;

public sealed class CommandHelp : CommandModule
{
    public CommandHelp(IClient client) : base(client)
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
        client.SendMessage("You can type the \"--help\" command at any time to view these commands.");
        client.SendMessage("\nAvailable Commands: ");
        Client concreteClient = (Client)client;
        foreach (ICommandModule module in concreteClient.Modules)
        {
            client.SendMessage($"- {module.CommandParameters.CommandName} ({module.CommandParameters.CommandNameAbbreviation})");
            client.SendMessage($"     - Description: {module.CommandParameters.CommandDescription}");
            client.SendMessage($"     - Parameters: {module.CommandParameters.CommandParameters}");
        }
    }
}
