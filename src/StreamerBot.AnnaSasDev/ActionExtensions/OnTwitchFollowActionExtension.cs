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
    public static bool ExecuteOnTwitchFollow(this IStreamerBotUtilities utilities) {
        if (!FollowUpdater.UpdateDailyFollowerGoal(utilities)) return false;
        if (!FollowUpdater.UpdateGlobalFollowerGoal(utilities)) return false;
        if (!FollowUpdater.UpdateNewFollowerText(utilities)) return false;
        
        if (!utilities.Notification.TryPublishNotification("newFollower")) return false;
        if (!utilities.Sound.TryPlaySound(SoundIds.NewFollower)) return false;
        if (!utilities.Notification.TryUnPublishNotification()) return false;
        
        return true;
    }
}