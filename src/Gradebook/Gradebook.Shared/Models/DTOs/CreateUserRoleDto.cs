namespace Gradebook.Shared.Models.DTOs;

public class CreateUserRoleDto<T> where T : class, new()
{
    public T Role { get; set; } = new();

    public UserDto User { get; set; } = new();

    public bool FromNewUser { get; set; }
}