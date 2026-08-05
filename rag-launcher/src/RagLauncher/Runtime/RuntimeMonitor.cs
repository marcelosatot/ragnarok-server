
using System.Diagnostics;

namespace RagLauncher.Runtime;

internal sealed class RuntimeMonitor
{
    private readonly DateTime _startedAt = DateTime.Now;

    public async Task StartAsync(RuntimeStatistics runtime)
    {
        while (true)
        {
            Update(runtime);

            await Task.Delay(1000);
        }
    }

    private void Update(RuntimeStatistics runtime)
    {
        runtime.State.LoginOnline =
            Process.GetProcessesByName("login-server").Any();

        runtime.State.CharOnline =
            Process.GetProcessesByName("char-server").Any();

        runtime.State.MapOnline =
            Process.GetProcessesByName("map-server").Any();

        runtime.State.DatabaseOnline =
            Process.GetProcessesByName("mariadbd").Any();
    }

    private void UpdateMemory(RuntimeStatistics runtime)
    {
        runtime.State.MemoryMB =
    Process.GetCurrentProcess().WorkingSet64 / 1024d / 1024d;

        runtime.State.LoginMemory =
            GetMemory("login-server");

        runtime.State.CharMemory =
            GetMemory("char-server");

        runtime.State.MapMemory =
            GetMemory("map-server");
    }

    private void UpdateServers(RuntimeStatistics runtime)
    {
        runtime.State.LoginOnline =
            Process.GetProcessesByName("login-server").Any();

        runtime.State.CharOnline =
            Process.GetProcessesByName("char-server").Any();

        runtime.State.MapOnline =
            Process.GetProcessesByName("map-server").Any();

        runtime.State.DatabaseOnline =
            Process.GetProcessesByName("mariadbd").Any();
    }

    private void UpdateUptime(RuntimeStatistics runtime)
    {
        runtime.State.Uptime =
    DateTime.Now - runtime.State.StartedAt;
    }

    private static long GetMemory(string process)
    {
        var p = Process.GetProcessesByName(process).FirstOrDefault();

        if (p == null)
            return 0;

        return p.WorkingSet64 / 1024 / 1024;
    }
}