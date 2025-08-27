// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using StreamerBot.AnnaSasDev.Services;

namespace StreamerBot.AnnaSasDev;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
// ReSharper disable once UnusedType.Global
public static class OnTwitchSubActionExtension {
    // ReSharper disable once UnusedMember.Global
    public static bool ExecuteOnTwitchSubscription(this IStreamerBotUtilities utilities) {
        if (!SubscriberUpdater.UpdateGlobalSubscriberGoal(utilities)) return false;
        if (!SubscriberUpdater.UpdateNewSubscriberText(utilities)) return false;
        
        if (!utilities.Notification.TryPublishNotification("newSubscriber")) return false;
        if (!utilities.Notification.TryUnPublishNotification()) return false;
        
        return true;
    }
}