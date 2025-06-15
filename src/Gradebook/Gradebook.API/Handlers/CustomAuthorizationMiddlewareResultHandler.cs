using Microsoft.AspNetCore.Authorization.Policy;

namespace Gradebook.API.Handlers;

public class CustomAuthorizationMiddlewareResultHandler : IAuthorizationMiddlewareResultHandler
{
    private readonly AuthorizationMiddlewareResultHandler _defaultHandler = new();

    public async Task HandleAsync(RequestDelegate next, HttpContext context,
        AuthorizationPolicy policy, PolicyAuthorizationResult authorizeResult)
    {
        if (authorizeResult.Forbidden)
        {
            context.Response.StatusCode = 403;
            context.Response.ContentType = "application/json";

            var response = new ErrorResult("Unauthorized access", ErrorCodes.ACCESS_NOT_AUTHORIZED);

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            return;
        }

        await _defaultHandler.HandleAsync(next, context, policy, authorizeResult);
    }
}