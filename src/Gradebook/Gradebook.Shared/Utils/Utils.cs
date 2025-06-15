namespace Gradebook.Shared.Utils;

public static class Utils
{
    public static string GetFullExceptionMessage(Exception ex)
    {
        if (ex == null) return string.Empty;

        var errorMessage = new StringBuilder();
        errorMessage.AppendLine($"Exception: {ex.Message}");
        errorMessage.AppendLine($"Stack Trace: {ex.StackTrace}");

        var innerException = ex.InnerException;
        while (innerException != null)
        {
            errorMessage.AppendLine("---- Inner Exception ----");
            errorMessage.AppendLine($"Exception: {innerException.Message}");
            errorMessage.AppendLine($"Stack Trace: {innerException.StackTrace}");
            innerException = innerException.InnerException;
        }

        return errorMessage.ToString();
    }
}
