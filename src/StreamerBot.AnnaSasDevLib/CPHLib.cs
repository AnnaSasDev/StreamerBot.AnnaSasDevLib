// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Streamer.bot.Plugin.Interface;
using Streamer.bot.Plugin.Interface.Enums;
using Streamer.bot.Plugin.Interface.Model;
using Streamer.bot.Common.Events;

namespace StreamerBot.AnnaSasDevLib;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class CPHLib {
    public static bool RunSomething(IInlineInvokeProxy cph) {
        cph.SendMessage("Hello World, from a special place!");
        return true;
    }
}
