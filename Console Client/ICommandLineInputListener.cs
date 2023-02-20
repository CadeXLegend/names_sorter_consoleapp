namespace ConsoleClient.Core;

public interface ICommandLineInputListener
{
    delegate void InputReceivedEvent();
    event InputReceivedEvent OnInputReceived;

    public void ListenForInput();
}
