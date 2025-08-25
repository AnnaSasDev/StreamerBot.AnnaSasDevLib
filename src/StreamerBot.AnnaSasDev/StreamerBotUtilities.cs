// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Streamer.bot.Plugin.Interface;
using StreamerBot.AnnaSasDev.Utilities;

namespace StreamerBot.AnnaSasDev;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class StreamerBotUtilities : IStreamerBotUtilities {
    public IInlineInvokeProxy InlineInvokeProxy { get; private init; } = null!;
    public IArgumentUtilities Arguments { get; private set; } = null!;
    public IMessageUtilities Messages { get; private set; } = null!;
    public IChatInputUtilities ChatInput { get; private set; } = null!;

    // -----------------------------------------------------------------------------------------------------------------
    // Constructors
    // -----------------------------------------------------------------------------------------------------------------
    public static StreamerBotUtilities FromCPH(IInlineInvokeProxy cph) {
        var streamerBotUtilities = new StreamerBotUtilities {
            InlineInvokeProxy = cph
        };
        
        streamerBotUtilities.Arguments = new ArgumentUtilities(streamerBotUtilities);
        streamerBotUtilities.Messages = new MessageUtilities(streamerBotUtilities);
        streamerBotUtilities.ChatInput = new ChatInputUtilities(streamerBotUtilities);
        
        return streamerBotUtilities;
    }

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public bool WithExceptionHandling(Func<StreamerBotUtilities, bool> func) {
        try {
            return func(this);
        }
        catch (Exception e) {
            var exceptionString = e.ToString();
            Arguments.TrySetActionArg(StreamerBotUtilitiesArguments.Exceptions, exceptionString);
            InlineInvokeProxy.LogError(exceptionString);
            return false;
        }
        finally {
            
        }
    }
}