namespace Gradebook.API.Middlewares;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (DbUpdateException ex)
        {
            // Unique constraint violation in PostgreSQL
            if (ex.InnerException is PostgresException pgEx)
            {
                switch (pgEx.SqlState)
                {
                    case PostgresErrorCodes.UniqueViolation:
                        var indexViolationMessage = GetIndexViolationMessage(pgEx.ConstraintName);

                        await HandleException(context, ex, ErrorCodes.DB_UNIQUE_VIOLATION, indexViolationMessage ?? "This data already exists");
                        break;
                    case PostgresErrorCodes.ForeignKeyViolation:
                        var isDeleteRequest = context.Request.Method.Equals("DELETE", StringComparison.OrdinalIgnoreCase);

                        var errorMessage = isDeleteRequest
                            ? "Cannot delete this item because it is referenced by other data"
                            : "Referenced data not found or invalid foreign key";

                        await HandleException(context, ex, ErrorCodes.DB_FOREIGN_KEY_VIOLATION, errorMessage);
                        break;
                    case PostgresErrorCodes.NotNullViolation:
                        await HandleException(context, ex, ErrorCodes.DB_NOT_NULL_VIOLATION, "Required field is missing");
                        break;
                    default:
                        await HandleException(context, ex, ErrorCodes.DB_UNEXPECTED_ERROR, "An unexpected database error occurred");
                        break;
                }
            }
        }
        catch (ArgumentException ex)
        {
            await HandleException(context, ex, ErrorCodes.API_INVALID_ARGUMENT, ex.Message);
        }
        catch (Exception ex)
        {
            await HandleException(context, ex, ErrorCodes.API_UNEXPECTED_ERROR, "An unexpected error occurred");
        }
    }

    private static async Task HandleException(HttpContext context, Exception ex, string errorCode, string errorMessage, List<string> details = null!)
    {
        Log.Error(Utils.GetFullExceptionMessage(ex));

        var errorResult = new ErrorResult(errorMessage, errorCode, details);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = ApiResponseFactory.GetHttpStatusCode(errorCode);

        string text = JsonSerializer.Serialize(errorResult);
        await context.Response.WriteAsync(text);
    }

    private static string? GetIndexViolationMessage(string? constraintName)
    {
        if (string.IsNullOrWhiteSpace(constraintName))
            return null;

        var indexViolation = IndexConstants.IndexViolations
            .FirstOrDefault(iv => iv.IndexName.Equals(constraintName, StringComparison.OrdinalIgnoreCase));

        return indexViolation?.ErrorMessage;
    }
}
