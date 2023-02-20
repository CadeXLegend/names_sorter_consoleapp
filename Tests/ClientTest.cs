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
    public void ListenForInput_GetsTestInput()
    {
        string userInput = "testinput";
        //string expectedOutput = "Module of given name [testinput] has not been added to this client.";
        StringReader input = new(userInput);
        Console.SetIn(input);
        client.ListenForInput();
        //StringWriter output = new();
        //Console.SetOut(output);
        bool isSame = userInput.Equals(client.ConsoleInput);
        Assert.True(isSame);
    }

    [Fact]
    public void ListenForInput_HasModuleAdded()
    {
        CommandHelp module = new(client);
        client.AddModule(module);
        Assert.Contains(module, client.Modules);
    }
}
