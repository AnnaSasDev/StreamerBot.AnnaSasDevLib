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
        if (CphService.TryGetCph(out IInlineInvokeProxy? cph) == false) return ErrorMessageService.AddErrorMessage("Could not find the CPH");

        return false;
    }
    
    public static bool TryUpdateSubscriberPanel(IInlineInvokeProxy? cphEntryPoint = null) {
        if (cphEntryPoint is not null) CphService.SetCph(cphEntryPoint);
        if (CphService.TryGetCph(out IInlineInvokeProxy? cph) == false) return ErrorMessageService.AddErrorMessage("Could not find the CPH");
        return false;
    }

    public static bool TryUpdateSubjectPanel(IInlineInvokeProxy? cphEntryPoint = null) {
        if (cphEntryPoint is not null) CphService.SetCph(cphEntryPoint);
        
        if (!CphService.TryGetCph(out IInlineInvokeProxy? cph)) return ErrorMessageService.AddErrorMessage("Could not find the CPH");
        if (!CphService.TryGetGlobalVar(ObjectSubjectTextVarName, out string? subjectText)) return ErrorMessageService.AddErrorMessage("Could not find the subject text");
        cph.ObsSetGdiText("Backend-Overlay-Blank", "text-subject", subjectText);
        return true;
    }
}
