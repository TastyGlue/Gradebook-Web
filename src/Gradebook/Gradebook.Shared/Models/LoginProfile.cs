namespace Gradebook.Shared.Models;

public class LoginProfile
{
    public RoleType RoleType { get; set; }

    public Guid ProfileId { get; set; }
}