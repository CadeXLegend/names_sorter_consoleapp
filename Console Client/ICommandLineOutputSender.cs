namespace ConsoleClient.Core;

    public interface ICommandLineOutputSender
    {
        void SendMessage(string message);
        void SendErrorMessage(object caller, string errorMessage);
    }
