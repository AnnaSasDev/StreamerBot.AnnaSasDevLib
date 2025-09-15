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
    public static bool ExecuteOnTwitchSubscription(this IStreamerBotUtilities utilities)
        => SubscriberUpdater.UpdateGlobalSubscriberGoal(utilities)
           && SubscriberUpdater.UpdateNewSubscriberText(utilities)
           && utilities.Notification.TryPublishNotification("newSubscriber")
           && utilities.Notification.TryUnPublishNotification();
}