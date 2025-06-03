namespace Gradebook.Data.Models;

[Index(nameof(UserId), IsUnique = false)]
public abstract class Profile
{
    public Guid Id { get; set; }

    [ForeignKey(nameof(User))]
    public Guid UserId { get; set; }

    public User User { get; set; } = default!;

    public RoleType RoleType { get; set; }

    public string FullName { get; set; } = default!;

    public bool IsActive { get; set; }
}
