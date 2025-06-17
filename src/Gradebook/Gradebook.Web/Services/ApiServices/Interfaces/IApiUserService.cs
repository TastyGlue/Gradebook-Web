namespace Gradebook.Web.Services.ApiServices.Interfaces;

public interface IApiUserService
{
    Task<CustomResult<IEnumerable<UserDto>>> GetUsers();
}
