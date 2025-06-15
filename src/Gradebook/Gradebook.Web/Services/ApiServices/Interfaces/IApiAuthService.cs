namespace Gradebook.Web.Services.ApiServices.Interfaces;

public interface IApiAuthService
{
    Task<CustomResult<string>> LoginWithCredentials(LoginCredentials request);

    Task<CustomResult<string>> LoginProfile(LoginProfile request, string token);

    Task<CustomResult<string>> LoginWithProfileToken(LoginProfile request, string accessToken);
}
