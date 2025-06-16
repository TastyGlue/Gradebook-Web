namespace Gradebook.Web.Services.ApiServices;

public class ApiSchoolService : IApiSchoolService
{
    private readonly HttpClientService _httpClientService;
    private readonly TokenService _tokenService;

    public ApiSchoolService(HttpClientService httpClientService, TokenService tokenService)
    {
        _httpClientService = httpClientService;
        _tokenService = tokenService;
    }

    public async Task<CustomResult<IEnumerable<SchoolDto>>> GetSchools()
    {
        var token = await _tokenService.GetToken(Constants.ACCESS_TOKEN_KEY);
        var client = _httpClientService.CreateApiClient(token);

        string apiEndpoint = "api/school";

        var response = await client.GetAsync(apiEndpoint);
        var content = await response.Content.ReadAsStringAsync();

        return CustomResultUtils.GetApiResponse<IEnumerable<SchoolDto>>(response, content);
    }

    public async Task<CustomResult<SchoolDto>> GetSchool(Guid id)
    {
        var token = await _tokenService.GetToken(Constants.ACCESS_TOKEN_KEY);
        var client = _httpClientService.CreateApiClient(token);

        string apiEndpoint = $"api/school/{id}";

        var response = await client.GetAsync(apiEndpoint);
        var content = await response.Content.ReadAsStringAsync();

        return CustomResultUtils.GetApiResponse<SchoolDto>(response, content);
    }

    public async Task<CustomResult<SchoolDto>> CreateSchool(SchoolDto dto)
    {
        var token = await _tokenService.GetToken(Constants.ACCESS_TOKEN_KEY);
        var client = _httpClientService.CreateApiClient(token);

        string apiEndpoint = "api/school";

        var response = await client.PostAsJsonAsync(apiEndpoint, dto);
        var content = await response.Content.ReadAsStringAsync();

        return CustomResultUtils.GetApiResponse<SchoolDto>(response, content);
    }

    public async Task<CustomResult<SchoolDto>> EditSchool(Guid id, SchoolDto dto)
    {
        var token = await _tokenService.GetToken(Constants.ACCESS_TOKEN_KEY);
        var client = _httpClientService.CreateApiClient(token);

        string apiEndpoint = $"api/school/{id}";

        var response = await client.PutAsJsonAsync(apiEndpoint, dto);
        var content = await response.Content.ReadAsStringAsync();

        return CustomResultUtils.GetApiResponse<SchoolDto>(response, content);
    }
}
