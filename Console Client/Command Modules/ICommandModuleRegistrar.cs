namespace ConsoleClient.Modules;

public interface ICommandModuleRegistrar
{
    void AddModule(ICommandModule module);
    void AddModules(params ICommandModule[] modules);
}
