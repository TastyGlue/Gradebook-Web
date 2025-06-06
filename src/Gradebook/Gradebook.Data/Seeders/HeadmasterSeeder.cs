using static Gradebook.Shared.Constants.SeedConstants;

namespace Gradebook.Data.Seeders;

public class HeadmasterSeeder : IDataSeeder
{
    public int Order => 4;

    public async Task Seed(DbContext context)
    {
        var headmasters = GetHeadmasters();

        foreach (var headmaster in headmasters)
        {
            bool exists = await context.Set<Headmaster>().AnyAsync(x => x.Id == headmaster.Id);
            if (!exists)
            {
                await context.Set<Headmaster>().AddAsync(headmaster);
                await context.SaveChangesAsync();
            }
        }
    }

    private static List<Headmaster> GetHeadmasters()
    {
        return [
            new Headmaster
            {
                Id = Guid.Parse(HEADMASTER_RIVERSIDE_ID),
                UserId = Guid.Parse(USER_BJ_ID),
                SchoolId = Guid.Parse(SCHOOL_RIVERSIDE_ID),
                RoleType = RoleType.Headmaster,
                BusinessEmail = "headmaster.riverside@example.com",
                BusinessPhoneNumber = "+1234567890",
                IsActive = true
            },
            new Headmaster
            {
                Id = Guid.Parse(HEADMASTER_HILLCREST_ID),
                UserId = Guid.Parse(USER_CW_ID),
                SchoolId = Guid.Parse(SCHOOL_HILLCREST_ID),
                RoleType = RoleType.Headmaster,
                BusinessEmail = "headmaster.hillcrest@example.com",
                BusinessPhoneNumber = "+1987654321",
                IsActive = true
            }
        ];
    }
}