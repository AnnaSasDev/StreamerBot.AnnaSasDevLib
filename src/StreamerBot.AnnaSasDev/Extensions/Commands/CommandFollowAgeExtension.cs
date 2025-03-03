// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace StreamerBot.AnnaSasDev;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class CommandFollowAgeExtension {
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static bool CommandFollowAge(this StreamerBotWrapper wrapper) {
        if (!wrapper.TryParseUserInput()) return wrapper.SendFailureReply("Arguments could not be parsed for this command");

        wrapper.TryGetUserInput(0, out string? targetUser);
        wrapper.TryGetArg("userName", out string? invokedBy);
        if (targetUser is null && invokedBy is null) return wrapper.SendFailureReply("Could not find the target user.");

        string target = (targetUser?.TrimStart('@') ?? invokedBy)!;// One of these has to not null, see if check above.

        if (!wrapper.TryGetArg("broadcastUser", out string? broadcaster)) return wrapper.SendFailureReply("Broadcaster could not be found.");
        if (string.Equals(broadcaster, target, StringComparison.OrdinalIgnoreCase)) return wrapper.TrySendMessage("A broadcaster can't follow themselves.");

        if (!wrapper.TryGetArg("isFollowing", out bool isFollowing) || isFollowing == false) return wrapper.TrySendMessage($"Could not find if the user '{target}' is following '{broadcaster}'");
        if (!wrapper.TryGetArg("followAgeSeconds", out int followAgeSeconds) || followAgeSeconds <= 0) return wrapper.SendFailureReply($"User '{target}' has not been following '{broadcaster}' for a long time.");
        if (!wrapper.TryGetArg("followAgeLong", out string? followAge)) return wrapper.SendFailureReply($"Could not find the follow age of '{target}'");

        return wrapper.TrySendReply($"@{target} has been following @{broadcaster} for {followAge}");
    }
}
