namespace Gradebook.Web.Services.ApiServices
{
    public class ApiParentService : IApiParentService
    {
        private readonly HttpClientService _httpClientService;
        private readonly TokenService _tokenService;

        public ApiParentService(HttpClientService httpClientService, TokenService tokenService)
        {
            _httpClientService = httpClientService;
            _tokenService = tokenService;
        }

        public async Task<CustomResult<IEnumerable<ParentDto>>> GetParents()
        {
            var token = await _tokenService.GetToken(Constants.ACCESS_TOKEN_KEY);
            var client = _httpClientService.CreateApiClient(token);
            var response = await client.GetAsync("api/parents");
            var content = await response.Content.ReadAsStringAsync();
            return CustomResultUtils.GetApiResponse<IEnumerable<ParentDto>>(response, content);
        }

        public async Task<CustomResult<ParentDto>> GetParent(Guid id)
        {
            var token = await _tokenService.GetToken(Constants.ACCESS_TOKEN_KEY);
            var client = _httpClientService.CreateApiClient(token);
            var response = await client.GetAsync($"api/parents/{id}");
            var content = await response.Content.ReadAsStringAsync();
            return CustomResultUtils.GetApiResponse<ParentDto>(response, content);
        }

        public async Task<CustomResult<ParentDto>> CreateParent(CreateUserRoleDto<ParentDto> dto)
        {
            var token = await _tokenService.GetToken(Constants.ACCESS_TOKEN_KEY);
            var client = _httpClientService.CreateApiClient(token);
            var response = await client.PostAsJsonAsync("api/parents", dto);
            var content = await response.Content.ReadAsStringAsync();
            return CustomResultUtils.GetApiResponse<ParentDto>(response, content);
        }

        public async Task<CustomResult<ParentDto>> EditParent(Guid id, CreateUserRoleDto<ParentDto> dto)
        {
            var token = await _tokenService.GetToken(Constants.ACCESS_TOKEN_KEY);
            var client = _httpClientService.CreateApiClient(token);
            var response = await client.PutAsJsonAsync($"api/parents/{id}", dto);
            var content = await response.Content.ReadAsStringAsync();
            return CustomResultUtils.GetApiResponse<ParentDto>(response, content);
        }
    }
}