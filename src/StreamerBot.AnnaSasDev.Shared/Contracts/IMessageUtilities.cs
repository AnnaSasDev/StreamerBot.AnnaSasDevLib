// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using StreamerBot.AnnaSasDev.Shared;

// ReSharper disable once CheckNamespace

namespace StreamerBot.AnnaSasDev;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IMessageUtilities { 
    bool TrySendMessage(string message, MessageTarget target); 
    bool TrySendMessageContextAware(string message);
}