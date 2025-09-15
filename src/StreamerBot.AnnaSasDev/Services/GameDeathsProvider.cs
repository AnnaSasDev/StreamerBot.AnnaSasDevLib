// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;

namespace StreamerBot.AnnaSasDev.Services;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class GameDeathsProvider {
    private const string DataFileName = "deathData.json";

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    private static bool TryGetDeaths([NotNullWhen(true)] out Dictionary<string, int>? deaths)
        => AssetFileProvider.TryParseJsonAssetFile(DataFileName, out deaths);

    private static bool TrySaveDeaths(Dictionary<string, int> deaths) 
        => AssetFileProvider.TrySaveJsonAssetFile(DataFileName, deaths);

    public static bool TryAddDeath(string categoryName, int value = 1) {
        if (!TryGetDeaths(out Dictionary<string, int>? deaths)) return false;
        if (!deaths.TryGetValue(categoryName, out int currentDeaths)) deaths.Add(categoryName, 0);
        deaths[categoryName] = Math.Max(currentDeaths + value, 0);
        return TrySaveDeaths(deaths);
    }

    public static bool TryGetDeaths(string categoryName, out int deaths) {
        deaths = 0;
        return TryGetDeaths(out Dictionary<string, int>? deathsData)
            && deathsData.TryGetValue(categoryName, out deaths);
    }

    public static bool TryResetDeaths(string categoryName) {
        if (!TryGetDeaths(out Dictionary<string, int>? deaths)) {
            var data = new Dictionary<string, int> {
                [categoryName] = 0
            };
            return TrySaveDeaths(data);
        }

        if (!deaths.TryGetValue(categoryName, out int _)) {
            deaths.Add(categoryName, 0);
        } else {
            deaths[categoryName] = 0;
        }
        return TrySaveDeaths(deaths);
    }
}