
namespace Gradebook.Web.Services.ApiServices
{
    public class ApiStudentGradeService : IApiStudentGradeService
    {
        private readonly HttpClientService _httpClientService;
        private readonly TokenService _tokenService;
        public ApiStudentGradeService(HttpClientService httpClientService, TokenService tokenService)
        {
            _httpClientService = httpClientService;
            _tokenService = tokenService;
        }

        public async Task<CustomResult<IEnumerable<GradeDto>>> GetStudentGrades(Guid id)
        {
            var token = await _tokenService.GetToken(Constants.ACCESS_TOKEN_KEY);
            var client = _httpClientService.CreateApiClient(token);

            string apiEndpoint = $"api/grades/student/{id}";

            var response = await client.GetAsync(apiEndpoint);
            var content = await response.Content.ReadAsStringAsync();

            return CustomResultUtils.GetApiResponse<IEnumerable<GradeDto>>(response, content);


        }
      
    }


}
