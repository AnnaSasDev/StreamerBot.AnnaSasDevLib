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
    Camera,
    TextGoalFollower,
    TextGoalSubscriber,
    TextFollowerGoalDaily,
}

public static class SourceReferenceIdUtilities {
    public static string ToId(this SourceReferenceId sourceReferenceId) => sourceReferenceId switch {
        SourceReferenceId.SetSubjectAction => "panelSubjectText",
        SourceReferenceId.PanelGoals => "panelGoals",
        SourceReferenceId.PanelKofi => "panelKofi",
        SourceReferenceId.AssetScreenMain => "assetScreenMain",
        SourceReferenceId.Camera => "camera",
        SourceReferenceId.TextGoalFollower => "textGoalFollower",
        SourceReferenceId.TextGoalSubscriber => "textGoalSubscriber",
        SourceReferenceId.TextFollowerGoalDaily => "textFollowerGoalDaily",
        _ => throw new ArgumentOutOfRangeException(nameof(sourceReferenceId), sourceReferenceId, null)
    };
}