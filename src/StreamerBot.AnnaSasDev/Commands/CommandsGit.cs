// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Streamer.bot.Plugin.Interface;

namespace StreamerBot.AnnaSasDev.Commands;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class CommandsGit {
    private const string UrlAnnaSasDev = "https://github.com/AnnaSasDev";
    private const string UrlOrgAterraEngine = "https://github.com/AterraEngine";
    private const string UrlOrgCodeOfChaos = "https://github.com/code-of-chaos";
    private const string UrlOrgInfiniLore = "https://github.com/InfiniLore";
    private const string UrlColoredTagsWrangler = "https://github.com/code-of-chaos/obsidian-colored_tags_wrangler";
    private const string UrlStreamerBotAnnaSasDevLib = "https://github.com/AnnaSasDev/StreamerBot.AnnaSasDevLib";

    // Todo test if all urls are valid?
    // Todo put all urls in a small lib for easy access and testing.
    public static string[] AllUrls => [
        UrlAnnaSasDev,
        UrlOrgAterraEngine,
        UrlOrgCodeOfChaos,
        UrlOrgInfiniLore,
        UrlColoredTagsWrangler
    ];

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static bool CommandEntryPoint(IInlineInvokeProxy cph) {
        // Always needed to run the various libraries that re in this assembly.
        CphService.SetCph(cph);

        if (!InputParsingService.TryParseInput()) return CphService.SendFailureReply();
        if (InputParsingService.GetAmountOfArguments() <= 1) return CphService.TrySendReply("Anna does lots of chaotic coding projects, most can be found at https://github.com/code-of-chaos or https://github.com/AnnaSasDev");
        if (!InputParsingService.TryGetInput(0, out string? command)) return CphService.SendFailureReply("Could not find the command.");

        switch (command.ToLowerInvariant()) {
            #region AterraEngine
            case "ateraengine":
            case "aterraengine":
            case "aterra": {
                return CphService.TrySendReply($"Anna is developing their own Game Engine called AterraEngine : {UrlOrgAterraEngine}");
            }
            #endregion

            #region InfiniLore
            case "infinlore":
            case "infinilore": {
                return CphService.TrySendReply($"Anna is developing their own platform to create and share Lore : {UrlOrgInfiniLore}");
            }
            #endregion

            #region Code Of Chaos
            case "coc":
            case "codeofchaos":
            case "code-ofchaos":
            case "codeof-chaos":
            case "code-of-chaos": {
                const string defaultAnswer = $"Anna has a multitude of random project, all of these are compiled into the Code Of Chaos Organization : {UrlOrgCodeOfChaos}";
                if (!InputParsingService.TryGetInput(1, out string? repoName)) return CphService.TrySendReply(defaultAnswer);

                switch (repoName.ToLowerInvariant()) {
                    case "ctw":
                    case "colored-tags-wrangler":
                    case "coloredtagswrangler": {
                        return CphService.TrySendReply($"Anna made a plugin for Obsidian.md called 'Colored Tags Wrangler' which add fancy colors to your tags : {UrlColoredTagsWrangler}");
                    }

                    default: {
                        return CphService.TrySendReply(defaultAnswer);
                    }
                }

            }
            #endregion
            
            #region Bot
            case "twitch-bot":
            case "twitchbot": {
                return CphService.TrySendReply($"Anna uses Streamer.Bot for most of her twitch automation. This is extended by writing her own service library : {UrlStreamerBotAnnaSasDevLib}");
            }
            #endregion

            default: {
                return CphService.TrySendReply($"Anna does lots of chaotic coding projects, most can be found at {UrlOrgCodeOfChaos} or {UrlAnnaSasDev}.");
            }
        }
    }
}
