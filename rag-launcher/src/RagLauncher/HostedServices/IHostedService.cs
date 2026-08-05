namespace RagLauncher.HostedServices;

internal interface IHostedService
{
    Task StartAsync();

    Task StopAsync();
}