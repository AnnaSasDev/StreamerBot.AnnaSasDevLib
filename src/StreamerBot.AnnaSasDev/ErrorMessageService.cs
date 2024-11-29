// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace StreamerBot.AnnaSasDev;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class ErrorMessageService {
    private readonly static Queue<string> ErrorMessages = new();
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static bool AddErrorMessage(string errorMessage) {
        ErrorMessages.Enqueue(errorMessage);
        return false;
    }
    public static bool TryGetErrorMessage(out string? errorMessage) {
        errorMessage = null;
        if (ErrorMessages.Count == 0) return false;
        errorMessage = ErrorMessages.Dequeue();
        return true;
    }
}
