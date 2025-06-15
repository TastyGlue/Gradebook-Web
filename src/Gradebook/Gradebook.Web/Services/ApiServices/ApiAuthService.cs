namespace Gradebook.Web.Services.ApiServices;

public class ApiAuthService : IApiAuthService
{
    private readonly TokenService _tokenService;
    private readonly HttpClientService _httpClientService;

    public ApiAuthService(TokenService tokenService, HttpClientService httpClientService)
    {
        _tokenService = tokenService;
        _httpClientService = httpClientService;
    }

    public async Task<CustomResult<string>> LoginWithCredentials(LoginCredentials credentials)
    {
        var client = _httpClientService.CreateApiClient();

        string apiEndpoint = "auth/login/credentials";

        var response = await client.PostAsJsonAsync(apiEndpoint, credentials);
        var content = await response.Content.ReadAsStringAsync();

        return CustomResultUtils.GetApiResponse<string>(response, content);
    }

    public async Task<CustomResult<string>> LoginProfile(LoginProfile loginProfile, string token)
    {
        var client = _httpClientService.CreateApiClient(token);

        string apiEndpoint = "auth/login/profile";

        var response = await client.PostAsJsonAsync(apiEndpoint, loginProfile);
        var content = await response.Content.ReadAsStringAsync();

        return CustomResultUtils.GetApiResponse<string>(response, content);
    }

    public async Task LoginWithProfileToken(LoginProfile request)
    {
        throw new NotImplementedException();
    }
}
