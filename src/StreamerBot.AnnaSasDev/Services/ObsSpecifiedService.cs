// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Diagnostics.CodeAnalysis;

namespace StreamerBot.AnnaSasDev.Services;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class ObsSpecifiedService(StreamerBotWrapper wrapper) {
    public const string ObjectSubjectTextVarName = "CSharpTrackedArgument_ObsSubjectText";

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public bool TryUpdateFollowerPanel() {
        if (!wrapper.TryGetArg("triggerName", out string? triggerName) || !triggerName.Equals("follow", StringComparison.OrdinalIgnoreCase)) {
            return wrapper.SendFailureMessages("Could not find the follow value for triggerName");
        }

        if (!wrapper.TryGetArg("userName", out string? followerName) || string.IsNullOrWhiteSpace(followerName)) {
            return wrapper.SendFailureMessages("Could not find the follower name");
        }
        
        wrapper.Cph.ObsSetGdiText("Backend-Overlay-Blank", "text-follower", followerName);
        return wrapper.SendFailureMessages();
    }

    public bool TryUpdateSubscriberPanel() {
        if (!wrapper.TryGetArg("triggerName", out string? triggerName) || !triggerName.Equals("subscription", StringComparison.OrdinalIgnoreCase)) {
            return wrapper.SendFailureMessages("Could not find the follow value for triggerName");
        }

        if (!wrapper.TryGetArg("userName", out string? subscriberName) || string.IsNullOrWhiteSpace(subscriberName)) {
            return wrapper.SendFailureMessages("Could not find the follower name");
        }

        wrapper.Cph.ObsSetGdiText("Backend-Overlay-Blank", "text-subscriber", subscriberName);
        return wrapper.SendFailureMessages();
    }

    public bool TryUpdateSubjectPanel() {
        if (!wrapper.TryGetGlobalVar(ObjectSubjectTextVarName, out string? subjectText)) return wrapper.ErrorMessages.Add("Could not find the subject text");

        wrapper.Cph.ObsSetGdiText("Backend-Overlay-Blank", "text-subject", subjectText);
        return true;
    }
    
    public bool TryToggleBrb() {
        string? sceneName = wrapper.Cph.ObsGetCurrentScene();
        if (sceneName is null) return wrapper.SendFailureMessages("Could not find the current scene");
        
        string[] possibleScenes = ["Screen - Main", "Game - Stream"];
        if (!possibleScenes.Contains(sceneName)) return wrapper.SendFailureMessages("Could not toggle brb on the current scene");

        foreach (string scene in possibleScenes) {
            bool brbVisible = wrapper.Cph.ObsIsSourceVisible(scene, "brb");
            wrapper.Cph.ObsSetSourceVisibility(scene, "brb", !brbVisible);
        
            bool cameraVisible = wrapper.Cph.ObsIsSourceVisible(scene, "cam - Nvidea Broadcast");
            wrapper.Cph.ObsSetSourceVisibility(scene, "cam - Nvidea Broadcast", !cameraVisible);
        }
        return true;
    } 
    
    public bool TryUpdateFollowerGoalObsSources() {
        // Get values
        if (!wrapper.TryGetGlobalNonPersistedVar("DailyFollowerValue", out long followerValue)) return wrapper.ErrorMessages.Add("Value could not be retrieved");
        if (!wrapper.TryGetGlobalVar("DailyFollowerGoal", out long followerGoal)) return wrapper.ErrorMessages.Add("Goal could not be retrieved");

        // Apply to OBS
        string followerGoalText = $"{followerValue}/{followerGoal}";
        wrapper.TrySetGlobalVar("DailyFollowerText", followerGoalText);
        wrapper.Cph.ObsSetGdiText("Backend-Overlay-Blank", "FollowerGoal.Text", followerGoalText);
        return true;
    }
}
