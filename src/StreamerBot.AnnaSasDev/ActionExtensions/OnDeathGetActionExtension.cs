// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------

using JetBrains.Annotations;
using StreamerBot.AnnaSasDev.Services;

namespace StreamerBot.AnnaSasDev;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class OnDeathGetActionExtension {
    [UsedImplicitly]
    public static bool ExecuteDeathGetActionCommand(this IStreamerBotUtilities utilities) {
        // ReSharper disable once ConvertIfStatementToReturnStatement
        if (!utilities.Arguments.TryGetActionArg(ActionArguments.GameId, out string? gameId)) throw new Exception("Could not find gameId");
        if (!GameDeathsProvider.TryGetDeaths(gameId, out int deaths)) throw new Exception($"Could not add deaths to {gameId}");
        
        utilities.Messages.TrySendMessageContextAware($"Anna Died {deaths} times during this game already.");
        
        return true;
    }
}
