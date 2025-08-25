// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------

namespace StreamerBot.AnnaSasDev;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
// ReSharper disable once UnusedType.Global
public static class SetSubjectActionExtension {
    // ReSharper disable once UnusedMember.Global
    // ReSharper disable once ConvertIfStatementToReturnStatement
    public static bool ExecuteSetSubjectActionCommand(this IStreamerBotUtilities utilities) {
        if (!utilities.ChatInput.TryGetUserInput(out string? input)) throw new Exception("Could not parse user input");
        if (string.IsNullOrWhiteSpace(input)) throw new Exception("Could not find the input argument.");
        
        if (!utilities.Obs.TryGetSourceReference(SourceReferenceId.SetSubjectAction, out string? sceneName, out string? sourceName)) throw new Exception("Could not find the source reference.");
        return utilities.Obs.TryUpdateTextSource(sceneName, sourceName, input);
    }
}