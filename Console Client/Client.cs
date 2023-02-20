using ConsoleClient.Core;
using ConsoleClient.Modules;

namespace ConsoleClient;

/// <summary>
/// This is a base concrete class for a Console to run using the <see cref="ConsoleClient.Modules.ICommandModule"/> Framework.
/// </summary>
public class Client : IClient
{
    public event ICommandLineInputListener.InputReceivedEvent? OnInputReceived;
    public event ICommandLineInputListener.InputReceivedEvent? OnModuleRan;
    //I'm enforcing readonly on this list because 
    //I want to ensure module assignments are non-erasable
    private readonly List<ICommandModule> modules = new();
    public List<ICommandModule> Modules { get => modules; }
    private string? consoleInput = null;
    public string? ConsoleInput { get => consoleInput; }

    //I've chosen this event-driven loop because I prefer the flexibility and control it gives
    public void InitializeCoreServices()
    {
        OnInputReceived += RunModule;
        OnModuleRan += ListenForInput;
    }

    //this gives us some extra control over
    //when we want the feedback loop to run
    public void TerminateCoreServices()
    {
        OnInputReceived -= RunModule;
        OnModuleRan -= ListenForInput;
    }

    public void ListenForInput()
    {
        string? input = Console.ReadLine();
        consoleInput = input;
        OnInputReceived?.Invoke();
    }

    public void SendMessage(string message, bool noNewLine = false)
    {
        if (noNewLine)
            Console.Write(message);
        else
            Console.WriteLine(message);
    }

    public void SendErrorMessage(object caller, string errorMessage)
    {
        Console.WriteLine($"\n({caller.GetType().Name}): {errorMessage}\n");
    }

    public void AddModule(ICommandModule module)
    {
        modules.Add(module);
    }

    public void AddModules(params ICommandModule[] modules)
    {
        foreach (ICommandModule module in modules)
            this.modules.Add(module);
    }

    //you'll notice I call OnModuleRan in every exit condition,
    //this is because I want to restart the input -> output loop
    //whether the module completes or not
    //I could add OnModuleRanSuccessfully and Unsuccessfully but no need
    //for the scope of this project
    private void RunModule()
    {
        if (consoleInput == null)
        {
            SendMessage("Warning: A module was just called to run while the input is empty.\n");
            OnModuleRan?.Invoke();
            return;
        }

        string moduleName = consoleInput.Split(' ')[0];
        string taskParameters = consoleInput.Replace(moduleName + " ", "");
        ICommandModule? module = null;
        foreach (ICommandModule mod in modules)
        {
            if (mod.CommandParameters.CommandName == moduleName
            || mod.CommandParameters.CommandNameAbbreviation == moduleName)
            {
                module = mod;
                break;
            }
            continue;
        }

        if (module == null)
        {
            SendMessage($"Module of given name [{moduleName}] has not been added to this client.\n");
            OnModuleRan?.Invoke();
            return;
        }

        module.Execute(taskParameters);
        OnModuleRan?.Invoke();
    }
}
