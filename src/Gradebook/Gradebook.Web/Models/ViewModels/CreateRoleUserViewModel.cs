namespace Gradebook.Web.Models.ViewModels;

public class CreateRoleUserViewModel<T> where T : class, new()
{
    public T Role { get; set; } = new();

    public UserViewModel User { get; set; } = new();

    public bool FromNewUser { get; set; }
}
