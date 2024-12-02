// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Streamer.bot.Plugin.Interface;

namespace StreamerBot.AnnaSasDev.Services.OBS;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class BottomPanelService {
    public const string ObjectSubjectTextVarName = "CSharpTrackedArgument_ObsSubjectText";

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static bool TryUpdateFollowerPanel(IInlineInvokeProxy? cphEntryPoint = null) {
        if (cphEntryPoint is not null) CphService.SetCph(cphEntryPoint);
        if (CphService.TryGetCph(out IInlineInvokeProxy? cph) == false) {
            return CphService.SendFailureMessages("Could not find the CPH");
        }

        if (!CphService.TryGetArg("triggerName", out string? triggerName) || !triggerName.Equals("follow", StringComparison.OrdinalIgnoreCase)) {
            return CphService.SendFailureMessages("Could not find the follow value for triggerName");
        }

        if (!CphService.TryGetArg("userName", out string? followerName) || string.IsNullOrWhiteSpace(followerName)) {
            return CphService.SendFailureMessages("Could not find the follower name");
        }
        
        cph.ObsSetGdiText("Backend-Overlay-Blank", "text-follower", followerName);
        return CphService.SendFailureMessages();
    }

    public static bool TryUpdateSubscriberPanel(IInlineInvokeProxy? cphEntryPoint = null) {
        if (cphEntryPoint is not null) CphService.SetCph(cphEntryPoint);
        if (CphService.TryGetCph(out IInlineInvokeProxy? cph) == false) {
            return CphService.SendFailureMessages("Could not find the CPH");
        }

        if (!CphService.TryGetArg("triggerName", out string? triggerName) || !triggerName.Equals("subscription", StringComparison.OrdinalIgnoreCase)) {
            return CphService.SendFailureMessages("Could not find the follow value for triggerName");
        }

        if (!CphService.TryGetArg("userName", out string? subscriberName) || string.IsNullOrWhiteSpace(subscriberName)) {
            return CphService.SendFailureMessages("Could not find the follower name");
        }

        cph.ObsSetGdiText("Backend-Overlay-Blank", "text-subscriber", subscriberName);
        return CphService.SendFailureMessages();
    }

    public static bool TryUpdateSubjectPanel(IInlineInvokeProxy? cphEntryPoint = null) {
        if (cphEntryPoint is not null) CphService.SetCph(cphEntryPoint);

        if (!CphService.TryGetCph(out IInlineInvokeProxy? cph)) return ErrorMessageService.AddErrorMessage("Could not find the CPH");
        if (!CphService.TryGetGlobalVar(ObjectSubjectTextVarName, out string? subjectText)) return ErrorMessageService.AddErrorMessage("Could not find the subject text");

        cph.ObsSetGdiText("Backend-Overlay-Blank", "text-subject", subjectText);
        return true;
    }
}
