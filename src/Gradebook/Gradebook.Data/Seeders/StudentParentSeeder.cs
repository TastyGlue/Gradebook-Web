using static Gradebook.Shared.Constants.SeedConstants;

namespace Gradebook.Data.Seeders;

public class StudentParentSeeder : IDataSeeder
{
    public int Order => 14;

    public async Task Seed(DbContext context)
    {
        var studentParents = GetStudentParents();

        foreach (var studentParent in studentParents)
        {
            bool exists = await context.Set<StudentParent>().AnyAsync(x => x.StudentId == studentParent.StudentId && x.ParentId == studentParent.ParentId);
            if (!exists)
            {
                await context.Set<StudentParent>().AddAsync(studentParent);
                await context.SaveChangesAsync();
            }
        }
    }

    private static List<StudentParent> GetStudentParents()
    {
        return [
            new StudentParent
            {
                StudentId = Guid.Parse(STUDENT_JM_ID),
                ParentId = Guid.Parse(PARENT_TM_ID)
            },
            new StudentParent
            {
                StudentId = Guid.Parse(STUDENT_KT_ID),
                ParentId = Guid.Parse(PARENT_JT_ID)
            }
        ];
    }
}