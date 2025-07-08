
namespace Gradebook.Web.Services.ApiServices.Interfaces;

public interface IApiUserService
{
    Task<CustomResult<IEnumerable<UserDto>>> GetUsers();
    Task<CustomResult<string>> ResetPassword(Guid id);
}
