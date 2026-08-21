using CryptoClipper.Models;
using CryptoClipper.Utils;

namespace CryptoClipper.Wallets;

public sealed class SolanaMatcher : IWalletPattern
{
    public CoinType CoinType => CoinType.Solana;

    public bool IsMatch(string address) =>
        RegexPatterns.Solana().IsMatch(address);
}
