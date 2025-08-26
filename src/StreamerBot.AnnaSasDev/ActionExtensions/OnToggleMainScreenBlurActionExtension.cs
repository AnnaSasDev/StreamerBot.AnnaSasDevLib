// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace StreamerBot.AnnaSasDev;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
// ReSharper disable once UnusedType.Global
public static class OnToggleMainScreenBlurActionExtension {
    // ReSharper disable once UnusedMember.Global
    public static bool ExecuteOnToggleMainScreenBlur(this IStreamerBotUtilities utilities) {
        if (!utilities.Obs.TryGetSourceReference(SourceReferenceId.AssetScreenMain, out string? sceneName, out string? sourceName)) throw new Exception("Could not find the source reference.");
        
        utilities.InlineInvokeProxy.ObsToggleFilter(sceneName, sourceName, "HexagonBlur");
        return true;
    }
}