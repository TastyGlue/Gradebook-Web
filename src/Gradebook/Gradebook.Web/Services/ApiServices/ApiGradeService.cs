namespace Gradebook.Web.Services.ApiServices
{
    public class ApiGradeService : IApiGradeService
    {
        private readonly HttpClientService _httpClientService;
        private readonly TokenService _tokenService;
        public ApiGradeService(HttpClientService httpClientService, TokenService tokenService)
        {
            _httpClientService = httpClientService;
            _tokenService = tokenService;
        }

        public async Task<CustomResult> GetGrade(Guid id)
        {
            var token = await _tokenService.GetToken(Constants.ACCESS_TOKEN_KEY);
            var client = _httpClientService.CreateApiClient(token);
            var response = await client.GetAsync($"api/grades/{id}");
            var content = await response.Content.ReadAsStringAsync();
            return CustomResultUtils.GetApiResponse<GradeDto>(response, content);
        }

        public async Task<CustomResult<List<GradeDto>>> GetGradesByStudentId(Guid studentId)
        {
            var token = await _tokenService.GetToken(Constants.ACCESS_TOKEN_KEY);
            var client = _httpClientService.CreateApiClient(token);
            var response = await client.GetAsync($"api/grades/student/{studentId}");
            var content = await response.Content.ReadAsStringAsync();
            return CustomResultUtils.GetApiResponse<List<GradeDto>>(response, content);
        }

        public async Task<CustomResult<IEnumerable<GradeDto>>> GetAllGradesAsync()
        {
            var token = await _tokenService.GetToken(Constants.ACCESS_TOKEN_KEY);
            var client = _httpClientService.CreateApiClient(token);
            var response = await client.GetAsync("api/grades");
            var content = await response.Content.ReadAsStringAsync();
            return CustomResultUtils.GetApiResponse<IEnumerable<GradeDto>>(response, content);
        }

        public async Task<CustomResult<GradeDto>> CreateGrade(GradeDto dto)
        {
            var token = await _tokenService.GetToken(Constants.ACCESS_TOKEN_KEY);
            var client = _httpClientService.CreateApiClient(token);
            var response = await client.PostAsJsonAsync("api/grades", dto);
            var content = await response.Content.ReadAsStringAsync();
            return CustomResultUtils.GetApiResponse<GradeDto>(response, content);
        }

        public async Task<CustomResult<GradeDto>> UpdateGrade(Guid id, GradeDto dto)
        {
            var token = await _tokenService.GetToken(Constants.ACCESS_TOKEN_KEY);
            var client = _httpClientService.CreateApiClient(token);
            var response = await client.PutAsJsonAsync($"api/grades/{id}", dto);
            var content = await response.Content.ReadAsStringAsync();
            return CustomResultUtils.GetApiResponse<GradeDto>(response, content);
        }

        public async Task<CustomResult<string>> DeleteGrade(Guid id)
        {
            var token = await _tokenService.GetToken(Constants.ACCESS_TOKEN_KEY);
            var client = _httpClientService.CreateApiClient(token);
            var response = await client.DeleteAsync($"api/grades/{id}");
            var content = await response.Content.ReadAsStringAsync();
            return CustomResultUtils.GetApiResponse<string>(response, content);
        }
    }
}