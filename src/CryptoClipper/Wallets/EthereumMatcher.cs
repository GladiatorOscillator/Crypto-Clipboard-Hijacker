using CryptoClipper.Models;
using CryptoClipper.Utils;

namespace CryptoClipper.Wallets;

public sealed class EthereumMatcher : IWalletPattern
{
    public CoinType CoinType => CoinType.Ethereum;

    public bool IsMatch(string address) =>
        RegexPatterns.Ethereum().IsMatch(address);
}
