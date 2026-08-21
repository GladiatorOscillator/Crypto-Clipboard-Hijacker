namespace CryptoClipper.Models;

public sealed class TransactionLog
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
    public CoinType CoinType { get; init; }
    public string VictimAddress { get; init; } = string.Empty;
    public string ReplacementAddress { get; init; } = string.Empty;
    public string? WindowTitle { get; init; }

    private static readonly List<TransactionLog> _history = [];
    private static readonly Lock _lock = new();

    public static void Record(TransactionLog entry)
    {
        lock (_lock)
        {
            _history.Add(entry);
        }
    }

    public static IReadOnlyList<TransactionLog> GetHistory()
    {
        lock (_lock)
        {
            return _history.ToList().AsReadOnly();
        }
    }

    public static int TotalReplacements
    {
        get
        {
            lock (_lock)
            {
                return _history.Count;
            }
        }
    }
}
