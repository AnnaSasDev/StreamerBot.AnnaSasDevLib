// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using StreamerBot.AnnaSasDev.Services;

namespace StreamerBot.AnnaSasDev.Utilities;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class SoundUtilities(IStreamerBotUtilities utilities, ILoggingProvider logging) : ISoundUtilities {
    private const string DataFileName = "soundData.json";

    private Lazy<SoundData?> LazySoundData { get; } = new(static () => {
        AssetFileProvider.TryParseJsonAssetFile(DataFileName, out SoundData? data);
        return data;
    });

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public bool TryPlaySound(string soundName, float volume = 1f) {
        if (LazySoundData.Value is null) return logging.WarningAsFalse("Could not parse sound data file {0}", DataFileName);
        if (!LazySoundData.Value.Sounds.TryGetValue(soundName, out string? soundFileName)) return logging.WarningAsFalse("Could not find sound id {0}", soundName);
        if (string.IsNullOrWhiteSpace(soundFileName)) return logging.WarningAsFalse("Could not find valid sound file {0} path", soundName);

        string fullPath = Path.Combine(LazySoundData.Value.RootFolder, soundFileName);
        if (!File.Exists(fullPath)) return logging.WarningAsFalse("Could not find file {0}", fullPath);
        
        utilities.InlineInvokeProxy.PlaySound(fullPath, volume, true);
        return true;
    }
}