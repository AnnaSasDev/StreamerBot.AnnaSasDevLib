// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using StreamerBot.AnnaSasDev.Services;

namespace StreamerBot.AnnaSasDev;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
// ReSharper disable once UnusedType.Global
public static class GitActionExtension {
    private const string DataFileName = "gitActionExtensionData.json";
    private const string DefaultGitMessage = "Anna does lots of chaotic coding projects, most can be found at https://github.com/InfiniLore or https://github.com/code-of-chaos";

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    // ReSharper disable once UnusedMember.Global
    public static bool ExecuteGitActionCommand(this IStreamerBotUtilities utilities) {
        if (!utilities.ChatInput.TryParseUserInput(out var input)) throw new Exception("Could not parse user input");
        if (!AssetFileProvider.TryParseJsonAssetFile(DataFileName, out GitActionData[]? data)) throw new Exception("Could not parse asset file");

        string? aliasArgument = input.FirstOrDefault()?.ToLowerInvariant();
        if (aliasArgument is null) return utilities.Messages.TrySendMessageContextAware(DefaultGitMessage);
        GitActionData? gitData = data.FirstOrDefault(d => d.Aliases.Contains(aliasArgument));
        
        return utilities.Messages.TrySendMessageContextAware(gitData is null 
            ? $"A project by the name of '{aliasArgument}' could not be found. {DefaultGitMessage}" 
            : gitData.Message
        );
    }
}