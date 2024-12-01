// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Streamer.bot.Plugin.Interface;

namespace StreamerBot.AnnaSasDev.Commands;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class CommandsFollowAge {
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static bool CommandEntryPoint(IInlineInvokeProxy cph) {
        // Always needed to run the various libraries that re in this assembly.
        CphService.SetCph(cph);

        if (!InputParsingService.TryParseInput()) return CphService.SendFailureReply("Arguments could not be parsed for this command");

        InputParsingService.TryGetInput(0, out string? targetUser);
        CphService.TryGetArg("userName", out string? invokedBy);
        if (targetUser is null && invokedBy is null) return CphService.SendFailureReply("Could not find the target user.");

        string target = (targetUser?.TrimStart('@') ?? invokedBy)!;// One of these has to not null, see if check above.

        if (!CphService.TryGetArg("broadcastUser", out string? broadcaster)) return CphService.SendFailureReply("Broadcaster could not be found.");
        if (string.Equals(broadcaster, target, StringComparison.OrdinalIgnoreCase)) return CphService.TrySendMessage("A broadcaster can't follow themselves.");

        if (!CphService.TryGetArg("isFollowing", out bool isFollowing) || isFollowing == false) return CphService.TrySendMessage($"Could not find if the user '{target}' is following '{broadcaster}'");
        if (!CphService.TryGetArg("followAgeSeconds", out int followAgeSeconds) || followAgeSeconds <= 0) return CphService.SendFailureReply($"User '{target}' has not been following '{broadcaster}' for a long time.");
        if (!CphService.TryGetArg("followAgeLong", out string? followAge)) return CphService.SendFailureReply($"Could not find the follow age of '{target}'");

        return CphService.TrySendReply($"@{target} has been following @{broadcaster} for {followAge}");
    }
}
