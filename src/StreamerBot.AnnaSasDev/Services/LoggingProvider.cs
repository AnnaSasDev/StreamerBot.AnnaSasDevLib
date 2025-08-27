// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Diagnostics.CodeAnalysis;

namespace StreamerBot.AnnaSasDev.Services;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class LoggingProvider<T> : ILoggingProvider {
    private const string LogFileName = "application.log";
    
    private const string DebugLevel = "DEBUG";
    private const string InfoLevel = "INFO";
    private const string WarnLevel = "WARN";
    private const string ErrorLevel = "ERROR";

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public bool Information([StringSyntax(StringSyntaxAttribute.CompositeFormat)] string messageTemplate, params object[] args) {
        try {
            return WriteLogEntry(InfoLevel, string.Format(messageTemplate, args));
        }
        catch (FormatException) {
            // If formatting fails, log the template as-is
            return WriteLogEntry(InfoLevel, $"Template: {messageTemplate} | Args: [{string.Join(", ", args)}]");
        }
    }

    public bool Warning([StringSyntax(StringSyntaxAttribute.CompositeFormat)] string messageTemplate, params object[] args) {
        try {
            return WriteLogEntry(WarnLevel, string.Format(messageTemplate, args));
        }
        catch (FormatException) {
            return WriteLogEntry(WarnLevel, $"Template: {messageTemplate} | Args: [{string.Join(", ", args)}]");
        }
    }
    
    public bool WarningAsFalse([StringSyntax(StringSyntaxAttribute.CompositeFormat)]string messageTemplate, params object[] args) {
        Warning(messageTemplate, args);
        return false;
    }

    public bool Error([StringSyntax(StringSyntaxAttribute.CompositeFormat)] string messageTemplate, params object[] args) {
        try {
            return WriteLogEntry(ErrorLevel, string.Format(messageTemplate, args));
        }
        catch (FormatException) {
            return WriteLogEntry(ErrorLevel, $"Template: {messageTemplate} | Args: [{string.Join(", ", args)}]");
        }
    }

    public bool Error(Exception ex,[StringSyntax(StringSyntaxAttribute.CompositeFormat)] string messageTemplate, params object[] args) {
        try {       
            string additionalMessage = string.Format(messageTemplate, args);
            return WriteLogEntry(ErrorLevel, $"{additionalMessage}\nException: {ex.Message}\nStackTrace: {ex.StackTrace}");
        }
        catch (FormatException) {
            return WriteLogEntry(ErrorLevel, $"Exception:{ex} Template: {messageTemplate} | Args: [{string.Join(", ", args)}]");
        }
    }

    public bool ErrorAsFalse([StringSyntax(StringSyntaxAttribute.CompositeFormat)]string messageTemplate, params object[] args) {
        Error(messageTemplate, args);
        return false;
    }
    
    public bool ErrorAsFalse(Exception ex, [StringSyntax(StringSyntaxAttribute.CompositeFormat)]string messageTemplate, params object[] args) {
        Error(ex, messageTemplate, args);
        return false;
    }

    public bool Debug([StringSyntax(StringSyntaxAttribute.CompositeFormat)] string messageTemplate, params object[] args) {
        try {
            return WriteLogEntry(DebugLevel,string.Format(messageTemplate, args));
        }
        catch (FormatException) {
            return WriteLogEntry(DebugLevel,$"Template: {messageTemplate} | Args: [{string.Join(", ", args)}]");
        }
    }

    private static bool WriteLogEntry(string level, string message) {
        var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        var logEntry = $"[{timestamp} | {nameof(T)} | {level}] {message}";
        return AssetFileProvider.TryWriteLineToAssetFile(LogFileName, logEntry);
    }
}