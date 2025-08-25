// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace StreamerBot.AnnaSasDev;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class ActionArguments {
    public const string ActionName = "actionName";
    
    public const string CommandName = "commandName";
    public const string CommandSource = "commandSource";
    
    public const string MsgId = "msgId";
    public const string RawInput = "rawInput";
    
    
    private const string StreamerBotUtilities = nameof(StreamerBotUtilities);
    private const string RawInputParsed = StreamerBotUtilities + nameof(RawInputParsed) ;
    public static string RawInputParsedAtIndex(int index) => RawInputParsed + index;
    public const string RawInputParsedCount = StreamerBotUtilities + nameof(RawInputParsedCount);
}