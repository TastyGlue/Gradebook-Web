namespace Gradebook.Web.Services.ApiServices.Interfaces;

public interface IApiAuthService
{
    Task<CustomResult<string>> LoginWithCredentials(LoginCredentials request);

    Task<CustomResult<string>> LoginProfile(LoginProfile loginProfile, string token);

    Task LoginWithProfileToken(LoginProfile request);
}
