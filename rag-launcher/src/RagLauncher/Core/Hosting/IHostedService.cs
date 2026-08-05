namespace RagLauncher.Core.Hosting;

internal interface IHostedService
{
    Task StartAsync();

    Task StopAsync();
}