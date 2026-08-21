using CryptoClipper.Models;
using CryptoClipper.Utils;

namespace CryptoClipper.Wallets;

public sealed class TronMatcher : IWalletPattern
{
    public CoinType CoinType => CoinType.Tron;

    public bool IsMatch(string address) =>
        RegexPatterns.Tron().IsMatch(address);
}
