using ConsoleClient;
using ConsoleClient.Modules;

namespace Tests;

public class ClientTest
{
    private readonly Client client;

    public ClientTest()
    {
        client = new Client();
    }

    [Fact]
    public void ListenForInput_MessageSends()
    {
        client.TerminateCoreServices();
        string testMessage = "hello this is a message";
        client.SendMessage(testMessage);
        StringWriter output = new();
        Console.SetOut(output);
        bool isSame = testMessage.Contains(output.ToString());
        Assert.True(isSame);
    }

    [Fact]
    public void ListenForInput_ErrorMessageSends()
    {
        client.TerminateCoreServices();
        string testErrorMessage = "hello this is an error message";
        client.SendErrorMessage(this, testErrorMessage);
        StringWriter output = new();
        Console.SetOut(output);
        bool isSame = testErrorMessage.Contains(output.ToString());
        Assert.True(isSame);
    }


    [Fact]
    public void ListenForInput_GetsTestInput()
    {
        client.TerminateCoreServices();
        string userInput = "testinput";
        StringReader input = new(userInput);
        Console.SetIn(input);
        client.ListenForInput();
        bool isSame = userInput.Equals(client.ConsoleInput);
        Assert.True(isSame);
    }

    [Fact]
    public void ListenForInput_HasModuleAdded()
    {
        client.TerminateCoreServices();
        CommandHelp module = new(client);
        client.AddModule(module);
        Assert.Contains(module, client.Modules);
    }

    [Fact]
    public void ListenForInput_RunsModule()
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

    [Fact]
    public void ListenForInput_ModuleDoesntExist()
    {
        string userInput = "encode";
        string expectedOutput = $"Module of given name [{userInput}] has not been added to this client.";
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

    [Fact]
    public void ListenForInput_ModuleInputIsEmpty()
    {
        string userInput = "encode";
        string expectedOutput = $"Warning: A module was just called to run while the input is empty.\n";
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
