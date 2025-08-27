// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------

using StreamerBot.AnnaSasDev.Services;

namespace StreamerBot.AnnaSasDev.Utilities;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class NotificationUtilities(IStreamerBotUtilities utilities, ILoggingProvider logging) : INotificationUtilities {
    private const string DataFileName = "notificationData.json";

    private Lazy<NotificationData?> LazyNotificationData { get; } = new(static () =>
        AssetFileProvider.TryParseJsonAssetFile(DataFileName, out NotificationData? data)
            ? data
            : null
    );

    private NotificationData? NotificationData => LazyNotificationData.Value;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public bool TryPublishNotification(string notificationId) {
        if (NotificationData is null) {
            return logging.WarningAsFalse("Could not load {0} file", DataFileName);
        }

        if (!NotificationData.Templates.TryGetValue(notificationId, out NotificationTemplate? template)) {
            return logging.WarningAsFalse("Could not find notification id {0}", notificationId);
        }

        if (!TryUpdateNotificationText(template.Text, out string filledTemplate)) {
            return logging.WarningAsFalse("Could not fill out notification text template");
        }

        if (!utilities.Obs.TryUpdateTextSource(SourceReferenceId.NotificationText, filledTemplate)) {
            return logging.WarningAsFalse("Could not update notification text");
        }
        if (!utilities.Obs.TrySetImageSource(SourceReferenceId.NotificationDucky, template.VariableDuckyFileName)) {
            return logging.WarningAsFalse("Could not update notification ducky");
        }

        if (!utilities.Obs.TrySetVisibility(SourceReferenceId.Notification, true)) {
            return logging.WarningAsFalse("Could not update notification visibility");
        }

        return true;
    }

    public bool TryUnPublishNotification() => utilities.Obs.TrySetVisibility(SourceReferenceId.Notification, false);

    private bool TryUpdateNotificationText(string template, out string filled) {
        filled = template;

        try {
            // Gather arguments
            utilities.Arguments.TryGetActionArg(ActionArguments.User, out string? userName);
            utilities.Arguments.TryGetActionArg(ActionArguments.Tier, out string? tier);
            
            // Fill out template
            filled = template
                .Replace("{user}", userName)
                .Replace("{tier}", tier);
            return true;
        }
        catch (Exception e) {
            logging.Error(e, "Could not update notification text");
            return false;
        }
    }
}