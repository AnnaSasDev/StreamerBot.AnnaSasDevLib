// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Streamer.bot.Plugin.Interface;

namespace StreamerBot.AnnaSasDev.Services.OBS;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class FollowerGoalService {
    public const long DefaultGoalAmount = 5L;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static bool TryUpdateObsSource() {
        if (CphService.TryGetCph(out IInlineInvokeProxy? cph) == false) return ErrorMessageService.AddErrorMessage("Could not find the CPH");
        
        // Get values
        if (!CphService.TryGetGlobalNonPersistedVar("DailyFollowerValue", out long followerValue)) return ErrorMessageService.AddErrorMessage("Value could not be retrieved");
        if (!CphService.TryGetGlobalVar("DailyFollowerGoal", out long followerGoal )) return ErrorMessageService.AddErrorMessage("Goal could not be retrieved");

        // Apply to OBS
        string followerGoalText = $"{followerValue}/{followerGoal}";
        cph.SetGlobalVar("DailyFollowerText", followerGoalText);
        cph.ObsSetGdiText("Backend-Overlay-Blank", "FollowerGoal.Text", followerGoalText);
        return true;
    }
}
