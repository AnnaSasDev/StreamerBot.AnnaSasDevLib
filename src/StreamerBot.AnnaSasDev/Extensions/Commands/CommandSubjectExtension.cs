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
// public static class CommandSubjectExtension {
//     // -----------------------------------------------------------------------------------------------------------------
//     // Methods
//     // -----------------------------------------------------------------------------------------------------------------
//     public static bool CommandSubject(this StreamerBotWrapper wrapper) {
//         if (!wrapper.TryGetArg("rawInput", out string? rawInput))
//             return wrapper.SendFailureMessages("Could not find the rawInput argument.");
//
//         if (!string.IsNullOrWhiteSpace(rawInput))
//             wrapper.TrySetGlobalVar(ObsSpecifiedService.ObjectSubjectTextVarName, rawInput);
//
//         if (!wrapper.ObsSpecifiedService.TryUpdateSubjectPanel())
//             return wrapper.SendFailureMessages("Could not update the OBS source.");
//
//         // Everything is nominal
//         return true;
//     }
// }
