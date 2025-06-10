namespace Gradebook.API.Utils;

public static class HttpUtils
{
    public static Guid? GetUserIdFromHttpContext(HttpContext context)
    {
        if (context.User.Identity?.IsAuthenticated != true)
            return null;

        var userIdClaim = context.User.FindFirst(Claims.USER_ID);
        if (userIdClaim is null)
            return null;

        if (!Guid.TryParse(userIdClaim.Value, out var userId))
            return null;

        return userId;
    }
}
