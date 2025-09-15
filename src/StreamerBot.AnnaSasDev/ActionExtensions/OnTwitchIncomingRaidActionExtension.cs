// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using JetBrains.Annotations;

namespace StreamerBot.AnnaSasDev;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class OnTwitchIncomingRaidActionExtension {
    [UsedImplicitly]
    public static bool ExecuteOnTwitchIncomingRaid(this IStreamerBotUtilities utilities)
        => utilities.Notification.TryPublishNotification("newRaid")
           && utilities.Notification.TryUnPublishNotification();
}