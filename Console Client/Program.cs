using ConsoleClient;
using ConsoleClient.Modules;
using ConsoleClient.Helpers;

Console.CursorVisible = true;

Client outputSender = new();
outputSender.InitializeCoreServices();

CommandHelp helpCommand = new(outputSender);
SortNamesFromFile sortNamesFromFile = new(outputSender);

outputSender.AddModules(helpCommand, sortNamesFromFile);

TextFileReader textFileReader = new();
string consoleLogoPath = ProjectDirectoryHelper.GetLocalPathTo(@"Logo");
string logo = textFileReader.ReadFromFile(consoleLogoPath);
outputSender.SendMessage(message: logo);
outputSender.SendMessage("You can type --h at any time to see a list of the available commands.\n");

outputSender.ListenForInput();
