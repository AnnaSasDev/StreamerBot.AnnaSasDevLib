// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------

using JetBrains.Annotations;
using StreamerBot.AnnaSasDev.Services;

namespace StreamerBot.AnnaSasDev;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class OnTwitchFollowActionExtension {
    [UsedImplicitly]
    public static bool ExecuteOnTwitchFollow(this IStreamerBotUtilities utilities)
        => FollowUpdater.UpdateDailyFollowerGoal(utilities)
           && FollowUpdater.UpdateGlobalFollowerGoal(utilities)
           && FollowUpdater.UpdateNewFollowerText(utilities)
           && utilities.Notification.TryPublishNotification("newFollower") 
           && utilities.Notification.TryUnPublishNotification();
}