// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------

using JetBrains.Annotations;
using StreamerBot.AnnaSasDev.Services;

namespace StreamerBot.AnnaSasDev;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class OnDeathAddActionExtension {
    [UsedImplicitly]
    public static bool ExecuteDeathAddActionCommand(this IStreamerBotUtilities utilities) {
        // ReSharper disable once ConvertIfStatementToReturnStatement
        if (!utilities.Arguments.TryGetActionArg(ActionArguments.GameId, out string? gameId)) throw new Exception("Could not find gameId");
        if (!GameDeathsProvider.TryAddDeath(gameId)) throw new Exception($"Could not add deaths to {gameId}");
        return true;
    }
}
