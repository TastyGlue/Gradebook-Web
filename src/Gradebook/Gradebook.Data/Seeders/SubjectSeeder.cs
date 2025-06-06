using static Gradebook.Shared.Constants.SeedConstants;

namespace Gradebook.Data.Seeders;

public class SubjectSeeder : IDataSeeder
{
    public int Order => 6;

    public async Task Seed(DbContext context)
    {
        var subjects = GetSubjects();

        foreach (var subject in subjects)
        {
            bool exists = await context.Set<Subject>().AnyAsync(x => x.Id == subject.Id);
            if (!exists)
            {
                await context.Set<Subject>().AddAsync(subject);
                await context.SaveChangesAsync();
            }
        }
    }

    private static List<Subject> GetSubjects()
    {
        return [
            new Subject
            {
                Id = Guid.Parse(SUBJECT_MATHEMATICS_ID),
                Name = "Mathematics",
                SchoolId = Guid.Parse(SCHOOL_RIVERSIDE_ID)
            },
            new Subject
            {
                Id = Guid.Parse(SUBJECT_HISTORY_ID),
                Name = "History",
                SchoolId = Guid.Parse(SCHOOL_RIVERSIDE_ID)
            },
            new Subject
            {
                Id = Guid.Parse(SUBJECT_PHYSICS_ID),
                Name = "Physics",
                SchoolId = Guid.Parse(SCHOOL_RIVERSIDE_ID)
            },
            new Subject
            {
                Id = Guid.Parse(SUBJECT_LITERATURE_ID),
                Name = "Literature",
                SchoolId = Guid.Parse(SCHOOL_RIVERSIDE_ID)
            },
            new Subject
            {
                Id = Guid.Parse(SUBJECT_CHEMISTRY_ID),
                Name = "Chemistry",
                SchoolId = Guid.Parse(SCHOOL_RIVERSIDE_ID)
            }
        ];
    }
}