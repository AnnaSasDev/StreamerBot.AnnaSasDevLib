// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------

using StreamerBot.AnnaSasDev.Services;

namespace StreamerBot.AnnaSasDev;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
// ReSharper disable once UnusedType.Global
public static class OnTwitchFollowActionExtension {
    // ReSharper disable once UnusedMember.Global
    public static bool ExecuteOnTwitchFollow(this IStreamerBotUtilities utilities)
        => FollowUpdater.UpdateDailyFollowerGoal(utilities)
           && FollowUpdater.UpdateGlobalFollowerGoal(utilities);
}