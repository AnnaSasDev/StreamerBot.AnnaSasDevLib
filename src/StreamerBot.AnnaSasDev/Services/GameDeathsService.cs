// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Streamer.bot.Plugin.Interface.Model;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace StreamerBot.AnnaSasDev.Services;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class GameDeathsService(StreamerBotWrapper wrapper) {
    private const string Prefix = "CSharpTrackedArgument_GameDeaths_";
    private const string GameDeathsJson = Prefix + "GameDeathsJson";
    private const string CurrentGameName = Prefix + "CurrentGameName";
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    private bool TryGetGameDeaths(out Dictionary<string, int> gameDeaths) {
        gameDeaths = new Dictionary<string, int>();
        try {
            if (!wrapper.TryGetGlobalVar(GameDeathsJson, out string? gameDeathsJson)) { gameDeaths = new Dictionary<string, int>(); }
            else {
                var data = JsonSerializer.Deserialize<Dictionary<string, int>>(gameDeathsJson);
                gameDeaths = data ?? [];
            }
            return true;
        }
        catch (Exception ex) {
            wrapper.ErrorMessages.Add(ex.Message);
            return false;
        }
    }

    private bool TrySetGameDeaths(Dictionary<string, int> gameDeaths) {
        try {
            string data = JsonSerializer.Serialize(gameDeaths);
            wrapper.TrySetGlobalVar(GameDeathsJson, data);
            return true;
        }
        catch (Exception ex) {
            wrapper.ErrorMessages.Add(ex.Message);
            return false;
        }
    }
    
    public bool OnGameChange() {
        if(!wrapper.TryGetArg("gameName", out string? gameName)) return wrapper.SendFailureMessages("Could not find the gameName argument.");
        if(!TryGetGameDeaths(out Dictionary<string, int> gameDeaths)) return wrapper.SendFailureMessages("Could not find the gameDeathsJson argument.");

        if (!gameDeaths.ContainsKey(gameName)) gameDeaths.Add(gameName, 0); 
        if(!wrapper.TrySetGlobalVar(CurrentGameName, gameName)) return wrapper.SendFailureMessages("Could not set the current game name.");
        
        if (!TrySetGameDeaths(gameDeaths) ) return wrapper.SendFailureMessages("Could not set the gameDeathsJson argument.");
        int deaths = gameDeaths[gameName];
        wrapper.Cph.ObsSetGdiText("Game - Stream", "text-deaths", $"{deaths} Deaths");
        return true;
    }

    public bool OnGameDeath() {
        // Try and get user input on howmany deaths we should add
        if (!wrapper.TryParseUserInput()) return wrapper.SendFailureMessages();
        int deathsToAdd = 1;
        if (wrapper.TryGetUserInput(0, out string? targetInt) && !int.TryParse(targetInt, out deathsToAdd)) return wrapper.SendFailureMessages("Could not parse the targetInt argument.");

        // Try and get the game name
        if (!wrapper.TryGetGlobalVar(CurrentGameName, out string? gameName)) {
            TwitchUserInfo? info = wrapper.Cph.TwitchGetBroadcaster();
            if (info is null) return wrapper.SendFailureMessages("Could not find the current broadcaster info.");
            
            TwitchUserInfoEx? userInfo = wrapper.Cph.TwitchGetExtendedUserInfoById(info.UserId);
            if (userInfo is null) return wrapper.SendFailureMessages("Could not find the current broadcaster extended info.");

            gameName = userInfo.Game;
        }
        if (!TryGetGameDeaths(out Dictionary<string, int> gameDeaths)) return wrapper.SendFailureMessages("Could not find the gameDeathsJson argument.");
        if (!gameDeaths.ContainsKey(gameName)) gameDeaths.Add(gameName, 0);
        
        // Apply deaths
        gameDeaths[gameName] += deathsToAdd;
        if (!TrySetGameDeaths(gameDeaths) ) return wrapper.SendFailureMessages("Could not set the gameDeathsJson argument.");
        int deaths = gameDeaths[gameName];
        wrapper.Cph.ObsSetGdiText("Game - Stream", "text-deaths", $"{deaths} Deaths");
        
        // output it in chat as well
        return OnGameDeathGetCount();
    }

    public bool OnGameDeathReset() {
        if (!wrapper.TryGetGlobalVar(CurrentGameName, out string? gameName)) return wrapper.SendFailureMessages("Could not find the current game name.");
        if (!TryGetGameDeaths(out Dictionary<string, int> gameDeaths)) return wrapper.SendFailureMessages("Could not find the gameDeathsJson argument.");
        if (!gameDeaths.ContainsKey(gameName)) gameDeaths.Add(gameName, 0);
        gameDeaths[gameName] = 0;
        return TrySetGameDeaths(gameDeaths);
    }
    
    public bool OnGameDeathResetAll() {
        if (!TryGetGameDeaths(out Dictionary<string, int> gameDeaths)) return wrapper.SendFailureMessages("Could not find the gameDeathsJson argument.");
        gameDeaths.Clear();
        return TrySetGameDeaths(gameDeaths);
    }

    public bool OnGameDeathGetCount() {
        if (!wrapper.TryGetGlobalVar(CurrentGameName, out string? gameName)) return wrapper.SendFailureMessages("Could not find the current game name.");
        if (!TryGetGameDeaths(out Dictionary<string, int> gameDeaths)) return wrapper.SendFailureMessages("Could not find the gameDeathsJson argument.");
        if (!gameDeaths.ContainsKey(gameName)) gameDeaths.Add(gameName, 0);

        int count = gameDeaths[gameName];
        string s = count is 1 or 0  ? string.Empty : "s";
        return wrapper.TrySendReply($"Anna has died {gameDeaths[gameName]} time{s} in {gameName}");
    }
}
