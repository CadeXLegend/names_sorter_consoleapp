namespace ConsoleClient.Core;

    public interface ICommandLineOutputSender
    {
        void SendMessage(string message, bool noNewLine = false);
        void SendErrorMessage(object caller, string errorMessage);
    }
