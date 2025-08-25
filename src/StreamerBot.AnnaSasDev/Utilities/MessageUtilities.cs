// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------

namespace StreamerBot.AnnaSasDev.Utilities;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class MessageUtilities(IStreamerBotUtilities utilities) : IMessageUtilities {
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public bool TrySendMessage(string message, MessageTarget target) {
        switch (target) {
            case MessageTarget.Twitch: {
                utilities.InlineInvokeProxy.SendMessage(message);
                return true;
            }
            
            case MessageTarget.Youtube: {
                utilities.InlineInvokeProxy.SendYouTubeMessage(message);
                return true;
            }

            case MessageTarget.Unknown:
            default: {
                // TODO: think to log or something
                return false;
            }
        }
    }

    public bool TrySendMessageContextAware(string message) {
        if (!utilities.Arguments.TryGetActionArg(ActionArguments.CommandSource, out string? commandSource)) return false;
        if (!MessageTargetUtilities.TryParse(commandSource, out MessageTarget target)) return false;

        // ReSharper disable once InvertIf
        if (target.IsTwitch() && utilities.Arguments.TryGetActionArg(ActionArguments.MsgId, out string? messageId)) {
            utilities.InlineInvokeProxy.TwitchReplyToMessage(message, messageId);
            return true;
        }
        
        return TrySendMessage(message, target);
    }
}