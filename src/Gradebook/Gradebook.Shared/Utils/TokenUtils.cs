using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Gradebook.Shared.Utils;

public static class TokenUtils
{
    public static Guid? GetUserId(ClaimsPrincipal user)
    {
        if (user.Identity is null)
            return null;

        var userIdClaim = user.FindFirst(Claims.USER_ID);
        if (userIdClaim is null)
            return null;

        if (!Guid.TryParse(userIdClaim.Value, out var userId))
            return null;

        return userId;
    }

    public static bool? IsTokenExpired(ClaimsPrincipal user, TimeSpan? expirationTimeSpan = null)
    {
        if (user.Identity is null)
            return null;

        var expiredClaim = user.FindFirst(JwtRegisteredClaimNames.Exp);
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

    public static string? GetTokenString(IHeaderDictionary headers)
    {
        if (!headers.TryGetValue("Authorization", out var values))
            return null;

        if (string.IsNullOrWhiteSpace(values))
            return null;

        if (!values.ToString().StartsWith("Bearer "))
            return null;

        string token = values.ToString().Substring("Bearer ".Length).Trim();

        return token;
    }

    public static bool ValidateToken(string token, string securityKey, bool validateLifetime = true) 
    { 
        var tokenHandler = new JwtSecurityTokenHandler();

        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(securityKey)),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = validateLifetime,
            ClockSkew = TimeSpan.Zero // Disable clock skew for precise validation
        };

        try
        {
            tokenHandler.ValidateToken(token, validationParameters, out _);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
