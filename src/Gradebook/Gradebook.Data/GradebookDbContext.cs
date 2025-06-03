namespace Gradebook.Data;

public class GradebookDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
}
