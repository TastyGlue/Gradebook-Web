namespace Gradebook.Data.Models;

public class User : IdentityUser<Guid>
{
    public string FullName { get; set; } = default!;

    public bool IsActive { get; set; }

    public ICollection<Profile> Profiles { get; set; } = [];
}
