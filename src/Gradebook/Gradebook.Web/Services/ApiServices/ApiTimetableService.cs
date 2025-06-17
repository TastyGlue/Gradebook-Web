namespace Gradebook.Web.Services.ApiServices
{
    public class ApiTimetableService : IApiTimetableService
    {
        private readonly HttpClientService _httpClientService;
        private readonly TokenService _tokenService;

        public ApiTimetableService(HttpClientService httpClientService, TokenService tokenService)
        {
            _httpClientService = httpClientService;
            _tokenService = tokenService;
        }

        public async Task<CustomResult<IEnumerable<TimetableDto>>> GetTimetables()
        {
            var token = await _tokenService.GetToken(Constants.ACCESS_TOKEN_KEY);
            var client = _httpClientService.CreateApiClient(token);
            var response = await client.GetAsync("api/timetables");
            var content = await response.Content.ReadAsStringAsync();
            return CustomResultUtils.GetApiResponse<IEnumerable<TimetableDto>>(response, content);
        }

        public async Task<CustomResult<TimetableDto>> GetTimetable(Guid id)
        {
            var token = await _tokenService.GetToken(Constants.ACCESS_TOKEN_KEY);
            var client = _httpClientService.CreateApiClient(token);
            var response = await client.GetAsync($"api/timetables/{id}");
            var content = await response.Content.ReadAsStringAsync();
            return CustomResultUtils.GetApiResponse<TimetableDto>(response, content);
        }

        public async Task<CustomResult<TimetableDto>> CreateTimetable(TimetableDto dto)
        {
            var token = await _tokenService.GetToken(Constants.ACCESS_TOKEN_KEY);
            var client = _httpClientService.CreateApiClient(token);
            var response = await client.PostAsJsonAsync("api/timetables", dto);
            var content = await response.Content.ReadAsStringAsync();
            return CustomResultUtils.GetApiResponse<TimetableDto>(response, content);
        }

        public async Task<CustomResult<TimetableDto>> EditTimetable(Guid id, TimetableDto dto)
        {
            var token = await _tokenService.GetToken(Constants.ACCESS_TOKEN_KEY);
            var client = _httpClientService.CreateApiClient(token);
            var response = await client.PutAsJsonAsync($"api/timetables/{id}", dto);
            var content = await response.Content.ReadAsStringAsync();
            return CustomResultUtils.GetApiResponse<TimetableDto>(response, content);
        }
    }
}