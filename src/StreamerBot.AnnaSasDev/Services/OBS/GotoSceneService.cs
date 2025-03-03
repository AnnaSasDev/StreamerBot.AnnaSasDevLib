// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Streamer.bot.Plugin.Interface;
using System.Diagnostics.CodeAnalysis;

namespace StreamerBot.AnnaSasDev.Services.OBS;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class GotoSceneService {
    public static bool TryToggleBrb(IInlineInvokeProxy? cphEntryPoint = null) {
        if (cphEntryPoint is not null) CphService.SetCph(cphEntryPoint);
        if (CphService.TryGetCph(out IInlineInvokeProxy? cph) == false) {
            return CphService.SendFailureMessages("Could not find the CPH");
        }

        string? sceneName = cph.ObsGetCurrentScene();
        if (sceneName is null) return CphService.SendFailureMessages("Could not find the current scene");
        
        string[] possibleScenes = ["Screen - Main", "Game - Stream"];
        if (!possibleScenes.Contains(sceneName)) return CphService.SendFailureMessages("Could not toggle brb on the current scene");
        
        bool brbVisible = cph.ObsIsSourceVisible(sceneName, "brb");
        cph.ObsSetSourceVisibility(sceneName, "brb", !brbVisible);
        
        bool cameraVisible = cph.ObsIsSourceVisible(sceneName, "cam - Nvidea Broadcast");
        cph.ObsSetSourceVisibility(sceneName, "cam - Nvidea Broadcast", !cameraVisible);
        return true;
    } 
}
