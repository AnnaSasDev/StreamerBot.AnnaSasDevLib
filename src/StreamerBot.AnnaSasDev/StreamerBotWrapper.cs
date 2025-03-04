// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Streamer.bot.Plugin.Interface;
using StreamerBot.AnnaSasDev.Services;
using System.Diagnostics.CodeAnalysis;

namespace StreamerBot.AnnaSasDev;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class StreamerBotWrapper(IInlineInvokeProxy cph) {
    public IInlineInvokeProxy Cph { get; } = cph;
    public readonly ErrorMessageService ErrorMessages = new();

    private FollowerGoalService? _followerGoalService;
    public FollowerGoalService FollowerGoalService => _followerGoalService ??= new FollowerGoalService(this);

    private ObsSpecifiedService? _obsSpecifiedService;
    public ObsSpecifiedService ObsSpecifiedService => _obsSpecifiedService ??= new ObsSpecifiedService(this);
    
    private GameDeathsService? _gameDeathsService;
    public GameDeathsService GameDeathsService => _gameDeathsService ??= new GameDeathsService(this);

    // -----------------------------------------------------------------------------------------------------------------
    // Code
    // -----------------------------------------------------------------------------------------------------------------
    public bool TrySendMessage(string message) {

        TryGetArg("commandSource", out string? commandSource);

        switch (commandSource?.ToLowerInvariant()) {
            case null:// If not a command, do some normal behaviour and return
            case "twitch": {
                Cph.SendMessage(message);
                return true;
            }

            case "youtube": {
                Cph.SendYouTubeMessage(message);
                return true;
            }

            default: {
                ErrorMessages.Add("Could not find the commandSource argument'");
                return false;
            }
        }
    }

    public bool TrySendReply(string message) {
        TryGetArg("commandSource", out string? commandSource);

        switch (commandSource?.ToLowerInvariant()) {
            case null:// If not a command, do some normal behaviour and return
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
                ErrorMessages.Add("Could not find the commandSource argument'");
                return false;
            }
        }
    }

    public bool SendFailureMessages(string extraErrorMessage) {
        ErrorMessages.Add(extraErrorMessage);
        return SendFailureMessages();
    }

    public bool SendFailureMessages() {
        while (ErrorMessages.TryGet(out string? message)) {
            if (TrySendMessage(string.IsNullOrWhiteSpace(message)
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
    #region Argument manipulation
    public bool TryGetArg<T>(string argName, [NotNullWhen(true)] out T? value) {
        if (!Cph.TryGetArg(argName, out value)) return false;
        return value is not null;
    }

    public bool TrySetArg<T>(string argName, T value) {
        Cph.SetArgument(argName, value);
        return true;
    }

    public bool TryGetGlobalVar<T>(string varName, [NotNullWhen(true)] out T? value) {
        value = default;
        value = Cph.GetGlobalVar<T>(varName);
        return value is not null;
    }

    public bool TryGetGlobalNonPersistedVar<T>(string varName, [NotNullWhen(true)] out T? value) {
        value = default;

        value = Cph.GetGlobalVar<T>(varName, false);
        return value is not null;
    }
    
    public bool TrySetGlobalVar<T>(string varName, T value) {
        Cph.SetGlobalVar(varName, value, true);
        return true;
    }
    public bool TrySetGlobalNonPersistedVar<T>(string varName, T value) {
        Cph.SetGlobalVar(varName, value, false);
        return true;
    }
    
    #endregion
    
    #region User Input manipulation
    public bool TryGetUserInput(int index, [NotNullWhen(true)] out string? output)
        => TryGetArg($"{Defaults.UserInputPrefix}{index}", out output);

    private void SetUserInput(string value, int index) {
        Cph.SetArgument($"{Defaults.UserInputPrefix}{index}", value);
    }

    public bool TryParseUserInput() {
        if (!TryGetArg("rawInput", out string? rawInput)) {
            ErrorMessages.Add("Could not find the rawInput argument.");
            return false;
        }

        int foundArguments = 0;
        foreach (string inputVariable in rawInput.Split(' ')) {
            if (string.IsNullOrWhiteSpace(inputVariable)) continue;

            SetUserInput(inputVariable, foundArguments++);
        }

        Cph.SetArgument(Defaults.UserInputAmountOfArguments, foundArguments + 1); // Else the amount of arguments will be off by one.

        return true;
    }
    
    public int GetAmountOfUserInputArguments() =>
        TryGetArg(Defaults.UserInputAmountOfArguments, out int? amountOfArguments)
            ? (int)amountOfArguments
            : -1;

    public IEnumerable<string> GetUserInputArguments() {
        for (int i = 0; i < GetAmountOfUserInputArguments() - 1; i++) {
            if (!TryGetUserInput(i, out string? input)) continue;

            yield return input;
        }
    }
    #endregion
}
