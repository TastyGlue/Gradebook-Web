namespace Gradebook.API.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly ITokenService _tokenService;
    private readonly GradebookDbContext _context;

    public AuthService(UserManager<User> userManager, ITokenService tokenService, GradebookDbContext context)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _context = context;
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
}
