// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------

namespace StreamerBot.AnnaSasDev.Shared;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public enum MessageTarget {
    Unknown = 0,
    Twitch,
    Youtube,
}

public static class MessageTargetUtilities {
    private const string SourceTwitch = "twitch";
    private const string SourceYoutube = "youtube";

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static bool IsTwitch(this MessageTarget target) => target == MessageTarget.Twitch;
    public static bool IsYoutube(this MessageTarget target) => target == MessageTarget.Youtube;

    public static string ToStreamerBotMessageSource(this MessageTarget target) => target switch {
        MessageTarget.Twitch => SourceTwitch,
        MessageTarget.Youtube => SourceYoutube,
        MessageTarget.Unknown => throw new ArgumentOutOfRangeException(nameof(target), target, null),
        _ => throw new ArgumentOutOfRangeException(nameof(target), target, null)
    };

    public static bool TryParse(string? value, out MessageTarget target) {
        target = MessageTarget.Unknown;
        if (value is null) return false;

        target = value.ToLowerInvariant() switch {
            SourceTwitch => MessageTarget.Twitch,
            SourceYoutube => MessageTarget.Youtube,
            _ => MessageTarget.Unknown
        };
        return target != MessageTarget.Unknown;

    }
}