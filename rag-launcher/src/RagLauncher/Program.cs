using RagLauncher.Core;
using RagLauncher.Logging;

Console.Title = "Rag Launcher";

Logger.Title("Rag Launcher");

await new LauncherHost().RunAsync();