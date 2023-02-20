using ConsoleClient;
using ConsoleClient.Core;
using ConsoleClient.Modules;
using ConsoleClient.Modules.DataContainers;

namespace Tests;

public class ClientTest
{
    public sealed class MockModule1 : CommandModule
    {
        public MockModule1(ICommandLineOutputSender outputSender) : base(outputSender)
        {
            commandParameters = new CommandModuleParameters
                    (
                        CommandName: "notsospecial",
                        CommandNameAbbreviation: "nss",
                        CommandDescription: "It doesn't do much.",
                        CommandParameters: "None. "
                    );
        }

        public override void Execute(string taskParameters)
        {
            outputSender.SendMessage($"Executing the not so special task of: {GetType().Name}");
        }
    }

    public sealed class MockModule2 : CommandModule
    {
        public MockModule2(ICommandLineOutputSender outputSender) : base(outputSender)
        {
            commandParameters = new CommandModuleParameters
                    (
                        CommandName: "sospecial",
                        CommandNameAbbreviation: "ss",
                        CommandDescription: "It does something special.",
                        CommandParameters: "None. "
                    );
        }

        public override void Execute(string taskParameters)
        {
            outputSender.SendMessage($"Executing the special task of: {GetType().Name}");
        }
    }

    private readonly Client client;

    public ClientTest() => client = new Client();

    [Fact]
    public void ListenForInput_MessageSendsWithNewLine()
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
    public void ListenForInput_MessageSendsWithoutNewLine()
    {
        client.TerminateCoreServices();
        string testMessage = "hello this is a message";
        client.SendMessage(testMessage, true);
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
        MockModule1 module = new(client);
        client.AddModule(module);
        Assert.Contains(module, client.Modules);
    }

    [Fact]
    public void ListenForInput_HasModulesAdded()
    {
        client.TerminateCoreServices();
        MockModule1 helpModule = new(client);
        MockModule2 nameSortModule = new(client);
        client.AddModules(helpModule, nameSortModule);
        Assert.Contains(helpModule, client.Modules);
        Assert.Contains(expected: nameSortModule, client.Modules);
    }

    [Fact]
    public void ListenForInput_RunsModuleWithCommandFullName()
    {
        string userInput = "notsospecial";
        string expectedOutput = "Executing the not so special task of: MockModule1";
        MockModule1 module = new(client);
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
    public void ListenForInput_RunsModuleWithCommandNameAbbreviated()
    {
        string userInput = "ss";
        string expectedOutput = "Executing the special task of: MockModule2";
        MockModule2 module = new(client);
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
