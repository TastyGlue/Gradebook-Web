namespace Gradebook.API.Interfaces;

public interface IAuthService
{
    Task<CustomResult> LoginWithCredentials(LoginCredentials credentials);

    Task<CustomResult> LoginProfile(LoginProfile profileInfo, Guid userId);
}
