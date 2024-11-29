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
    public static bool EntryPoint(IInlineInvokeProxy cph) {
        // Always needed to run the various libraries that re in this assembly.
        CphService.SetCph(cph);
        CphService.TryGetGlobalNonPersistedVar("DailyFollowerValue", out long followerValue); // if it doesn't exist, it'll be 0
        followerValue += 1;
        
        // Set the value
        cph.SetGlobalVar("DailyFollowerValue", followerValue, false);
        if (!TryUpdateObsSource()) return CphService.SendFailureMessages("Could not update the OBS source.");

        // cph.TtsSpeak("Default", "Quack Quack Quack A new ducky in the pond");
        // cph.PlaySound(@"E:\bots\sounds\ducks.wav");
        cph.PlaySound(@"E:\bots\sounds\rubber_duck.mp3");
        return CphService.SendFailureMessages();
    }
    
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
