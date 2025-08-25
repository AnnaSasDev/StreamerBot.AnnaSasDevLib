// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;

namespace StreamerBot.AnnaSasDev;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IObsUtilities {
    bool TryGetSourceReference(SourceReferenceId referenceId, [NotNullWhen(true)] out string? sceneName, [NotNullWhen(true)] out string? sourceName);
    
    bool TryUpdateTextSource(SourceReferenceId referenceId, string text);
    bool TryUpdateTextSource(string sceneName, string sourceName, string text);
    
    bool TrySetVisibility(SourceReferenceId referenceId, bool visible);
    bool TrySetVisibility(string sceneName, string sourceName, bool visible);

    bool TryToggleVisibility(SourceReferenceId referenceId);
    bool TryToggleVisibility(string sceneName, string sourceName);
}