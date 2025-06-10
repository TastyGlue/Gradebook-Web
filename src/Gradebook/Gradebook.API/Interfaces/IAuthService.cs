namespace Gradebook.API.Interfaces;

public interface IAuthService
{
    Task<CustomResult> LoginWithCredentials(LoginCredentials credentials);
}
