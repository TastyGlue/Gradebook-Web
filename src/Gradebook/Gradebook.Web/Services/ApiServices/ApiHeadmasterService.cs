namespace Gradebook.Web.Services.ApiServices;

public class ApiHeadmasterService : IApiHeadmasterService
{
    private readonly HttpClientService _httpClientService;
    private readonly TokenService _tokenService;

    public ApiHeadmasterService(HttpClientService httpClientService, TokenService tokenService)
    {
        _httpClientService = httpClientService;
        _tokenService = tokenService;
    }

    public async Task<CustomResult<IEnumerable<HeadmasterDto>>> GetHeadmasters()
    {
        var token = await _tokenService.GetToken(Constants.ACCESS_TOKEN_KEY);
        var client = _httpClientService.CreateApiClient(token);

        string apiEndpoint = "api/headmaster";

        var response = await client.GetAsync(apiEndpoint);
        var content = await response.Content.ReadAsStringAsync();

        return CustomResultUtils.GetApiResponse<IEnumerable<HeadmasterDto>>(response, content);
    }
}
