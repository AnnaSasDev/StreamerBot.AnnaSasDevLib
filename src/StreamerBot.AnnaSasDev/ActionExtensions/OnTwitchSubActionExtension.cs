// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using JetBrains.Annotations;
using StreamerBot.AnnaSasDev.Services;

namespace StreamerBot.AnnaSasDev;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class OnTwitchSubActionExtension {
    [UsedImplicitly]
    public static bool ExecuteOnTwitchSubscription(this IStreamerBotUtilities utilities) {
        if (!SubscriberUpdater.UpdateGlobalSubscriberGoal(utilities)) return false;
        if (!SubscriberUpdater.UpdateNewSubscriberText(utilities)) return false;

        bool state = false;
        try {
            state = utilities.Notification.TryPublishNotification("newSubscriber");
        }
        finally {
            state |= utilities.Notification.TryUnPublishNotification();
        }
        return state;
    }
}