// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Streamer.bot.Plugin.Interface;

// ReSharper disable once CheckNamespace
namespace StreamerBot.AnnaSasDev;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IStreamerBotUtilities {
    IInlineInvokeProxy InlineInvokeProxy { get; }
    
    IArgumentUtilities ArgumentUtilities { get; }
    IMessageUtilities MessageUtilities { get; }
}