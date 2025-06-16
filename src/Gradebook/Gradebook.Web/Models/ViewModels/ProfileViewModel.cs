namespace Gradebook.Web.Models.ViewModels;

public class ProfileViewModel
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public UserViewModel? User { get; set; } = default!;

    public RoleType RoleType { get; set; }

    public bool IsActive { get; set; } = true;
}
