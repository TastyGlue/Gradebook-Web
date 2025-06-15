namespace Gradebook.Shared.Models;

public class JwtSettings
{
    public string AuthSecurityKey { get; set; } = default!;

    public string AccessSecurityKey { get; set; } = default!;

    public int AuthTokenExpirationMinutes { get; set; }

    public int AccessTokenExpirationMinutes { get; set; }

    public int RefreshTokenExpirationDays { get; set; }
}