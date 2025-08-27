// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;
using StreamerBot.AnnaSasDev.Services;

namespace StreamerBot.AnnaSasDev.Utilities;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class ObsUtilities(IStreamerBotUtilities utilities, ILoggingProvider logging) : IObsUtilities {
    private const string DataFileName = "obsData.json";

    private Lazy<ObsData?> LazyObsData { get; } = new(static () =>
        AssetFileProvider.TryParseJsonAssetFile(DataFileName, out ObsData? data)
            ? data
            : null
    );

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    private bool TryVerifySceneName(string sceneName) {
        if (LazyObsData.Value is not { } data) return logging.WarningAsFalse("Could not parse asset file");
        return data.SceneNames.Contains(sceneName) || logging.WarningAsFalse("Could not find sceneName {0}", sceneName);
    }
    
    public bool TryGetSourceReference(SourceReferenceId referenceId, [NotNullWhen(true)] out string? sceneName,
        [NotNullWhen(true)] out string? sourceName) {
        sourceName = null;
        sceneName = null;
        if (LazyObsData.Value is not { } data) return logging.WarningAsFalse("Could not parse asset file");
        if (!data.SourceReferences.TryGetValue(referenceId.ToId(), out ObsSourceReference? sourceReference)) return logging.WarningAsFalse("Could not find source reference {0}", referenceId);
        sceneName = sourceReference.SceneName;
        sourceName = sourceReference.SourceName;
        return true;
    }

    public bool TryUpdateTextSource(SourceReferenceId referenceId, string text)
        => TryGetSourceReference(referenceId, out string? sceneName, out string? sourceName)
           && TryUpdateTextSource(sceneName, sourceName, text);
    
    public bool TryUpdateTextSource(string sceneName, string sourceName, string text) {
        if (!TryVerifySceneName(sceneName)) return false;

        utilities.InlineInvokeProxy.ObsSetGdiText(sceneName, sourceName, text);
        return true;
    }
    
    public bool TrySetVisibility(SourceReferenceId referenceId, bool visible)
        => TryGetSourceReference(referenceId, out string? sceneName, out string? sourceName)
           && TryToggleVisibility(sceneName, sourceName);

    public bool TrySetVisibility(string sceneName, string sourceName, bool visible) {
        if (!TryVerifySceneName(sceneName)) return false;
        utilities.InlineInvokeProxy.ObsSetSourceVisibility(sceneName, sourceName, visible);
        return true;
    }

    public bool TryToggleVisibility(SourceReferenceId referenceId)
        => TryGetSourceReference(referenceId, out string? sceneName, out string? sourceName)
           && TryToggleVisibility(sceneName, sourceName);

    public bool TryToggleVisibility(string sceneName, string sourceName) {
        if (!TryVerifySceneName(sceneName)) return false;

        bool state = utilities.InlineInvokeProxy.ObsIsSourceVisible(sceneName, sourceName);
        utilities.InlineInvokeProxy.ObsSetSourceVisibility(sceneName, sourceName, !state);
        return true;
    }

    public bool TrySetImageSource(SourceReferenceId referenceId, string imageFilePath) 
        => TryGetSourceReference(referenceId, out string? sceneName, out string? sourceName)
           && TrySetImageSource(sceneName, sourceName, imageFilePath);
    
    public bool TrySetImageSource(string sceneName, string sourceName, string imageFilePath) {
        if (!TryVerifySceneName(sceneName)) return false;
        
        utilities.InlineInvokeProxy.ObsSetImageSourceFile(sceneName, sourceName, imageFilePath);
        return true;
    }
}