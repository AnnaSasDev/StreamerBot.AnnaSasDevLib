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
// ReSharper disable once AutoPropertyCanBeMadeGetOnly.Global
[UsedImplicitly]
public class NotificationData {
    [JsonPropertyName("templates")] public Dictionary<string, NotificationTemplate> Templates { get; set; } = new();
}