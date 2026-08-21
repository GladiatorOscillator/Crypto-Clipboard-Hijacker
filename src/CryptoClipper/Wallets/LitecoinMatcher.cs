using CryptoClipper.Models;
using CryptoClipper.Utils;

namespace CryptoClipper.Wallets;

public sealed class LitecoinMatcher : IWalletPattern
{
    public CoinType CoinType => CoinType.Litecoin;

    public bool IsMatch(string address) =>
        RegexPatterns.Litecoin().IsMatch(address);
}
