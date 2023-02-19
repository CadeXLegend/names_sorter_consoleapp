using ConsoleClient.Core;
using ConsoleClient.Modules;

namespace ConsoleClient;

public interface IClient : ICommandLineInputListener, ICommandLineOutputSender, ICommandModuleRegistrar
{
    void InitializeCoreServices();
}
