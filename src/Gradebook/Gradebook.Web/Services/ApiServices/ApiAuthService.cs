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

    public async Task<CustomResult<string>> LoginWithCredentials(LoginCredentials request)
    {
        var client = _httpClientService.CreateApiClient();

        string apiEndpoint = "auth/login/credentials";

        var response = await client.PostAsJsonAsync(apiEndpoint, request);
        var content = await response.Content.ReadAsStringAsync();

        return CustomResultUtils.GetApiResponse<string>(response, content);
    }

    public async Task<CustomResult<string>> LoginProfile(LoginProfile request, string authToken)
    {
        var client = _httpClientService.CreateApiClient(authToken);

        string apiEndpoint = "auth/login/profile";

        var response = await client.PostAsJsonAsync(apiEndpoint, request);
        var content = await response.Content.ReadAsStringAsync();

        return CustomResultUtils.GetApiResponse<string>(response, content);
    }

    public async Task<CustomResult<string>> LoginWithProfileToken(LoginProfile request, string accessToken)
    {
        var client = _httpClientService.CreateApiClient(accessToken);

        string apiEndpoint = "auth/login/profile/token";

        var response = await client.PostAsJsonAsync(apiEndpoint, request);
        var content = await response.Content.ReadAsStringAsync();

        return CustomResultUtils.GetApiResponse<string>(response, content);
    }
}
