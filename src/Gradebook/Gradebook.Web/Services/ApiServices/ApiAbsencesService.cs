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

        public async Task<CustomResult<IEnumerable<AbsenceDto>>> GetStudentAbsences(Guid id)
        {
            var token = await _tokenService.GetToken(Constants.ACCESS_TOKEN_KEY);
            var client = _httpClientService.CreateApiClient(token);

            string apiEndpoint = $"api/absence/student/{id}";

            var response = await client.GetAsync(apiEndpoint);
            var content = await response.Content.ReadAsStringAsync();

            return CustomResultUtils.GetApiResponse<IEnumerable<AbsenceDto>>(response, content);
        }
    }
}
