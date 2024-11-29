// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Streamer.bot.Plugin.Interface;

namespace StreamerBot.AnnaSasDev.Library;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class GitCommand {
    private const string UrlAnnaSasDev = "https://github.com/AnnaSasDev";
    private const string UrlOrgAterraEngine = "https://github.com/AterraEngine";
    private const string UrlOrgCodeOfChaos = "https://github.com/code-of-chaos";
    private const string UrlOrgInfiniLore = "https://github.com/InfiniLore";
    private const string UrlColoredTagsWrangler = "https://github.com/code-of-chaos/obsidian-colored_tags_wrangler";
    
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

        if (!InputParsingService.TryParseInput()) return CphService.SendFailureMessages();
        if (InputParsingService.GetAmountOfArguments() <= 0) return CphService.TrySendMessage("something default"); 
        if (!InputParsingService.TryGetInput(0, out string? command)) return CphService.SendFailureMessages("Could not find the command.");
        
        switch (command.ToLowerInvariant()) {
            #region InfiniLore
            case "infinilore": {
                return CphService.TrySendMessage("something infinilore"); 
            }
            #endregion

            #region Code Of Chaos
            case "coc":
            case "codeofchaos":
            case "code-ofchaos":
            case "codeof-chaos":
            case "code-of-chaos": {
                const string defaultAnswer = $"Anna has a multitude of random project, all of these are compiled into the Code Of Chaos Organization : {UrlOrgCodeOfChaos}";
                if (!InputParsingService.TryGetInput(1, out string? repoName)) return CphService.TrySendMessage(defaultAnswer);

                switch (repoName.ToLowerInvariant()) {
                    case "ctw":
                    case "colored-tags-wrangler":
                    case "coloredtagswrangler": {
                        return CphService.TrySendMessage($"Anna made a plugin for Obsidian.md called 'Colored Tags Wrangler' which add fancy colors to your tags : {UrlColoredTagsWrangler}");
                    }
                    
                    default: {
                        return CphService.TrySendMessage(defaultAnswer);
                    }
                }
                
            }
            #endregion
                
            default: {
                return CphService.TrySendMessage($"Anna does lots of chaotic coding projects, most can be found at {UrlOrgCodeOfChaos} or {UrlAnnaSasDev}."); 
            }
        }
    }
}
