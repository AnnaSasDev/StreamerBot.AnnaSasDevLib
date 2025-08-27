// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Streamer.bot.Plugin.Interface;

namespace StreamerBot.AnnaSasDev;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IStreamerBotUtilities {
    IInlineInvokeProxy InlineInvokeProxy { get; }
    
    IArgumentUtilities Arguments { get; }
    IMessageUtilities Messages { get; }
    IChatInputUtilities ChatInput { get; }
    IObsUtilities Obs { get; }
    ISoundUtilities Sound { get; }
}