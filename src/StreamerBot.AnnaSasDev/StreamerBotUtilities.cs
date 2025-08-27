// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Streamer.bot.Plugin.Interface;
using StreamerBot.AnnaSasDev.Services;
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
    public IObsUtilities Obs { get; private set; } = null!;
    public ISoundUtilities Sound { get; private set; } = null!;
    // ReSharper disable once PropertyCanBeMadeInitOnly.Local
    public ILoggingProvider Logging { get; private set; } = null!;
    public INotificationUtilities Notification { get; private set; } = null!;

    // -----------------------------------------------------------------------------------------------------------------
    // Constructors
    // -----------------------------------------------------------------------------------------------------------------
    public static StreamerBotUtilities FromCPH(IInlineInvokeProxy cph) {
        var streamerBotUtilities = new StreamerBotUtilities {
            InlineInvokeProxy = cph,
            Logging = new LoggingProvider<StreamerBotUtilities>()
        };
        
        
        streamerBotUtilities.Arguments = new ArgumentUtilities(streamerBotUtilities);
        streamerBotUtilities.Messages = new MessageUtilities(streamerBotUtilities);
        streamerBotUtilities.ChatInput = new ChatInputUtilities(streamerBotUtilities);
        streamerBotUtilities.Obs = new ObsUtilities(streamerBotUtilities, new LoggingProvider<ObsUtilities>());
        streamerBotUtilities.Sound = new SoundUtilities(streamerBotUtilities, new LoggingProvider<SoundUtilities>());
        streamerBotUtilities.Notification = new NotificationUtilities(streamerBotUtilities, new LoggingProvider<SoundUtilities>());
        
        return streamerBotUtilities;
    }
    
    public static bool WithExceptionHandling(IInlineInvokeProxy cph, Func<StreamerBotUtilities, bool> func) {
        StreamerBotUtilities streamerBotUtilities = FromCPH(cph);
        return streamerBotUtilities.WithExceptionHandling(func);
    }

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    private bool WithExceptionHandling(Func<StreamerBotUtilities, bool> func) {
        try {
            return func(this);
        }
        catch (Exception e) {
            return Logging.Error(e, "An exception occurred: {0}", e.Message);
        }
    }
}