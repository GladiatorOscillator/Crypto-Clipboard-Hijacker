namespace CryptoClipper.Models;

public sealed class ClipboardEvent
{
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public string OriginalContent { get; set; } = string.Empty;
    public string? ReplacedContent { get; set; }
    public bool WasReplaced { get; set; }

    public TimeSpan Age => DateTime.UtcNow - Timestamp;
}
