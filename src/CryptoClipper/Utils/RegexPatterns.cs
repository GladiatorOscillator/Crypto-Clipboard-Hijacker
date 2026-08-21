using System.Text.RegularExpressions;

namespace CryptoClipper.Utils;

public static partial class RegexPatterns
{
    [GeneratedRegex(@"^[13][a-km-zA-HJ-NP-Z1-9]{25,34}$", RegexOptions.Compiled)]
    public static partial Regex BitcoinLegacy();

    [GeneratedRegex(@"^3[a-km-zA-HJ-NP-Z1-9]{25,34}$", RegexOptions.Compiled)]
    public static partial Regex BitcoinSegwit();

    [GeneratedRegex(@"^bc1[a-zA-HJ-NP-Z0-9]{25,62}$", RegexOptions.Compiled)]
    public static partial Regex BitcoinBech32();

    [GeneratedRegex(@"^0x[0-9a-fA-F]{40}$", RegexOptions.Compiled)]
    public static partial Regex Ethereum();

    [GeneratedRegex(@"^4[0-9AB][1-9A-HJ-NP-Za-km-z]{93}$", RegexOptions.Compiled)]
    public static partial Regex Monero();

    [GeneratedRegex(@"^[1-9A-HJ-NP-Za-km-z]{32,44}$", RegexOptions.Compiled)]
    public static partial Regex Solana();

    [GeneratedRegex(@"^T[1-9A-HJ-NP-Za-km-z]{33}$", RegexOptions.Compiled)]
    public static partial Regex Tron();

    [GeneratedRegex(@"^(ltc1|[LM3])[a-km-zA-HJ-NP-Z1-9]{25,62}$", RegexOptions.Compiled)]
    public static partial Regex Litecoin();
}
