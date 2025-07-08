namespace Gradebook.Web.Services.ApiServices
{
    public class ApiAbsencesService : IApiAbsencesService
    {
        private readonly HttpClientService _httpClientService;
        private readonly TokenService _tokenService;
        public ApiAbsencesService(HttpClientService httpClientService, TokenService tokenService)
        {
            _httpClientService = httpClientService;
            _tokenService = tokenService;
        }
        public async Task<CustomResult<AbsenceDto>> CreateAbsence(AbsenceDto dto)
        {
            var token = await _tokenService.GetToken(Constants.ACCESS_TOKEN_KEY);
            var client = _httpClientService.CreateApiClient(token);
            var response = await client.PostAsJsonAsync("api/absences", dto);
            var content = await response.Content.ReadAsStringAsync();
            return CustomResultUtils.GetApiResponse<AbsenceDto>(response, content);
        }

        public async Task<CustomResult<AbsenceDto>> UpdateAbsence(Guid id, AbsenceDto dto)
        {
            var token = await _tokenService.GetToken(Constants.ACCESS_TOKEN_KEY);
            var client = _httpClientService.CreateApiClient(token);
            var response = await client.PutAsJsonAsync($"api/absences/{id}", dto);
            var content = await response.Content.ReadAsStringAsync();
            return CustomResultUtils.GetApiResponse<AbsenceDto>(response, content);
        }

        // Optional delete
        public async Task<CustomResult<string>> DeleteAbsence(Guid id)
        {
            var token = await _tokenService.GetToken(Constants.ACCESS_TOKEN_KEY);
            var client = _httpClientService.CreateApiClient(token);
            var response = await client.DeleteAsync($"api/absences/{id}");
            var content = await response.Content.ReadAsStringAsync();
            return CustomResultUtils.GetApiResponse<string>(response, content);
        }

        public async Task<CustomResult<IEnumerable<AbsenceDto>>> GetStudentAbsences(Guid id)
        {
            var token = await _tokenService.GetToken(Constants.ACCESS_TOKEN_KEY);
            var client = _httpClientService.CreateApiClient(token);

            string apiEndpoint = $"api/absences/student/{id}";

            var response = await client.GetAsync(apiEndpoint);
            var content = await response.Content.ReadAsStringAsync();

            return CustomResultUtils.GetApiResponse<IEnumerable<AbsenceDto>>(response, content);
        }
    }
}
