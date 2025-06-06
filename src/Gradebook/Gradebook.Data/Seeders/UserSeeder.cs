using static Gradebook.Shared.Constants.SeedConstants;

namespace Gradebook.Data.Seeders;

public class UserSeeder : IDataSeeder
{
    private readonly UserManager<User> _userManager;

    public UserSeeder(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public int Order => 1;

    public async Task Seed(DbContext context)
    {
        var users = GetUsers();

        foreach (var user in users)
        {
            var existingUser = await _userManager.FindByIdAsync(user.Id.ToString());
            if (existingUser == null)
            {
                var result = await _userManager.CreateAsync(user, Constants.DEFAULT_PASSWORD);
                if (!result.Succeeded)
                {
                    throw new Exception($"Failed to seed user {user.UserName}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }
        }
    }

    private static List<User> GetUsers()
    {
        return [
            new() { Id = Guid.Parse(USER_AS_ID), UserName = "alice.smith@example.com", Email = "alice.smith@example.com", FullName = "Alice Smith", IsActive = true, EmailConfirmed = true },
            new() { Id = Guid.Parse(USER_BJ_ID), UserName = "bob.johnson@example.com", Email = "bob.johnson@example.com", FullName = "Bob Johnson", IsActive = true, EmailConfirmed = true },
            new() { Id = Guid.Parse(USER_CW_ID), UserName = "carol.white@example.com", Email = "carol.white@example.com", FullName = "Carol White", IsActive = true, EmailConfirmed = true },
            new() { Id = Guid.Parse(USER_DB_ID), UserName = "dave.brown@example.com", Email = "dave.brown@example.com", FullName = "Dave Brown", IsActive = true, EmailConfirmed = true },
            new() { Id = Guid.Parse(USER_ED_ID), UserName = "emma.davis@example.com", Email = "emma.davis@example.com", FullName = "Emma Davis", IsActive = true, EmailConfirmed = true },
            new() { Id = Guid.Parse(USER_FW_ID), UserName = "frank.wilson@example.com", Email = "frank.wilson@example.com", FullName = "Frank Wilson", IsActive = true, EmailConfirmed = true },
            new() { Id = Guid.Parse(USER_GT_ID), UserName = "grace.taylor@example.com", Email = "grace.taylor@example.com", FullName = "Grace Taylor", IsActive = true, EmailConfirmed = true },
            new() { Id = Guid.Parse(USER_HM_ID), UserName = "henry.moore@example.com", Email = "henry.moore@example.com", FullName = "Henry Moore", IsActive = true, EmailConfirmed = true },
            new() { Id = Guid.Parse(USER_IJ_ID), UserName = "irene.jackson@example.com", Email = "irene.jackson@example.com", FullName = "Irene Jackson", IsActive = true, EmailConfirmed = true },
            new() { Id = Guid.Parse(USER_JM_ID), UserName = "jack.martin@example.com", Email = "jack.martin@example.com", FullName = "Jack Martin", IsActive = true, EmailConfirmed = true },
            new() { Id = Guid.Parse(USER_KT_ID), UserName = "kate.thomas@example.com", Email = "kate.thomas@example.com", FullName = "Kate Thomas", IsActive = true, EmailConfirmed = true },
            new() { Id = Guid.Parse(USER_LG_ID), UserName = "leo.garcia@example.com", Email = "leo.garcia@example.com", FullName = "Leo Garcia", IsActive = true, EmailConfirmed = true },
            new() { Id = Guid.Parse(USER_MM_ID), UserName = "mia.martinez@example.com", Email = "mia.martinez@example.com", FullName = "Mia Martinez", IsActive = true, EmailConfirmed = true },
            new() { Id = Guid.Parse(USER_TM_ID), UserName = "thomas.martin@example.com", Email = "thomas.martin@example.com", FullName = "Thomas Martin", IsActive = true, EmailConfirmed = true },
            new() { Id = Guid.Parse(USER_JT_ID), UserName = "jane.thomas@example.com", Email = "jane.thomas@example.com", FullName = "Jane Thomas", IsActive = true, EmailConfirmed = true }
        ];
    }
}