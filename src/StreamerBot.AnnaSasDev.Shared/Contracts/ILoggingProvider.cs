// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Diagnostics.CodeAnalysis;

namespace StreamerBot.AnnaSasDev;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface ILoggingProvider {
    bool Debug([StringSyntax(StringSyntaxAttribute.CompositeFormat)] string messageTemplate, params object[] args);
    
    bool Information([StringSyntax(StringSyntaxAttribute.CompositeFormat)] string messageTemplate, params object[] args);
    
    bool Warning([StringSyntax(StringSyntaxAttribute.CompositeFormat)] string messageTemplate, params object[] args);
    bool WarningAsFalse([StringSyntax(StringSyntaxAttribute.CompositeFormat)]string messageTemplate, params object[] args);
   
    bool Error([StringSyntax(StringSyntaxAttribute.CompositeFormat)] string messageTemplate, params object[] args);
    bool Error(Exception ex, [StringSyntax(StringSyntaxAttribute.CompositeFormat)] string messageTemplate, params object[] args);
    bool ErrorAsFalse([StringSyntax(StringSyntaxAttribute.CompositeFormat)]string messageTemplate, params object[] args);
    bool ErrorAsFalse(Exception ex,[StringSyntax(StringSyntaxAttribute.CompositeFormat)]string messageTemplate, params object[] args);
}