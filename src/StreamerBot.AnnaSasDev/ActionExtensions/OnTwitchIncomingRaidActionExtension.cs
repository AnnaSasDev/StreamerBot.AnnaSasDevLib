// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace StreamerBot.AnnaSasDev;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
// ReSharper disable once UnusedType.Global
public static class OnTwitchIncomingRaidActionExtension {
    // ReSharper disable once UnusedMember.Global
    public static bool ExecuteOnTwitchIncomingRaid(this IStreamerBotUtilities utilities) {
        if (!utilities.Notification.TryPublishNotification("newRaid")) return false;
        if (!utilities.Notification.TryUnPublishNotification()) return false;
        return true;
    }
}