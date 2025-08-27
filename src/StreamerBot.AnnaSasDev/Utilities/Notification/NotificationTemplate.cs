// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Text.Json.Serialization;
using JetBrains.Annotations;

namespace StreamerBot.AnnaSasDev.Utilities;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[UsedImplicitly]
public class NotificationTemplate {
    [JsonPropertyName("messageTemplate")] public string Text { get; set; } = string.Empty;
    [JsonPropertyName("variableDuckyFileName")] public string VariableDuckyFileName { get; set; } = string.Empty;
    [JsonPropertyName("soundId")] public string SoundId { get; set; } = string.Empty;
}