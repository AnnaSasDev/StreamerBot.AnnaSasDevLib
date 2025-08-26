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
    public static bool ExecuteOnTwitchSubscription(this IStreamerBotUtilities utilities)
        => SubscriberUpdater.UpdateGlobalSubscriberGoal(utilities)
           && SubscriberUpdater.UpdateNeSubscriberText(utilities);
}