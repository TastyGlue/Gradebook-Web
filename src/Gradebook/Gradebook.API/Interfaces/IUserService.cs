namespace Gradebook.API.Interfaces
{
    public interface IUserService
    {
        Task<CustomResult> GetUser(Guid id);
        Task<CustomResult> GetAllUsersAsync();
        Task<CustomResult> CreateUser(UserDto userDto);
        Task<CustomResult> UpdateUser(Guid id, UserDto userDto);
        Task<CustomResult> DeleteUser(Guid id);
        Task<CustomResult> ResetPassword(Guid id);
    }
}
