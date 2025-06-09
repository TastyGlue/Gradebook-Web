using static Gradebook.Shared.Constants.SeedConstants;

namespace Gradebook.Data.Seeders;

public class ClassSeeder : IDataSeeder
{
    public int Order => 8;

    public async Task Seed(DbContext context)
    {
        var classes = GetClasses();

        foreach (var c in classes)
        {
            bool exists = await context.Set<Class>().AnyAsync(x => x.Id == c.Id);
            if (!exists)
            {
                await context.Set<Class>().AddAsync(c);
                await context.SaveChangesAsync();
            }
        }
    }

    private static List<Class> GetClasses()
    {
        return
        [
            new Class
            {
                Id = Guid.Parse(CLASS_8A_ID),
                Year = 8,
                Signature = "A",
                SchoolId = Guid.Parse(SCHOOL_RIVERSIDE_ID),
                ClassTeacherId = Guid.Parse(TEACHER_MATH_ID)
            },
            new Class
            {
                Id = Guid.Parse(CLASS_8B_ID),
                Year = 8,
                Signature = "B",
                SchoolId = Guid.Parse(SCHOOL_RIVERSIDE_ID),
                ClassTeacherId = Guid.Parse(TEACHER_LITERATURE_ID)
            }
        ];
    }
}