// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace StreamerBot.AnnaSasDev;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
// ReSharper disable once UnusedType.Global
public class OnTogglePanelsActionExtension {
    // ReSharper disable once UnusedMember.Global
    public static bool ExecuteOnTogglePanels(IStreamerBotUtilities utilities)
        => utilities.Obs.TryToggleVisibility(SourceReferenceId.PanelGoals)
           && utilities.Obs.TryToggleVisibility(SourceReferenceId.PanelKofi);
}