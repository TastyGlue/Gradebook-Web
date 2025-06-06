using static Gradebook.Shared.Constants.SeedConstants;

namespace Gradebook.Data.Seeders;

public class StudentSeeder : IDataSeeder
{
    public int Order => 9;

    public async Task Seed(DbContext context)
    {
        var students = GetStudents();

        foreach (var student in students)
        {
            bool exists = await context.Set<Student>().AnyAsync(x => x.Id == student.Id);
            if (!exists)
            {
                await context.Set<Student>().AddAsync(student);
                await context.SaveChangesAsync();
            }
        }
    }

    private static List<Student> GetStudents()
    {
        return
        [
            new Student
            {
                Id = Guid.Parse(STUDENT_IJ_ID),
                UserId = Guid.Parse(USER_IJ_ID),
                RoleType = RoleType.Student,
                IsActive = true,
                SchoolId = Guid.Parse(SCHOOL_RIVERSIDE_ID),
                ClassId = Guid.Parse(CLASS_8A_ID)
            },
            new Student
            {
                Id = Guid.Parse(STUDENT_JM_ID),
                UserId = Guid.Parse(USER_JM_ID),
                RoleType = RoleType.Student,
                IsActive = true,
                SchoolId = Guid.Parse(SCHOOL_RIVERSIDE_ID),
                ClassId = Guid.Parse(CLASS_8A_ID)
            },
            new Student
            {
                Id = Guid.Parse(STUDENT_KT_ID),
                UserId = Guid.Parse(USER_KT_ID),
                RoleType = RoleType.Student,
                IsActive = true,
                SchoolId = Guid.Parse(SCHOOL_RIVERSIDE_ID),
                ClassId = Guid.Parse(CLASS_8A_ID)
            },
            new Student
            {
                Id = Guid.Parse(STUDENT_LG_ID),
                UserId = Guid.Parse(USER_LG_ID),
                RoleType = RoleType.Student,
                IsActive = true,
                SchoolId = Guid.Parse(SCHOOL_RIVERSIDE_ID),
                ClassId = Guid.Parse(CLASS_8A_ID)
            },
            new Student
            {
                Id = Guid.Parse(STUDENT_MM_ID),
                UserId = Guid.Parse(USER_MM_ID),
                RoleType = RoleType.Student,
                IsActive = true,
                SchoolId = Guid.Parse(SCHOOL_RIVERSIDE_ID),
                ClassId = Guid.Parse(CLASS_8A_ID)
            }
        ];
    }
}