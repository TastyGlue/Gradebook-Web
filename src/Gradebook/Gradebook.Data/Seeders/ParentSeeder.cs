using static Gradebook.Shared.Constants.SeedConstants;

namespace Gradebook.Data.Seeders;

public class ParentSeeder : IDataSeeder
{
    public int Order => 11;

    public async Task Seed(DbContext context)
    {
        var parents = GetParents();

        foreach (var parent in parents)
        {
            bool exists = await context.Set<Parent>().AnyAsync(x => x.Id == parent.Id);
            if (!exists)
            {
                await context.Set<Parent>().AddAsync(parent);
                await context.SaveChangesAsync();
            }
        }
    }

    private static List<Parent> GetParents()
    {
        return
        [
            new Parent
            {
                Id = Guid.Parse(PARENT_TM_ID),
                UserId = Guid.Parse(USER_TM_ID),
                RoleType = RoleType.Parent,
                IsActive = true
            },
            new Parent
            {
                Id = Guid.Parse(PARENT_JT_ID),
                UserId = Guid.Parse(USER_JT_ID),
                RoleType = RoleType.Parent,
                IsActive = true
            }
        ];
    }
}