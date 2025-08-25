// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using StreamerBot.AnnaSasDev.Services;
using System.Diagnostics.CodeAnalysis;

namespace StreamerBot.AnnaSasDev;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
// [SuppressMessage("ReSharper", "UnusedMember.Global")]
// public static class CommandSetValueExtension {
//     public static bool CommandSetValue(this StreamerBotWrapper wrapper) {
//
//         if (!wrapper.TryParseUserInput()) return wrapper.SendFailureMessages();
//         if (wrapper.GetAmountOfUserInputArguments() <= 0) return wrapper.SendFailureMessages("No Arguments for this command were given.");
//         if (!wrapper.TryGetUserInput(0, out string? command)) return wrapper.SendFailureMessages("Could not find command.");
//         if (!wrapper.TryGetUserInput(1, out string? argument)) return wrapper.SendFailureMessages("Could not find argument.");
//
//         switch (command.ToLowerInvariant(), argument.ToLowerInvariant()) {
//             #region FollowerGoal
//             case ("followergoal", "reset"): {
//                 wrapper.TrySetGlobalVar("DailyFollowerGoal", FollowerGoalService.DefaultGoalAmount);
//                 if (!wrapper.ObsSpecifiedService.TryUpdateFollowerGoalObsSources()) return wrapper.ErrorMessages.Add("Could not update the OBS source.");
//
//                 break;
//             }
//
//             case ("followergoal", {} arg) when long.TryParse(arg, out long value): {
//                  wrapper.TrySetGlobalVar("DailyFollowerGoal", value);
//                 if (!wrapper.ObsSpecifiedService.TryUpdateFollowerGoalObsSources()) return wrapper.ErrorMessages.Add("Could not update the OBS source.");
//
//                 break;
//             }
//
//             case ("followergoal", _): {
//                 wrapper.ErrorMessages.Add($"Silly Anna! The value `{argument}` cannot be converted to a long!");
//                 break;
//             }
//             #endregion
//
//             #region FollowerValue
//             case ("followervalue", "reset"): {
//                  wrapper.TrySetGlobalVar("DailyFollowerValue", 0L);
//                 if (!wrapper.ObsSpecifiedService.TryUpdateFollowerGoalObsSources()) return wrapper.ErrorMessages.Add("Could not update the OBS source.");
//
//                 break;
//             }
//
//             case ("followervalue", {} arg) when long.TryParse(arg, out long value): {
//                  wrapper.TrySetGlobalVar("DailyFollowerValue", value);
//                 if (!wrapper.ObsSpecifiedService.TryUpdateFollowerGoalObsSources()) return wrapper.ErrorMessages.Add("Could not update the OBS source.");
//
//                 break;
//             }
//
//             case ("followervalue", _): {
//                 wrapper.ErrorMessages.Add($"Silly Anna! The value `{argument}` cannot be converted to a long!");
//                 break;
//             }
//             #endregion
//             
//             #region SubscriberPanel
//             case ("subpnl", "reset"):
//             case ("subscriberpanel", "reset"): {
//                 wrapper.Cph.ObsSetGdiText("Backend-Overlay-Blank", "text-subscriber", "");
//                 break;
//             }
//
//             case ("subpnl", {} arg): {
//                 wrapper.Cph.ObsSetGdiText("Backend-Overlay-Blank", "text-subscriber", arg);
//                 break;
//             }
//             case ("subscriberpanel", {} arg): {
//                 wrapper.Cph.ObsSetGdiText("Backend-Overlay-Blank", "text-subscriber", arg);
//                 break;
//             }
//             #endregion
//             
//             #region FollowerPanel
//             case ("fwpnl", "reset"):
//             case ("followerpanel", "reset"): {
//                 wrapper.Cph.ObsSetGdiText("Backend-Overlay-Blank", "text-subscriber", "");
//                 break;
//             }
//
//             case ("fwpnl", {} arg): {
//                 wrapper.Cph.ObsSetGdiText("Backend-Overlay-Blank", "text-subscriber", arg);
//                 break;
//             }
//             
//             case ("followerpanel", {} arg): {
//                 wrapper.Cph.ObsSetGdiText("Backend-Overlay-Blank", "text-subscriber", arg);
//                 break;
//             }
//             #endregion
//                 
//         }
//
//         return wrapper.SendFailureMessages();
//     }
// }
