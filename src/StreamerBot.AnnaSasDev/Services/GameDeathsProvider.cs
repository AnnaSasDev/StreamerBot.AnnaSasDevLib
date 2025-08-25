// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;

namespace StreamerBot.AnnaSasDev.Services;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class GameDeathsProvider {
    private const string DataFileName = "deathsData.json";

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static bool TryGetDeaths([NotNullWhen(true)] out Dictionary<string, int>? deaths)
        => AssetFileProvider.TryParseJsonAssetFile(DataFileName, out deaths);

    public static bool TrySaveDeaths(Dictionary<string, int> deaths) 
        => AssetFileProvider.TrySaveJsonAssetFile(DataFileName, deaths);
}