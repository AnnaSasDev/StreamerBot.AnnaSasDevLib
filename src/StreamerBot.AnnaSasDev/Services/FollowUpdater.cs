// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------

namespace StreamerBot.AnnaSasDev.Services;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class FollowUpdater {
    // ReSharper disable once InvertIf
    public static bool UpdateGlobalFollowerGoal(IStreamerBotUtilities utilities) {
        if (!utilities.Arguments.TryGetActionArg(ActionArguments.FollowerCount, out long followCount)) throw new Exception("Could not find follower count");
        utilities.Arguments.TryGetGlobalArg(ActionArguments.GlobalFollowerGoal, out long followerGoal);
        if (followerGoal == 0) {
            followerGoal = followCount + 100;
            utilities.Arguments.TrySetGlobalArg(ActionArguments.GlobalFollowerGoal, followerGoal);
        }
        
        return utilities.Obs.TryUpdateTextSource(SourceReferenceId.TextGoalFollower, $"Followers\n{followCount}/{followerGoal}");
    }
    
    // ReSharper disable once InvertIf
    public static bool UpdateDailyFollowerGoal(IStreamerBotUtilities utilities, int specificCount = -1) {
        long followerDailyCurrent;
        if (specificCount != -1) followerDailyCurrent = 0;
        else {
            utilities.Arguments.TryGetGlobalArg(ActionArguments.GlobalFollowerDailyCurrent, out followerDailyCurrent);
            followerDailyCurrent++;
        }
        utilities.Arguments.TrySetGlobalArg(ActionArguments.GlobalFollowerDailyCurrent, followerDailyCurrent);

        utilities.Arguments.TryGetGlobalArg(ActionArguments.GlobalFollowerDailyGoal, out long followerDailyGoal);
        if (followerDailyGoal == 0) {
            followerDailyGoal = 5;
            utilities.Arguments.TrySetGlobalArg(ActionArguments.GlobalFollowerDailyGoal, followerDailyGoal);
        }

        return utilities.Obs.TryUpdateTextSource(
            SourceReferenceId.TextFollowerGoalDaily,
            $"{followerDailyCurrent}/{followerDailyGoal}"
        );
    }
}