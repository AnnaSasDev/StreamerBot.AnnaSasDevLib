// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Diagnostics.CodeAnalysis;

namespace StreamerBot.AnnaSasDev;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[SuppressMessage("ReSharper", "UnusedMember.Global")]
public static class UpdateRewardCostExtension {
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static bool UpdateRewardCost(this StreamerBotWrapper wrapper) {
        if (!wrapper.TryGetArg("rewardCost", out long? rewardCost)) return wrapper.SendFailureMessages("Could not find the rawInput argument.");
        if (!wrapper.TryGetGlobalVar("rewardCostIncrement", out long? increment)) return wrapper.SendFailureMessages("Could not find the rewardCostIncrement global variable.");

        rewardCost += increment;
        return wrapper.TrySetArg("rewardCost", rewardCost);
    }
}
