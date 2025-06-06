using static Gradebook.Shared.Constants.SeedConstants;

namespace Gradebook.Data.Seeders;

public class TeacherSubjectSeeder : IDataSeeder
{
    public int Order => 15;

    public async Task Seed(DbContext context)
    {
        var teacherSubjects = GetTeacherSubjects();

        foreach (var teacherSubject in teacherSubjects)
        {
            bool exists = await context.Set<TeacherSubject>().AnyAsync(x => x.TeacherId == teacherSubject.TeacherId && x.SubjectId == teacherSubject.SubjectId);
            if (!exists)
            {
                await context.Set<TeacherSubject>().AddAsync(teacherSubject);
                await context.SaveChangesAsync();
            }
        }
    }

    private static List<TeacherSubject> GetTeacherSubjects()
    {
        return [
            new TeacherSubject
            {
                TeacherId = Guid.Parse(TEACHER_CHEMISTRY_ID),
                SubjectId = Guid.Parse(SUBJECT_CHEMISTRY_ID)
            },
            new TeacherSubject
            {
                TeacherId = Guid.Parse(TEACHER_HISTORY_ID),
                SubjectId = Guid.Parse(SUBJECT_HISTORY_ID)
            },
            new TeacherSubject
            {
                TeacherId = Guid.Parse(TEACHER_LITERATURE_ID),
                SubjectId = Guid.Parse(SUBJECT_LITERATURE_ID)
            },
            new TeacherSubject
            {
                TeacherId = Guid.Parse(TEACHER_MATH_ID),
                SubjectId = Guid.Parse(SUBJECT_MATHEMATICS_ID)
            },
            new TeacherSubject
            {
                TeacherId = Guid.Parse(TEACHER_PHYSICS_ID),
                SubjectId = Guid.Parse(SUBJECT_PHYSICS_ID)
            }
        ];
    }
}