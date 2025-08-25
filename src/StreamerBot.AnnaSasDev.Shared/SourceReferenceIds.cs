// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace StreamerBot.AnnaSasDev;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public enum SourceReferenceId {
    SetSubjectAction,
    PanelGoals,
    PanelKofi,
    AssetScreenMain,
    Camera
}

public static class SourceReferenceIdUtilities {
    public static string ToId(this SourceReferenceId sourceReferenceId) => sourceReferenceId switch {
        SourceReferenceId.SetSubjectAction => "panelSubjectText",
        SourceReferenceId.PanelGoals => "panelGoals",
        SourceReferenceId.PanelKofi => "panelKofi",
        SourceReferenceId.AssetScreenMain => "assetScreenMain",
        SourceReferenceId.Camera => "camera",
        _ => throw new ArgumentOutOfRangeException(nameof(sourceReferenceId), sourceReferenceId, null)
    };
}