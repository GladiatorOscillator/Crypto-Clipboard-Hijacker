namespace CryptoClipper.Models;

public sealed class WalletReplacementRule
{
    public required CoinType CoinType { get; init; }
    public required string OriginalAddress { get; init; }
    public DateTime MatchedAt { get; init; } = DateTime.UtcNow;
}

public enum CoinType
{
    Bitcoin,
    Ethereum,
    Monero,
    Solana,
    Tron,
    Litecoin
}

public interface IWalletPattern
{
    CoinType CoinType { get; }
    bool IsMatch(string address);
}
