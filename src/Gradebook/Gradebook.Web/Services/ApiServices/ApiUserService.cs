namespace Gradebook.Web.Services.ApiServices;

public class ApiUserService : IApiUserService
{
    private readonly HttpClientService _httpClientService;
    private readonly TokenService _tokenService;

    public ApiUserService(HttpClientService httpClientService, TokenService tokenService)
    {
        _httpClientService = httpClientService;
        _tokenService = tokenService;
    }

    public async Task<CustomResult<IEnumerable<UserDto>>> GetUsers()
    {
        var token = await _tokenService.GetToken(Constants.ACCESS_TOKEN_KEY);
        var client = _httpClientService.CreateApiClient(token);

        string apiEndpoint = "api/users";

        var response = await client.GetAsync(apiEndpoint);
        var content = await response.Content.ReadAsStringAsync();

        return CustomResultUtils.GetApiResponse<IEnumerable<UserDto>>(response, content);
    }
}
