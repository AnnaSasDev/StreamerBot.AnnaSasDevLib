// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;

namespace StreamerBot.AnnaSasDev;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IObsUtilities {
    bool TryGetSourceReference(string referenceId, [NotNullWhen(true)] out string? sceneName, [NotNullWhen(true)] out string? sourceName);
    bool TryUpdateTextSource(string sceneName, string sourceName, string text);
    bool TrySetVisibility(string sceneName, string sourceName, bool visible);
    bool TryToggleVisibility(string sceneName, string sourceName);
}