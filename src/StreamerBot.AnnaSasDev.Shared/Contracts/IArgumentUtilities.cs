// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Diagnostics.CodeAnalysis;

// ReSharper disable once CheckNamespace
namespace StreamerBot.AnnaSasDev;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IArgumentUtilities {
    bool TryGetActionArg<T>(string argName, [NotNullWhen(true)] out T? value) ;
    bool TrySetActionArg<T>(string argName, T value) ;
    bool TryGetGlobalArg<T>(string argName, [NotNullWhen(true)] out T? value) ;
    bool TryGetGlobalNonPersistedArg<T>(string argName, [NotNullWhen(true)] out T? value) ;
    bool TrySetGlobalArg<T>(string argName, T value) ;
    bool TrySetGlobalNonPersistedArg<T>(string argName, T value) ;
}