// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace StreamerBot.AnnaSasDev;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
// ReSharper disable once UnusedType.Global
public static class OnToggleCameraActionExtension {
    // ReSharper disable once UnusedMember.Global
    public static bool ExecuteOnToggleCamera(this IStreamerBotUtilities utilities)
        => utilities.Obs.TryToggleVisibility(SourceReferenceId.Camera);
}