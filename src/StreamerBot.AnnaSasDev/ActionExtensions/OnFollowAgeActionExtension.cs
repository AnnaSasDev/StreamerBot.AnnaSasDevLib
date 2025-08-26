// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------

namespace StreamerBot.AnnaSasDev;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
// ReSharper disable once UnusedType.Global
public static class OnFollowAgeActionExtension {
    // ReSharper disable once UnusedMember.Global
    public static bool ExecuteFollowAgeActionCommand(this IStreamerBotUtilities utilities) {
        if (!utilities.Arguments.TryGetActionArg(ActionArguments.BroadcastUser, out string? broadcaster)) throw new Exception("Could not find broadcast user");
        if (!utilities.ChatInput.TryParseUserInput(out var input)) throw new Exception("Could not parse user input");
        
        string? targetUserName = input.FirstOrDefault();
        utilities.Arguments.TryGetActionArg(ActionArguments.User, out string? invokedUserName);
        string target = targetUserName ?? invokedUserName ?? string.Empty;
        
        if (string.IsNullOrWhiteSpace(target)) {
            utilities.Messages.TrySendMessageContextAware("Could not find a username to target for the FollowAge command.");
            return true;
        }

        if (string.Equals(broadcaster, target, StringComparison.OrdinalIgnoreCase)) {
            utilities.Messages.TrySendMessageContextAware("A broadcaster can't follow themselves and their follow age is not tracked.");
            return true;       
        }

        if (!utilities.Arguments.TryGetActionArg(ActionArguments.IsFollowing, out bool isFollowing) || !isFollowing) {
            utilities.Messages.TrySendMessageContextAware("Could not find if the user is following the broadcaster.");
            return true;       
        }

        // ReSharper disable once InvertIf
        if (!utilities.Arguments.TryGetActionArg(ActionArguments.FollowAgeLong, out string? followAge)) {
            utilities.Messages.TrySendMessageContextAware($"Could not find the follow age of user '{target}'.");
            return true;       
        }

        return utilities.Messages.TrySendMessageContextAware($"@{target} has been following @{broadcaster} for {followAge}");
    }
}