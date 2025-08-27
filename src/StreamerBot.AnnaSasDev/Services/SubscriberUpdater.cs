// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace StreamerBot.AnnaSasDev.Services;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class SubscriberUpdater {
    // ReSharper disable once ConvertIfStatementToReturnStatement
    public static bool UpdateNewSubscriberText(IStreamerBotUtilities utilities) {
        if (!utilities.Arguments.TryGetActionArg(ActionArguments.User, out string? userName)) throw new Exception("Could not find user name");
        return utilities.Obs.TryUpdateTextSource(SourceReferenceId.TextSubscriber, userName);
    }
    
    // ReSharper disable once InvertIf
    public static bool UpdateGlobalSubscriberGoal(IStreamerBotUtilities utilities) {
        if (!utilities.Arguments.TryGetActionArg(ActionArguments.SubscriberCount, out long subCount)) throw new Exception("Could not find subscriber count");
        utilities.Arguments.TryGetGlobalArg(ActionArguments.GlobalSubscriberGoal, out long subGoal);
        if (subGoal == 0 || subGoal < subCount + 5) {
            subGoal = subCount + 5;
            utilities.Arguments.TrySetGlobalArg(ActionArguments.GlobalSubscriberGoal, subGoal);
        }
        
        return utilities.Obs.TryUpdateTextSource(SourceReferenceId.TextGoalSubscriber, $"Subs\n{subCount}/{subGoal}");
    }
    
}