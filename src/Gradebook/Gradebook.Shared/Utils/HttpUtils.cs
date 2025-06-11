using System.IdentityModel.Tokens.Jwt;

namespace Gradebook.Shared.Utils;

public static class HttpUtils
{
    public static Guid? GetUserIdFrom(HttpContext context)
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

    public static bool? IsTokenExpired(HttpContext context, TimeSpan? expirationTimeSpan = null)
    {
        if (context.User.Identity?.IsAuthenticated != true)
            return null;

        var expiredClaim = context.User.FindFirst(JwtRegisteredClaimNames.Exp);
        if (expiredClaim is null)
            return null;

        if (!long.TryParse(expiredClaim.Value, out long expiryDateUnix))
            return null;

        var expiryDate = DateTimeOffset.FromUnixTimeSeconds(expiryDateUnix);

        if (expirationTimeSpan is null)
            return expiryDate < DateTime.UtcNow;
        else
        {
            var expirationTime = expiryDate.Add(expirationTimeSpan.Value);
            return expirationTime < DateTime.UtcNow;
        }
    }
}
