using RagLauncher.Accounts;
using RagLauncher.Configuration;
using RagLauncher.Database;
using RagLauncher.Runtime;
using RagLauncher.Servers;
using RagLauncher.Validation;
using RagLauncher.Dashboard;
using RagLauncher.Core.Events;
using RagLauncher.Logging;

namespace RagLauncher.Core;

internal sealed class LauncherServices
{
    public DatabaseManager DatabaseManager { get; } = new();

    public DatabaseInstaller DatabaseInstaller { get; } = new();

    public AccountService AccountService { get; } = new();

    public ConfigurationService ConfigurationService { get; } = new();

    public ValidationService ValidationService { get; } = new();

    public ServerManager ServerManager { get; } = new();

    public RuntimeMonitor RuntimeMonitor { get; } = new();

    public DashboardService Dashboard { get; } = new();

    public EventBus EventBus { get; } = new();

    public LogService LogService { get; } = new();
}