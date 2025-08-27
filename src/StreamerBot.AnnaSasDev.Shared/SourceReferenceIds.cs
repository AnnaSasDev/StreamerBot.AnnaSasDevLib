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
    TextFollower,
    TextSubscriber,
    AssetBrb,
    Notification,
    NotificationText,
    NotificationDucky,
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
        SourceReferenceId.TextFollower => "textFollower",
        SourceReferenceId.TextSubscriber => "textSubscriber",
        SourceReferenceId.AssetBrb => "assetBrb",
        SourceReferenceId.Notification => "notification",
        SourceReferenceId.NotificationText => "notificationText",
        SourceReferenceId.NotificationDucky => "notificationDucky",
        _ => throw new ArgumentOutOfRangeException(nameof(sourceReferenceId), sourceReferenceId, null)
    };
}