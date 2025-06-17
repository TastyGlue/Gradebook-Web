namespace Gradebook.Web.Services.ApiServices
{
    public class ApiSubjectService : IApiSubjectService
    {
        private readonly HttpClientService _httpClientService;
        private readonly TokenService _tokenService;

        public ApiSubjectService(HttpClientService httpClientService, TokenService tokenService)
        {
            _httpClientService = httpClientService;
            _tokenService = tokenService;
        }

        public async Task<CustomResult<IEnumerable<SubjectDto>>> GetSubjects()
        {
            var token = await _tokenService.GetToken(Constants.ACCESS_TOKEN_KEY);
            var client = _httpClientService.CreateApiClient(token);
            var response = await client.GetAsync("api/subjects");
            var content = await response.Content.ReadAsStringAsync();
            return CustomResultUtils.GetApiResponse<IEnumerable<SubjectDto>>(response, content);
        }

        public async Task<CustomResult<SubjectDto>> GetSubject(Guid id)
        {
            var token = await _tokenService.GetToken(Constants.ACCESS_TOKEN_KEY);
            var client = _httpClientService.CreateApiClient(token);
            var response = await client.GetAsync($"api/subjects/{id}");
            var content = await response.Content.ReadAsStringAsync();
            return CustomResultUtils.GetApiResponse<SubjectDto>(response, content);
        }

        public async Task<CustomResult<SubjectDto>> CreateSubject(SubjectDto dto)
        {
            var token = await _tokenService.GetToken(Constants.ACCESS_TOKEN_KEY);
            var client = _httpClientService.CreateApiClient(token);
            var response = await client.PostAsJsonAsync("api/subjects", dto);
            var content = await response.Content.ReadAsStringAsync();
            return CustomResultUtils.GetApiResponse<SubjectDto>(response, content);
        }

        public async Task<CustomResult<SubjectDto>> EditSubject(Guid id, SubjectDto dto)
        {
            var token = await _tokenService.GetToken(Constants.ACCESS_TOKEN_KEY);
            var client = _httpClientService.CreateApiClient(token);
            var response = await client.PutAsJsonAsync($"api/subjects/{id}", dto);
            var content = await response.Content.ReadAsStringAsync();
            return CustomResultUtils.GetApiResponse<SubjectDto>(response, content);
        }
    }
}
