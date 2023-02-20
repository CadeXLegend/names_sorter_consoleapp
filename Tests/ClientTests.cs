using Xunit;

namespace ConsoleClient.Tests;

public class ClientTests
{
    private readonly Client client;

    public ClientTests()
    {
        client = new Client();
        client.InitializeCoreServices();
    }


    [Fact]
    public void ListenForInput_ReturnsInput_NotNull()
    {
        // StringWriter output = new();
        // Console.SetOut(output);
        string expected = "input received";

        client.ListenForInput(this);

        StringReader input = new(expected);
        Console.SetIn(input);

        Assert.Same(expected, client.ConsoleInput);

    }

}
