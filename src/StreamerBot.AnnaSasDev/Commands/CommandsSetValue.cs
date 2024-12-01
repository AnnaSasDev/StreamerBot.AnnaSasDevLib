// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Streamer.bot.Plugin.Interface;
using StreamerBot.AnnaSasDev.Services.OBS;

namespace StreamerBot.AnnaSasDev.Commands;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class CommandsSetValue {
    public static bool CommandEntryPoint(IInlineInvokeProxy cph) {
        // Always needed to run the various libraries that re in this assembly.
        CphService.SetCph(cph);

        if (!InputParsingService.TryParseInput()) return CphService.SendFailureReply();
        if (InputParsingService.GetAmountOfArguments() <= 0) return CphService.SendFailureReply("No Arguments for this command were given.");
        if (!InputParsingService.TryGetInput(0, out string? command)) return CphService.SendFailureReply("Could not find command.");
        if (!InputParsingService.TryGetInput(1, out string? argument)) return CphService.SendFailureReply("Could not find argument.");

        switch (command.ToLowerInvariant(), argument.ToLowerInvariant()) {
            #region FollowerGoal
            case ("followergoal", "reset"): {
                cph.SetGlobalVar("DailyFollowerGoal", FollowerGoalService.DefaultGoalAmount);
                if (!FollowerGoalService.TryUpdateObsSource()) return ErrorMessageService.AddErrorMessage("Could not update the OBS source.");

                break;
            }

            case ("followergoal", {} arg) when long.TryParse(arg, out long value): {
                cph.SetGlobalVar("DailyFollowerGoal", value);
                if (!FollowerGoalService.TryUpdateObsSource()) return ErrorMessageService.AddErrorMessage("Could not update the OBS source.");

                break;
            }

            case ("followergoal", _): {
                ErrorMessageService.AddErrorMessage($"Silly Anna! The value `{argument}` cannot be converted to a long!");
                break;
            }
            #endregion

            #region FollowerValue
            case ("followervalue", "reset"): {
                cph.SetGlobalVar("DailyFollowerValue", 0L, false);
                if (!FollowerGoalService.TryUpdateObsSource()) return ErrorMessageService.AddErrorMessage("Could not update the OBS source.");

                break;
            }

            case ("followervalue", {} arg) when long.TryParse(arg, out long value): {
                cph.SetGlobalVar("DailyFollowerValue", value, false);
                if (!FollowerGoalService.TryUpdateObsSource()) return ErrorMessageService.AddErrorMessage("Could not update the OBS source.");

                break;
            }

            case ("followervalue", _): {
                ErrorMessageService.AddErrorMessage($"Silly Anna! The value `{argument}` cannot be converted to a long!");
                break;
            }
            #endregion
        }

        return CphService.SendFailureReply();
    }
}
