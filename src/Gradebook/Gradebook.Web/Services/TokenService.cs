namespace Gradebook.Web.Services;

public class TokenService
{
    private readonly JwtSettings _jwtSettings;
    private readonly ProtectedLocalStorage _localStorage;
    private readonly NavigationManager _navigationManager;

    public TokenService(IOptions<JwtSettings> jwtSettingsMonitor, ProtectedLocalStorage localStorage, NavigationManager navigationManager)
    {
        _jwtSettings = jwtSettingsMonitor.Value;
        _localStorage = localStorage;
        _navigationManager = navigationManager;
    }

    public async Task<string> GetToken(string tokenKey = Constants.ACCESS_TOKEN_KEY, bool navigateOnMissingToken = true)
    {
        var token = (await _localStorage.GetAsync<string>(tokenKey)).Value;
        if (token is null)
        {
            if (navigateOnMissingToken)
            {
                _navigationManager.NavigateTo("/account/login", forceLoad: true);
            }
            
            return string.Empty;
        }

        return token;
    }
}
