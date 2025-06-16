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

    public async Task<CustomResult<HeadmasterDto>> GetHeadmaster(Guid id)
    {
        var token = await _tokenService.GetToken(Constants.ACCESS_TOKEN_KEY);
        var client = _httpClientService.CreateApiClient(token);

        string apiEndpoint = $"api/headmaster/{id}";

        var response = await client.GetAsync(apiEndpoint);
        var content = await response.Content.ReadAsStringAsync();

        return CustomResultUtils.GetApiResponse<HeadmasterDto>(response, content);
    }

    public async Task<CustomResult<HeadmasterDto>> CreateHeadmaster(HeadmasterDto dto)
    {
        var token = await _tokenService.GetToken(Constants.ACCESS_TOKEN_KEY);
        var client = _httpClientService.CreateApiClient(token);

        string apiEndpoint = "api/headmaster";

        var response = await client.PostAsJsonAsync(apiEndpoint, dto);
        var content = await response.Content.ReadAsStringAsync();

        return CustomResultUtils.GetApiResponse<HeadmasterDto>(response, content);
    }

    public async Task<CustomResult<HeadmasterDto>> EditHeadmaster(Guid id, HeadmasterDto dto)
    {
        var token = await _tokenService.GetToken(Constants.ACCESS_TOKEN_KEY);
        var client = _httpClientService.CreateApiClient(token);

        string apiEndpoint = $"api/headmaster/{id}";

        var response = await client.PutAsJsonAsync(apiEndpoint, dto);
        var content = await response.Content.ReadAsStringAsync();

        return CustomResultUtils.GetApiResponse<HeadmasterDto>(response, content);
    }
}
