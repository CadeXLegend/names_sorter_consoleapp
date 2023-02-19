using ConsoleClient;
using ConsoleClient.Modules;
using ConsoleClient.Helpers;

Console.CursorVisible = true;

Client client = new();
client.InitializeCoreServices();

CommandHelp helpCommand = new(client);
SortNamesFromFile sortNamesFromFile = new(client);

client.AddModules(helpCommand, sortNamesFromFile);

TextFileReader textFileReader = new();
string consoleLogoPath = ProjectDirectoryHelper.GetLocalPathTo(@"Logo");
string logo = textFileReader.ReadFromFile(consoleLogoPath);
client.SendMessage(message: logo);
client.SendMessage("You can type --h at any time to see a list of the available commands.\n");

client.ListenForInput(client);
