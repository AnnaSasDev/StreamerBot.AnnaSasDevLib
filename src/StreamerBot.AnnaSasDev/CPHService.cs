// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Streamer.bot.Plugin.Interface;
using System.Diagnostics.CodeAnalysis;
namespace StreamerBot.AnnaSasDev;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class CphService {
    internal static IInlineInvokeProxy? Cph { get; set; }
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static bool TryGetCph([NotNullWhen(true)] out IInlineInvokeProxy? cph) {
        if ((cph = Cph) != null) return true;

        ErrorMessageService.AddErrorMessage("Could not find the CPH. This is most commonly solved by calling 'CphService.SetCph(cph)'");
        return false;
    }
    
    public static void SetCph(IInlineInvokeProxy cph) => Cph = cph;
    
    public static bool TrySendMessage(string message) {
        if (Cph is null) return false;
        TryGetArg("commandSource", out string? commandSource);
        
        switch (commandSource?.ToLowerInvariant()) {
            case null : // If not a command, do some normal behaviour and return
            case "twitch": {
                Cph.SendMessage(message);
                return true;
            }

            case "youtube": {
                Cph.SendYouTubeMessage(message);
                return true;
            }

            default: {
                ErrorMessageService.AddErrorMessage("Could not find the commandSource argument'");
                return false;
            }
        }
    }

    public static bool TrySendReply(string message) {
        if (Cph is null) return false;
        TryGetArg("commandSource", out string? commandSource);
        
        switch (commandSource?.ToLowerInvariant()) {
            case null : // If not a command, do some normal behaviour and return
            case "twitch": {
                TryGetArg("msgId", out string? msgId);
                Cph.TwitchReplyToMessage(message, msgId);
                return true;
            }

            case "youtube": {
                // YouTube doesn't support replies, so default to sending it as a normal message.
                Cph.SendYouTubeMessage(message);
                return true;
            }

            default: {
                ErrorMessageService.AddErrorMessage("Could not find the commandSource argument'");
                return false;
            }
        }
    }

    public static bool SendFailureReply(string extraErrorMessage) {
        ErrorMessageService.AddErrorMessage(extraErrorMessage);
        return SendFailureReply();
        
    }
    public static bool SendFailureReply() {
        while (ErrorMessageService.TryGetErrorMessage(out string? message)) {
            if(TrySendReply(string.IsNullOrWhiteSpace(message)
                    ? "Something went wrong without further information."
                    : $"ERROR : {message}"
                )) continue;
            
            // Something went wrong during sending of the error message
            // To ensure we don't cause an infinite loop we break here.
            break;
        }
        return true;
    }
    
    public static bool SendFailureMessages(string extraErrorMessage) {
        ErrorMessageService.AddErrorMessage(extraErrorMessage);
        return SendFailureMessages();
    }
    
    public static bool SendFailureMessages() {
        while (ErrorMessageService.TryGetErrorMessage(out string? message)) {
            if(TrySendMessage(string.IsNullOrWhiteSpace(message)
                ? "Something went wrong without further information."
                : $"ERROR : {message}"
            )) continue;
            
            // Something went wrong during sending of the error message
            // To ensure we don't cause an infinite loop we break here.
            break;
        }
        return true;
    }
    
    // -----------------------------------------------------------------------------------------------------------------
    // IInlineInvokeProxy overloads 
    // -----------------------------------------------------------------------------------------------------------------
    public static bool TryGetArg<T>(string argName, [NotNullWhen(true)] out T? value) {
        value = default;
        if (!TryGetCph(out IInlineInvokeProxy? cph)) return false;
        if (!cph.TryGetArg(argName, out value)) return false;
        return value is not null;
    }

    public static bool TryGetGlobalVar<T>(string varName, [NotNullWhen(true)] out T? value) {
        value = default;
        if (!TryGetCph(out IInlineInvokeProxy? cph)) return false;
        value = cph.GetGlobalVar<T>(varName);
        return value is not null;
    }

    public static bool TryGetGlobalNonPersistedVar<T>(string varName, [NotNullWhen(true)] out T? value) {
        value = default;
        if (!TryGetCph(out IInlineInvokeProxy? cph)) return false;
        value = cph.GetGlobalVar<T>(varName, false);
        return value is not null;
    }
}
