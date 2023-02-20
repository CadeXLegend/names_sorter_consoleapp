using ConsoleClient;
using ConsoleClient.Core;
using ConsoleClient.Modules;
using ConsoleClient.Modules.DataContainers;

namespace Tests;

public class CommandHelpTest
{
    private readonly Client client;

    public CommandHelpTest() => client = new();

    [Fact]
    public void CommandHelp_RunsSuccessfully()
    {
        string userInput = "--h";
        string expectedOutput =
        "You can type the \"--help\" command at any time to view these commands."
        + "\nAvailable Commands: "
        + "\n- --help (--h)"
        + "\n     - Description: Shows a list of all available commands with their descriptions and usage examples."
        + "\n     - Parameters: None."
        + "\nUsage Example: --help\n";
        CommandHelp module = new(client);
        client.AddModule(module);
        Assert.Contains(module, client.Modules);
        client.OnModuleRan += () =>
        {
            client.TerminateCoreServices();
            StringWriter output = new();
            Console.SetOut(output);
            bool isSame = expectedOutput.Contains(output.ToString());
            Assert.True(isSame);
        };
        client.InitializeCoreServices();
        StringReader input = new(userInput);
        Console.SetIn(input);
        client.ListenForInput();
    }
}
