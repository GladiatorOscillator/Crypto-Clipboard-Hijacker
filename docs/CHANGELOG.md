# Changelog

All notable changes to this project will be documented in this file.

## [2.1.0] - 2025-01-15

### Added
- Solana (SOL) wallet address detection and replacement
- TRON (TRX) address support with Base58 validation
- Anti-sandbox detection for common analysis tools
- Process protection via critical process flag
- Task Scheduler persistence as alternative to registry

### Changed
- Migrated to .NET 9 with AOT compilation support
- Improved regex patterns using source generators
- Reduced clipboard polling interval to 300ms

### Fixed
- False positive on Ethereum addresses with mixed case checksums
- Memory leak in clipboard handle management
- Race condition in concurrent clipboard access

## [2.0.0] - 2024-09-20

### Added
- Litecoin (LTC) Bech32 address support
- Statistics reporting module
- Transaction logging with timestamp

### Changed
- Complete rewrite using Microsoft.Extensions.Hosting
- File-scoped namespaces throughout
- Nullable reference types enabled globally

### Removed
- Legacy .NET Framework support
- Windows Forms dependency

## [1.0.0] - 2024-05-10

### Added
- Initial release
- Bitcoin (BTC) Legacy, SegWit, and Bech32 support
- Ethereum (ETH) address detection
- Monero (XMR) address detection
- Registry-based persistence
- Single instance mutex
- Basic anti-debug checks
