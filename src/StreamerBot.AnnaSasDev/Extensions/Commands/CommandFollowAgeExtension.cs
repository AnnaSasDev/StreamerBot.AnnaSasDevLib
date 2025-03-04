// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Diagnostics.CodeAnalysis;

namespace StreamerBot.AnnaSasDev;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[SuppressMessage("ReSharper", "UnusedMember.Global")]
public static class CommandFollowAgeExtension {
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static bool CommandFollowAge(this StreamerBotWrapper wrapper) {
        if (!wrapper.TryParseUserInput()) return wrapper.SendFailureMessages("Arguments could not be parsed for this command");
        
        wrapper.TryGetUserInput(0, out string? targetUser);
        wrapper.TryGetArg("userName", out string? invokedBy);
        if (targetUser is null && invokedBy is null) return wrapper.SendFailureMessages("Could not find the target user.");

        string target = (targetUser ?? invokedBy) ?? string.Empty;// One of these has to not null, see if check above.

        if (!wrapper.TryGetArg("broadcastUser", out string? broadcaster)) return wrapper.SendFailureMessages("Broadcaster could not be found.");
        if (string.Equals(broadcaster, target, StringComparison.OrdinalIgnoreCase)) return wrapper.TrySendMessage("A broadcaster can't follow themselves.");

        if (!wrapper.TryGetArg("isFollowing", out bool isFollowing) || isFollowing == false) return wrapper.TrySendMessage($"Could not find if the user '{target}' is following '{broadcaster}'");
        if (!wrapper.TryGetArg("followAgeLong", out string? followAge)) return wrapper.SendFailureMessages($"Could not find the follow age of '{target}'");
        
        return wrapper.TrySendReply($"@{target} has been following @{broadcaster} for {followAge}");
    }
}
