// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------

using System.Text.Json;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;

namespace StreamerBot.AnnaSasDev.Services;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class AssetFileProvider {
    private const string AssetFolderPath = "dlls/.assets/";

    private static readonly JsonSerializerOptions JsonOptions = new() { PropertyNameCaseInsensitive = true };

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static bool TryGetAssetFile(string fileName, out string? fileContents) {
        fileContents = null;
        string fullPath = Path.Combine(AssetFolderPath, fileName);
        if (!File.Exists(fullPath)) return false;
        try {
            fileContents = File.ReadAllText(fullPath);
            return true;
        }
        catch (Exception) {
            return false;
        }
    }

    public static bool TryParseJsonAssetFile<T>(string fileName, [NotNullWhen(true)] out T? data) {
        data = default;

        string fullPath = Path.Combine(AssetFolderPath, fileName);
        if (!File.Exists(fullPath)) return false;

        try {
            data = JsonSerializer.Deserialize<T>(File.ReadAllText(fullPath), JsonOptions);
            return data is not null;
        }
        catch (Exception) {
            return false;
        }
    }

    public static bool TryGetAssetFileHash(string fileName, [NotNullWhen(true)] out string? hash) {
        hash = null;
        string fullPath = Path.Combine(AssetFolderPath, fileName);
        if (!File.Exists(fullPath)) return false;

        try {
            using var sha256 = SHA256.Create();
            using FileStream stream = File.OpenRead(fullPath);
            byte[] hashBytes = sha256.ComputeHash(stream);
            hash = BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
            return !string.IsNullOrWhiteSpace(hash);
        }
        catch (Exception) {
            return false;
        }
    }
    
    public static bool TrySaveAssetFile(string fileName, string fileContents) {
        string fullPath = Path.Combine(AssetFolderPath, fileName);
        try {
            File.WriteAllText(fullPath, fileContents);
            return true;
        }
        catch (Exception) {
            return false;
        }
    }

    public static bool TryWriteLineToAssetFile(string fileName, string line) {
        string fullPath = Path.Combine(AssetFolderPath, fileName);
        try {
            File.AppendAllText(fullPath, line + Environment.NewLine);
            return true;
        }
        catch (Exception) {
            return false;
        }

    }
    
    public static bool TrySaveJsonAssetFile<T>(string fileName, T data) {
        string json = JsonSerializer.Serialize(data, JsonOptions);
        return TrySaveAssetFile(fileName, json);
    }
}