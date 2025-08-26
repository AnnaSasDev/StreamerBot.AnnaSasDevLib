// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------

using StreamerBot.AnnaSasDev.Services;

namespace StreamerBot.AnnaSasDev;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
// ReSharper disable once UnusedType.Global
public static class OnStreamStartActionExtension {
    // ReSharper disable once UnusedMember.Global
    public static bool ExecuteOnStreamStart(this IStreamerBotUtilities utilities)
        => FollowUpdater.UpdateDailyFollowerGoal(utilities, 0) 
           && FollowUpdater.UpdateGlobalFollowerGoal(utilities)
           && SubscriberUpdater.UpdateGlobalSubscriberGoal(utilities);
}