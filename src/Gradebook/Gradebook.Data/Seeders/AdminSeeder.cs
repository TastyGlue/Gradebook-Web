using static Gradebook.Shared.Constants.SeedConstants;

namespace Gradebook.Data.Seeders;

public class AdminSeeder : IDataSeeder
{
    public int Order => 2;

    public async Task Seed(DbContext context)
    {
        var admins = GetAdmins();

        foreach (var admin in admins)
        {
            bool exists = await context.Set<Admin>().AnyAsync(x => x.Id == admin.Id);
            if (!exists)
            {
                await context.Set<Admin>().AddAsync(admin);
                await context.SaveChangesAsync();
            }
        }
    }

    private static List<Admin> GetAdmins()
    {
        return [
            new Admin
            {
                Id = Guid.Parse(ADMIN_ID),
                UserId = Guid.Parse(USER_AS_ID),
                RoleType = RoleType.Admin,
                BusinessEmail = "admin.business@example.com",
                IsActive = true
            }
        ];
    }
}