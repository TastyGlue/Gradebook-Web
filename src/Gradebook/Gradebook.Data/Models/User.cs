namespace Gradebook.Data.Models;

public class User : IdentityUser<Guid>
{
    public bool IsActive { get; set; }

    public ICollection<Profile> Profiles { get; set; } = [];
}
