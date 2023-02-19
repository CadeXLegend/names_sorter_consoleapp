namespace ConsoleClient.Helpers;

public static class ProjectDirectoryHelper
{
    public static string GetLocalPathTo(string directory)
    {
        return Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @$"{directory}"));
    }
}
