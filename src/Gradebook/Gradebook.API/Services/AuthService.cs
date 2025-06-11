namespace Gradebook.API.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly ITokenService _tokenService;
    private readonly GradebookDbContext _context;
    private readonly IProfileContexService _profileContexService;

    public AuthService(UserManager<User> userManager, ITokenService tokenService, GradebookDbContext context, IProfileContexService profileContexService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _context = context;
        _profileContexService = profileContexService;
    }

    public async Task<CustomResult> LoginWithCredentials(LoginCredentials credentials)
    {
        (string email, string password) = (credentials.Email, credentials.Password);

        var user = await _context.Users
            .Include(u => u.Profiles)
            .FirstOrDefaultAsync(u => u.NormalizedEmail == email.ToUpper());

        if (user is null)
        {
            var error = new ErrorResult($"No user with the email: '{email}'", ErrorCodes.LOGIN_CREDENTIALS);
            return new(error);
        }

        var isPasswordValid = await _userManager.CheckPasswordAsync(user, password);

        if (!isPasswordValid)
        {
            var error = new ErrorResult("Invalid password", ErrorCodes.LOGIN_CREDENTIALS);
            return new(error);
        }

        return _tokenService.GenerateAuthToken(user);
    }

    public async Task<CustomResult> LoginProfile(LoginProfile profileInfo, Guid userId)
    {
        (RoleType roleType, Guid profileId) = (profileInfo.RoleType, profileInfo.ProfileId);

        var profileResult = await _profileContexService.GetProfileFromRoleType(roleType, profileId, userId);

        if (!profileResult.Succeeded)
            return profileResult;

        var profile = profileResult.Value!;

        return _tokenService.GenerateAccessToken(profile);
    }
}
