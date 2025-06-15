namespace Gradebook.Web.Services.ApiServices.Interfaces;

public interface IApiAuthService
{
    Task<CustomResult<string>> LoginWithCredentials(LoginCredentials request);

    Task LoginProfile(LoginProfile request);

    Task LoginWithProfileToken(LoginProfile request);
}
