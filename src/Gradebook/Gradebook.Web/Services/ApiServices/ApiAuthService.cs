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

    public async Task LoginProfile(LoginProfile loginProfile)
    {
        throw new NotImplementedException("This method is not implemented yet.");
    }

    public async Task LoginWithProfileToken(LoginProfile request)
    {
        throw new NotImplementedException();
    }
}
