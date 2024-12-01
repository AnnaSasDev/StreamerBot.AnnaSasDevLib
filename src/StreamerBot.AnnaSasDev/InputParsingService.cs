// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Streamer.bot.Plugin.Interface;
using System.Diagnostics.CodeAnalysis;

namespace StreamerBot.AnnaSasDev;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class InputParsingService {
    private const string ArgumentNamePrefix = "CSharpTrackedArgument_INPUT";
    private const string ArgumentNameAmountOfArguments = "CSharpTrackedArgument_ArgumentNameAmountOfArguments";

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static bool TryGetInput(int index, [NotNullWhen(true)] out string? output) => CphService.TryGetArg($"{ArgumentNamePrefix}{index}", out output);

    private static void SetInput(string value, int index) {
        if (!CphService.TryGetCph(out IInlineInvokeProxy? cph)) return;

        cph.SetArgument($"{ArgumentNamePrefix}{index}", value);
    }

    public static bool TryParseInput() {
        if (!CphService.TryGetArg("rawInput", out string? rawInput)) {
            ErrorMessageService.AddErrorMessage("Could not find the rawInput argument.");
            return false;
        }

        if (!CphService.TryGetCph(out IInlineInvokeProxy? cph)) return false;

        int foundArguments = 0;
        foreach (string inputVariable in rawInput.Split(' ')) {
            if (string.IsNullOrWhiteSpace(inputVariable)) continue;

            SetInput(inputVariable, foundArguments++);
        }

        cph.SetArgument(ArgumentNameAmountOfArguments, foundArguments + 1);// Else the amount of arguments will be off by one.

        return true;
    }
    public static int GetAmountOfArguments() =>
        CphService.TryGetArg(ArgumentNameAmountOfArguments, out int? amountOfArguments)
            ? (int)amountOfArguments
            : -1;

    public static IEnumerable<string> GetArguments() {
        for (int i = 0; i < GetAmountOfArguments() - 1; i++) {
            if (!TryGetInput(i, out string? input)) continue;

            yield return input;
        }
    }
}
