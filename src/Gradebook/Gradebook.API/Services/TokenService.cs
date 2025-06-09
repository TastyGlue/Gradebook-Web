namespace Gradebook.API.Services;

public class TokenService : ITokenService
{
    private readonly JwtSettings _jwtSettings;

    public TokenService(IOptions<JwtSettings> options)
    {
        _jwtSettings = options.Value;
    }

    public string GenerateAuthToken(User user, IList<Profile> profiles)
    {
        var claims = new List<Claim>
        {
            new(Claims.USER_ID, user.Id.ToString())
        };

        // Prevent users without profiles from generating an auth token
        if (profiles.Count == 0)
            throw new ArgumentException("User must have at least one profile.");

        var profileClaims = new List<ProfileClaim>();

        // Add profile claims
        foreach (var profile in profiles)
        {
            if (profile is ISchoolMember schoolMember)
                profileClaims.Add(new ProfileClaim(profile.RoleType.ToString(), profile.Id, schoolMember.SchoolName));
            else
                profileClaims.Add(new ProfileClaim(profile.RoleType.ToString(), profile.Id, null));
        }

        var profileClaimsJson = JsonSerializer.Serialize(profileClaims);

        claims.Add(new Claim(Claims.PROFILES, profileClaimsJson));

        return WriteToken(
            claims: claims,
            expirationMinutes: _jwtSettings.AuthTokenExpirationMinutes,
            securityKey: _jwtSettings.AuthSecurityKey
        );
    }

    public string GenerateAccessToken(Profile profile)
    {
        var claims = new List<Claim>();

        // Add profile-specific claims
        if (profile is IBusinessEmail profileWithBusinessEmail)
            claims.Add(new(Claims.BUSINESS_EMAIL, profileWithBusinessEmail.BusinessEmail));

        if (profile is ISchoolMember schoolMember)
            claims.Add(new(Claims.SCHOOL_ID, schoolMember.SchoolId!.Value.ToString()));

        if (profile is Student student)
        {
            if (student.ClassId is not null)
                claims.Add(new(Claims.CLASS_ID, student.ClassId.Value.ToString()));
        }
        if (profile is Parent parent)
        {
            if (parent.Students.Count > 0)
            {
                claims.Add(new(Claims.STUDENT_IDS, string.Join(',', parent.Students.Select(s => s.Id))));
            }
        }

        var user = profile.User;

        // Add general user claims
        var userClaims = new List<Claim>()
            {
                new(Claims.USER_ID, user.Id.ToString()),
                new(Claims.FULL_NAME, user.FullName),
                new(Claims.EMAIL, user.Email!)
            };

        claims.AddRange(userClaims);

        return WriteToken(
            claims: claims, 
            expirationMinutes: _jwtSettings.AccessTokenExpirationMinutes,
            securityKey: _jwtSettings.AccessSecurityKey
        );
    }

    public string GenerateRefreshToken()
        => Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

    /// <summary>
    /// Generates a JSON Web Token (JWT) based on the provided claims and expiration time.
    /// </summary>
    /// <remarks>The generated token is signed using the HMAC-SHA256 algorithm and a symmetric security key.</remarks>
    /// <param name="claims">A list of claims to include in the token. These claims represent the identity and additional metadata of the
    /// token's subject.</param>
    /// <param name="expirationMinutes">The number of minutes until the token expires. Must be a positive integer.</param>
    /// <param name="securityKey">The security key used for signing the token.</param>
    /// <returns>A string representation of the generated JWT.</returns>
    private static string WriteToken(List<Claim> claims, int expirationMinutes, string securityKey)
    {
        byte[] keyBytes = Encoding.UTF8.GetBytes(securityKey);

        // Set the key and algorithm for token signing
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(keyBytes),
            SecurityAlgorithms.HmacSha256);

        // Set the description for token creation
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(expirationMinutes),
            SigningCredentials = signingCredentials
        };

        // Create token
        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(securityToken);
    }
}
