// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;

namespace StreamerBot.AnnaSasDev.Utilities;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class ChatInputUtilities(IStreamerBotUtilities utilities) : IChatInputUtilities {
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public bool TryGetUserInput([NotNullWhen(true)] out string? input) {
        return utilities.Arguments.TryGetActionArg(ActionArguments.RawInput, out input);
    }

    public bool TryParseUserInputToActionVariables() => TryParseUserInput(out _);
    public bool TryParseUserInput([NotNullWhen(true)] out IEnumerable<string>? parsedInput) {
        if (!TryGetUserInput(out string? rawInput)) {
            parsedInput = null;
            return false;
        }

        var foundArguments = 0;
        var parsedArguments = new List<string>();
        
        // TODO: This is a bit of a hack, we should use a REGEX to parse the input so "... ..." is not seen as two arguments.
        foreach (string inputVariable in rawInput.Split(' ')) { 
            if (string.IsNullOrWhiteSpace(inputVariable)) continue;
            parsedArguments.Add(inputVariable);
            utilities.Arguments.TrySetActionArg(ActionArguments.RawInputParsedAtIndex(foundArguments++), inputVariable);
        }
        
        parsedInput = parsedArguments;
        utilities.Arguments.TrySetActionArg(ActionArguments.RawInputParsedCount, foundArguments ); 
        return true;
    }
}