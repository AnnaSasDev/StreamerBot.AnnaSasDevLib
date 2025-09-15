// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------

using JetBrains.Annotations;
using StreamerBot.AnnaSasDev.Services;

namespace StreamerBot.AnnaSasDev;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class OnDeathResetActionExtension {
    [UsedImplicitly]
    public static bool ExecuteDeathResetActionCommand(this IStreamerBotUtilities utilities) {
        // ReSharper disable once ConvertIfStatementToReturnStatement
        if (!utilities.Arguments.TryGetActionArg(ActionArguments.GameId, out string? gameId)) throw new Exception("Could not find gameId");
        if (!GameDeathsProvider.TryResetDeaths(gameId)) throw new Exception($"Could not reset deaths of {gameId}");
        return true;
    }
}
