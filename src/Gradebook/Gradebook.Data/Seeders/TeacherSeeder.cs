using static Gradebook.Shared.Constants.SeedConstants;

namespace Gradebook.Data.Seeders;

public class TeacherSeeder : IDataSeeder
{
    public int Order => 7;

    public async Task Seed(DbContext context)
    {
        var teachers = GetTeachers();

        foreach (var teacher in teachers)
        {
            bool exists = await context.Set<Teacher>().AnyAsync(x => x.Id == teacher.Id);
            if (!exists)
            {
                await context.Set<Teacher>().AddAsync(teacher);
                await context.SaveChangesAsync();
            }
        }
    }

    private static List<Teacher> GetTeachers()
    {
        return
        [
            new Teacher
            {
                Id = Guid.Parse(TEACHER_MATH_ID),
                UserId = Guid.Parse(USER_DB_ID),
                RoleType = RoleType.Teacher,
                IsActive = true,
                BusinessEmail = "math.teacher@riverside.edu",
                BusinessPhoneNumber = "+1000000001",
                SchoolId = Guid.Parse(SCHOOL_RIVERSIDE_ID),
                ClassId = null
            },
            new Teacher
            {
                Id = Guid.Parse(TEACHER_HISTORY_ID),
                UserId = Guid.Parse(USER_ED_ID),
                RoleType = RoleType.Teacher,
                IsActive = true,
                BusinessEmail = "history.teacher@riverside.edu",
                BusinessPhoneNumber = "+1000000002",
                SchoolId = Guid.Parse(SCHOOL_RIVERSIDE_ID),
                ClassId = null
            },
            new Teacher
            {
                Id = Guid.Parse(TEACHER_PHYSICS_ID),
                UserId = Guid.Parse(USER_FW_ID),
                RoleType = RoleType.Teacher,
                IsActive = true,
                BusinessEmail = "physics.teacher@hillcrest.edu",
                BusinessPhoneNumber = "+1000000003",
                SchoolId = Guid.Parse(SCHOOL_RIVERSIDE_ID),
                ClassId = null
            },
            new Teacher
            {
                Id = Guid.Parse(TEACHER_LITERATURE_ID),
                UserId = Guid.Parse(USER_GT_ID),
                RoleType = RoleType.Teacher,
                IsActive = true,
                BusinessEmail = "literature.teacher@hillcrest.edu",
                BusinessPhoneNumber = "+1000000004",
                SchoolId = Guid.Parse(SCHOOL_RIVERSIDE_ID),
                ClassId = null
            },
            new Teacher
            {
                Id = Guid.Parse(TEACHER_CHEMISTRY_ID),
                UserId = Guid.Parse(USER_HM_ID),
                RoleType = RoleType.Teacher,
                IsActive = true,
                BusinessEmail = "chemistry.teacher@riverside.edu",
                BusinessPhoneNumber = "+1000000005",
                SchoolId = Guid.Parse(SCHOOL_RIVERSIDE_ID),
                ClassId = null
            }
        ];
    }
}