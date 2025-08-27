// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Text.Json.Serialization;
using JetBrains.Annotations;

namespace StreamerBot.AnnaSasDev.Utilities;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
// ReSharper disable once CollectionNeverUpdated.Global
// ReSharper disable twice AutoPropertyCanBeMadeGetOnly.Global
[UsedImplicitly]
public class SoundData {
    [JsonPropertyName("rootFolder")] public string RootFolder { get; set; } = string.Empty;
    [JsonPropertyName("sounds")] public Dictionary<string, string> Sounds { get; set; } = new();
}