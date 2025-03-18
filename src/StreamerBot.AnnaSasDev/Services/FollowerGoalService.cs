// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Diagnostics.CodeAnalysis;

namespace StreamerBot.AnnaSasDev.Services;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class FollowerGoalService(StreamerBotWrapper wrapper) {
    public const long DefaultGoalAmount = 5L;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public bool UpdateFollowerGoal() {
        wrapper.TryGetGlobalVar("DailyFollowerValue", out long followerValue);// if it doesn't exist, it'll be 0
        followerValue += 1;

        // Set the value
        wrapper.TrySetGlobalVar("DailyFollowerValue", followerValue);
        if (!wrapper.ObsSpecifiedService.TryUpdateFollowerGoalObsSources()) return wrapper.SendFailureMessages("Could not update the OBS goal source.");
        if (!wrapper.ObsSpecifiedService.TryUpdateFollowerPanel()) return wrapper.SendFailureMessages("Could not update the OBS text source.");
        return true;
    }

    public bool ResetFollowerGoal() {
        long followerValue = wrapper.Cph.GetGlobalVar<long>("DailyFollowerValue", persisted:false);
        long followerGoal = wrapper.Cph.GetGlobalVar<long>("DailyFollowerGoal");
		
        // Increment the value
        followerValue = 0;
		
        // Store the text value so it can be used by OBS
        wrapper.Cph.SetGlobalVar("DailyFollowerValue", followerValue, persisted:false);
        wrapper.Cph.SetGlobalVar("DailyFollowerText", $"{followerValue}/{followerGoal}");
		
        // Apply value to OBS
        string followerText = wrapper.Cph.GetGlobalVar<string>("DailyFollowerText");
        wrapper.Cph.ObsSetGdiText("Backend-Overlay-Blank", "FollowerGoal.Text", followerText);
        
        // we're done here
        return true;
    }
}
