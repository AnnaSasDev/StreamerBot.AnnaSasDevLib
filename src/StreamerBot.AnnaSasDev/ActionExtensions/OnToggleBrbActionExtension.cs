// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------

namespace StreamerBot.AnnaSasDev;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
// ReSharper disable once UnusedType.Global
public static class OnToggleBrbActionExtension {
    // ReSharper disable once UnusedMember.Global
    public static bool ExecuteOnToggleBrb(this IStreamerBotUtilities utilities)
        => utilities.Obs.TryToggleVisibility(SourceReferenceId.Camera)
           && utilities.Obs.TryToggleVisibility(SourceReferenceId.AssetBrb);
}