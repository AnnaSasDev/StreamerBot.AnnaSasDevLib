// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Streamer.bot.Plugin.Interface;

namespace StreamerBot.AnnaSasDev.Services;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class UpdateRewardCost {
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static bool Update(IInlineInvokeProxy cph) {
        // Always needed to run the various libraries that re in this assembly.
        CphService.SetCph(cph);
        
        if (!CphService.TryGetArg("rewardCost", out long? rewardCost)) return CphService.SendFailureMessages("Could not find the rawInput argument.");
        if (!CphService.TryGetGlobalVar("rewardCostIncrement", out long? increment)) return CphService.SendFailureMessages("Could not find the rewardCostIncrement global variable.");

        rewardCost += increment;
        
        cph.SetArgument("rewardCost", rewardCost);

        return true;
    }
}
