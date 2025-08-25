// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace StreamerBot.AnnaSasDev;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
// ReSharper disable once CollectionNeverUpdated.Global
// ReSharper disable once ClassNeverInstantiated.Global
// ReSharper disable twice AutoPropertyCanBeMadeGetOnly.Global
public class GitActionData {
    [JsonPropertyName("aliases")] public HashSet<string> Aliases { get; set; } = new();
    [JsonPropertyName("message")] public string Message { get; set; } = string.Empty;
}