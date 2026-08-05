using System.Diagnostics;

namespace RagLauncher.Runtime;

internal sealed class RuntimeMonitor
{
    public async Task StartAsync(RuntimeStatistics statistics)
    {
        while (true)
        {
            Update(statistics);

            await Task.Delay(1000);
        }
    }

    private static void Update(RuntimeStatistics statistics)
    {
        statistics.State.LoginOnline =
            Process.GetProcessesByName("login-server").Any();

        statistics.State.CharOnline =
            Process.GetProcessesByName("char-server").Any();

        statistics.State.MapOnline =
            Process.GetProcessesByName("map-server").Any();

        statistics.State.LoginMemory =
            GetMemory("login-server");

        statistics.State.CharMemory =
            GetMemory("char-server");

        statistics.State.MapMemory =
            GetMemory("map-server");
    }

    private static long GetMemory(string process)
    {
        var p = Process.GetProcessesByName(process).FirstOrDefault();

        if (p == null)
            return 0;

        return p.WorkingSet64 / 1024 / 1024;
    }
}