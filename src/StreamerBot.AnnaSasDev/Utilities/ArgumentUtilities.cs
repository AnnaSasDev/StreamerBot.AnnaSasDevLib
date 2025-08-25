// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Diagnostics.CodeAnalysis;

namespace StreamerBot.AnnaSasDev.Utilities;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class ArgumentUtilities(IStreamerBotUtilities utilities) : IArgumentUtilities{
    public bool TryGetActionArg<T>(string argName, [NotNullWhen(true)] out T? value) {
        value = default;
        if (string.IsNullOrWhiteSpace(argName)) return false;
        
        if (!utilities.InlineInvokeProxy.TryGetArg(argName, out value)) return false;
        return value is not null;
    }

    public bool TrySetActionArg<T>(string argName, T value) {
        if (string.IsNullOrWhiteSpace(argName)) return false;
        
        // ReSharper disable once HeapView.PossibleBoxingAllocation
        utilities.InlineInvokeProxy.SetArgument(argName, value); 
        return true;
    }

    public bool TryGetGlobalArg<T>(string argName, [NotNullWhen(true)] out T? value) {
        value = default;
        if (string.IsNullOrWhiteSpace(argName)) return false;
        
        value = utilities.InlineInvokeProxy.GetGlobalVar<T>(argName);
        return value is not null;
    }

    public bool TryGetGlobalNonPersistedArg<T>(string argName, [NotNullWhen(true)] out T? value) {
        value = default;

        value = utilities.InlineInvokeProxy.GetGlobalVar<T>(argName, false);
        return value is not null;
    }
    
    public bool TrySetGlobalArg<T>(string argName, T value) {
        // ReSharper disable once HeapView.PossibleBoxingAllocation
        // ReSharper disable once RedundantArgumentDefaultValue
        utilities.InlineInvokeProxy.SetGlobalVar(argName, value, true);
        return true;
    }
    
    public bool TrySetGlobalNonPersistedArg<T>(string argName, T value) {
        // ReSharper disable once HeapView.PossibleBoxingAllocation
        utilities.InlineInvokeProxy.SetGlobalVar(argName, value, false);
        return true;
    }
}