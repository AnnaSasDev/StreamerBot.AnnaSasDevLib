// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Streamer.bot.Plugin.Interface;
using StreamerBot.AnnaSasDev.Services.OBS;

namespace StreamerBot.AnnaSasDev.Commands;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class CommandsSubject {
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static bool CommandEntryPoint(IInlineInvokeProxy cph) {
        // Always needed to run the various libraries that re in this assembly.
        CphService.SetCph(cph);

        if (!CphService.TryGetArg("rawInput", out string? rawInput))
            return CphService.SendFailureMessages("Could not find the rawInput argument.");

        if (!string.IsNullOrWhiteSpace(rawInput))
            cph.SetGlobalVar(BottomPanelService.ObjectSubjectTextVarName, rawInput);

        if (!BottomPanelService.TryUpdateSubjectPanel())
            return CphService.SendFailureMessages("Could not update the OBS source.");

        // Everything is nominal
        return true;
    }
}
