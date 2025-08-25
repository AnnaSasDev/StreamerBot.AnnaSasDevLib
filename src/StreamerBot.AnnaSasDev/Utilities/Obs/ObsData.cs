// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Text.Json.Serialization;

namespace StreamerBot.AnnaSasDev.Utilities;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
// ReSharper disable twice AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable once ClassNeverInstantiated.Global
// ReSharper disable once CollectionNeverUpdated.Global
public class ObsData {
    [JsonPropertyName("sceneNames")] public string[] SceneNames { get; set; } = Array.Empty<string>();
    [JsonPropertyName("sourceReferences")] public Dictionary<string, ObsSourceReference> SourceReferences { get; set; } = new();
}

// ReSharper disable twice AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable once ClassNeverInstantiated.Global
public class ObsSourceReference {
    [JsonPropertyName("scene")] public string SceneName { get; set; } = string.Empty;
    [JsonPropertyName("source")] public string SourceName { get; set; } = string.Empty;
}