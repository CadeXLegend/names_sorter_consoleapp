namespace ConsoleClient.Core;

public interface ICommandLineInputListener
{
    delegate void InputReceivedEvent(object sender);
    event InputReceivedEvent OnInputReceived;

    public void ListenForInput(object sender);
}
