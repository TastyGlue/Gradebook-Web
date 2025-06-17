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
        catch (Exception ex)
        {
            var isDeleteRequest = context.Request.Method.Equals("DELETE", StringComparison.OrdinalIgnoreCase);

            var errorResult = ex.GetErrorMessageFromException(isDeleteRequest);

            await HandleException(context, ex, errorResult);
        }
    }

    private static async Task HandleException(HttpContext context, Exception ex, ErrorResult errorResult)
    {
        Log.Error(Utils.GetFullExceptionMessage(ex));

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = ApiResponseFactory.GetHttpStatusCode(errorResult.ErrorCode);

        string text = JsonSerializer.Serialize(errorResult);
        await context.Response.WriteAsync(text);
    }
}
