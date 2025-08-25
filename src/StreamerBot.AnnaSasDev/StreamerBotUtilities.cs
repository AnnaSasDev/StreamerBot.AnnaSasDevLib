// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Streamer.bot.Plugin.Interface;
using StreamerBot.AnnaSasDev.Shared;
using StreamerBot.AnnaSasDev.Utilities;

namespace StreamerBot.AnnaSasDev;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class StreamerBotUtilities : IStreamerBotUtilities {
    public IInlineInvokeProxy InlineInvokeProxy { get; private init; } = null!;
    public IArgumentUtilities ArgumentUtilities { get; private set; } = null!;
    public IMessageUtilities MessageUtilities { get; private set; } = null!;

    // -----------------------------------------------------------------------------------------------------------------
    // Constructors
    // -----------------------------------------------------------------------------------------------------------------
    public static StreamerBotUtilities FromCPH(IInlineInvokeProxy cph) {
        var streamerBotUtilities = new StreamerBotUtilities {
            InlineInvokeProxy = cph
        };
        
        streamerBotUtilities.ArgumentUtilities = new ArgumentUtilities(streamerBotUtilities);
        streamerBotUtilities.MessageUtilities = new MessageUtilities(streamerBotUtilities);
        
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
            ArgumentUtilities.TrySetActionArg(StreamerBotUtilitiesArguments.Exceptions, exceptionString);
            InlineInvokeProxy.LogError(exceptionString);
            return false;
        }
        finally {
            
        }
    }
}