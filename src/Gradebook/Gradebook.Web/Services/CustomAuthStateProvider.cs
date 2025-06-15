using System.Security.Claims;

namespace Gradebook.Web.Services;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly ProtectedLocalStorage _localStorage;
    private readonly JwtSettings _jwtSettings;
    private readonly HttpClientService _httpClientService;
    private readonly NavigationManager _navigationManager;
    private readonly IApiAuthService _apiAuthService;

    public CustomAuthStateProvider(ProtectedLocalStorage localStorage, IOptions<JwtSettings> jwtSettingsOptions, NavigationManager navigationManager, HttpClientService httpClientService, IApiAuthService apiAuthService)
    {
        _localStorage = localStorage;
        _jwtSettings = jwtSettingsOptions.Value;
        _navigationManager = navigationManager;
        _httpClientService = httpClientService;
        _apiAuthService = apiAuthService;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var accessToken = (await _localStorage.GetAsync<string>(Constants.ACCESS_TOKEN_KEY)).Value;

        var identity = new ClaimsIdentity();

        if (!string.IsNullOrWhiteSpace(accessToken))
        {
            var claims = TokenUtils.ParseClaimsFromToken(accessToken);

            if (TokenUtils.ValidateToken(accessToken, _jwtSettings.AccessSecurityKey))
            {
                identity = new ClaimsIdentity(claims, "jwtAuth");
            }
            else
            {
                // Attempt to refresh the token
                if (await TryRefreshToken(accessToken, identity))
                {
                    accessToken = (await _localStorage.GetAsync<string>(Constants.ACCESS_TOKEN_KEY)).Value; // Get new token
                    claims = TokenUtils.ParseClaimsFromToken(accessToken!);
                    identity = new ClaimsIdentity(claims, "jwtAuth");
                }
                else
                {
                    // Redirect to login if refresh token is also expired
                    _navigationManager.NavigateTo("/account/login");
                }
            }
        }

        return new AuthenticationState(new ClaimsPrincipal(identity));
    }

    private async Task<bool> TryRefreshToken(string accessToken, ClaimsIdentity identity)
    {
        // Check if the token is expired
        var isTokenExpired = TokenUtils.IsTokenExpired(new ClaimsPrincipal(identity), TimeSpan.FromDays(_jwtSettings.RefreshTokenExpirationDays));
        if (isTokenExpired is null)
            return false;

        // Check if the refresh token expiration is still valid
        if (isTokenExpired.Value)
        {
            // Call the API to refresh the token
            var roleTypeString = identity.FindFirst(Claims.ROLE)?.Value;
            if (roleTypeString is null)
                return false;

            var roleType = Enum.Parse<RoleType>(roleTypeString, true);

            var profileIdString = identity.FindFirst(Claims.PROFILE_ID)?.Value;
            if (profileIdString is null)
                return false;

            var profileId = Guid.Parse(profileIdString);

            var request = new LoginProfile
            {
                ProfileId = profileId,
                RoleType = roleType
            };

            var result = await _apiAuthService.LoginWithProfileToken(request, accessToken);

            if (result.Succeeded)
            {
                var token = result.Value!;

                // Store the new token in LocalStorage
                await _localStorage.SetAsync(Constants.ACCESS_TOKEN_KEY, token);

                return true; // Successfully refreshed token
            }
        }

        return false;
    }
}
