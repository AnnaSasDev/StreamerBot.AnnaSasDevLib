// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;
using StreamerBot.AnnaSasDev.Services;

namespace StreamerBot.AnnaSasDev.Utilities;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class ObsUtilities(IStreamerBotUtilities utilities) : IObsUtilities {
    private const string DataFileName = "obsData.json";

    private Lazy<ObsData?> LazyObsData { get; } = new(static () =>
        AssetFileProvider.TryParseJsonAssetFile(DataFileName, out ObsData? data)
            ? data
            : null
    );

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public bool TryGetSourceReference(SourceReferenceId referenceId, [NotNullWhen(true)] out string? sceneName,
        [NotNullWhen(true)] out string? sourceName) {
        sourceName = null;
        sceneName = null;
        if (LazyObsData.Value is not { } data) return false;
        if (!data.SourceReferences.TryGetValue(referenceId.ToId(), out ObsSourceReference? sourceReference))
            return false;
        sceneName = sourceReference.SceneName;
        sourceName = sourceReference.SourceName;
        return true;
    }

    public bool TryUpdateTextSource(SourceReferenceId referenceId, string text)
        => TryGetSourceReference(referenceId, out string? sceneName, out string? sourceName)
           && TryUpdateTextSource(sceneName, sourceName, text);
    
    public bool TryUpdateTextSource(string sceneName, string sourceName, string text) {
        if (LazyObsData.Value is not { } data) throw new Exception("Could not parse asset file");
        if (!data.SceneNames.Contains(sceneName)) throw new Exception($"Could not find the scene name {sceneName}");

        utilities.InlineInvokeProxy.ObsSetGdiText(sceneName, sourceName, text);
        return true;
    }
    
    public bool TrySetVisibility(SourceReferenceId referenceId, bool visible)
        => TryGetSourceReference(referenceId, out string? sceneName, out string? sourceName)
           && TryToggleVisibility(sceneName, sourceName);

    public bool TrySetVisibility(string sceneName, string sourceName, bool visible) {
        if (LazyObsData.Value is not { } data) throw new Exception("Could not parse asset file");
        if (!data.SceneNames.Contains(sceneName)) throw new Exception($"Could not find the scene name {sceneName}");

        utilities.InlineInvokeProxy.ObsSetSourceVisibility(sceneName, sourceName, visible);
        return true;
    }

    public bool TryToggleVisibility(SourceReferenceId referenceId)
        => TryGetSourceReference(referenceId, out string? sceneName, out string? sourceName)
           && TryToggleVisibility(sceneName, sourceName);

    public bool TryToggleVisibility(string sceneName, string sourceName) {
        if (LazyObsData.Value is not { } data) throw new Exception("Could not parse asset file");
        if (!data.SceneNames.Contains(sceneName)) throw new Exception($"Could not find the scene name {sceneName}");

        bool state = utilities.InlineInvokeProxy.ObsIsSourceVisible(sceneName, sourceName);
        utilities.InlineInvokeProxy.ObsSetSourceVisibility(sceneName, sourceName, !state);
        return true;
    }
}