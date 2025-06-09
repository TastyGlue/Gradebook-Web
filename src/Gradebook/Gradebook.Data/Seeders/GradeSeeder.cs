using static Gradebook.Shared.Constants.SeedConstants;

namespace Gradebook.Data.Seeders;

public class GradeSeeder : IDataSeeder
{
    public int Order => 10;

    public async Task Seed(DbContext context)
    {
        var grades = GetGrades();

        foreach (var grade in grades)
        {
            bool exists = await context.Set<Grade>().AnyAsync(x => x.Id == grade.Id);
            if (!exists)
            {
                grade.Date = DateTime.SpecifyKind(grade.Date, DateTimeKind.Utc);

                await context.Set<Grade>().AddAsync(grade);
                await context.SaveChangesAsync();
            }
        }
    }

    private static List<Grade> GetGrades()
    {
        return
        [
            new Grade
            {
                Id = Guid.Parse("2a9b3508-5313-4122-947d-65e0a383c3df"),
                Value = 4.5m,
                SchoolYearId = Guid.Parse(SCHOOL_YEAR_RIVERSIDE_2024_1_ID),
                Date = new DateTime(2025, 1, 5),
                SubjectId = Guid.Parse(SUBJECT_CHEMISTRY_ID),
                StudentId = Guid.Parse(STUDENT_JM_ID),
                TeacherId = Guid.Parse(TEACHER_CHEMISTRY_ID)
            },
            new Grade
            {
                Id = Guid.Parse("4b34d317-91dc-49a6-8600-b7a4de3dcba0"),
                Value = 3.0m,
                SchoolYearId = Guid.Parse(SCHOOL_YEAR_RIVERSIDE_2024_1_ID),
                Date = new DateTime(2025, 1, 6),
                SubjectId = Guid.Parse(SUBJECT_HISTORY_ID),
                StudentId = Guid.Parse(STUDENT_JM_ID),
                TeacherId = Guid.Parse(TEACHER_HISTORY_ID)
            },
            new Grade
            {
                Id = Guid.Parse("6a3a405e-0cb7-4f3d-a28b-6f46087fda67"),
                Value = 5.0m,
                SchoolYearId = Guid.Parse(SCHOOL_YEAR_RIVERSIDE_2024_1_ID),
                Date = new DateTime(2025, 1, 7),
                SubjectId = Guid.Parse(SUBJECT_MATHEMATICS_ID),
                StudentId = Guid.Parse(STUDENT_JM_ID),
                TeacherId = Guid.Parse(TEACHER_MATH_ID)
            },
            new Grade
            {
                Id = Guid.Parse("b6e81db1-7d11-4ef9-8d99-e4d2c7df5e1f"),
                Value = 2.0m,
                SchoolYearId = Guid.Parse(SCHOOL_YEAR_RIVERSIDE_2024_1_ID),
                Date = new DateTime(2025, 1, 8),
                SubjectId = Guid.Parse(SUBJECT_LITERATURE_ID),
                StudentId = Guid.Parse(STUDENT_JM_ID),
                TeacherId = Guid.Parse(TEACHER_LITERATURE_ID)
            },
            new Grade
            {
                Id = Guid.Parse("7e4f0df6-44c3-44b3-b804-f5f2283b37a3"),
                Value = 6.0m,
                SchoolYearId = Guid.Parse(SCHOOL_YEAR_RIVERSIDE_2024_1_ID),
                Date = new DateTime(2025, 1, 9),
                SubjectId = Guid.Parse(SUBJECT_PHYSICS_ID),
                StudentId = Guid.Parse(STUDENT_JM_ID),
                TeacherId = Guid.Parse(TEACHER_PHYSICS_ID)
            },
            new Grade
            {
                Id = Guid.Parse("1ce19c31-c4a0-4b62-839a-2c5b3253ee1e"),
                Value = 5.0m,
                SchoolYearId = Guid.Parse(SCHOOL_YEAR_RIVERSIDE_2024_2_ID),
                Date = new DateTime(2025, 6, 1),
                SubjectId = Guid.Parse(SUBJECT_CHEMISTRY_ID),
                StudentId = Guid.Parse(STUDENT_JM_ID),
                TeacherId = Guid.Parse(TEACHER_CHEMISTRY_ID)
            },
            new Grade
            {
                Id = Guid.Parse("10b4ab6b-0b7c-41e0-b64a-c8c5947b8f1c"),
                Value = 4.0m,
                SchoolYearId = Guid.Parse(SCHOOL_YEAR_RIVERSIDE_2024_2_ID),
                Date = new DateTime(2025, 6, 2),
                SubjectId = Guid.Parse(SUBJECT_HISTORY_ID),
                StudentId = Guid.Parse(STUDENT_JM_ID),
                TeacherId = Guid.Parse(TEACHER_HISTORY_ID)
            },
            new Grade
            {
                Id = Guid.Parse("b79ac6c3-8cb1-42fe-b4f0-a5ae479d0a0e"),
                Value = 5.5m,
                SchoolYearId = Guid.Parse(SCHOOL_YEAR_RIVERSIDE_2024_2_ID),
                Date = new DateTime(2025, 6, 3),
                SubjectId = Guid.Parse(SUBJECT_MATHEMATICS_ID),
                StudentId = Guid.Parse(STUDENT_JM_ID),
                TeacherId = Guid.Parse(TEACHER_MATH_ID)
            },
            new Grade
            {
                Id = Guid.Parse("1835279e-6697-468e-afd1-23eafd340b65"),
                Value = 3.0m,
                SchoolYearId = Guid.Parse(SCHOOL_YEAR_RIVERSIDE_2024_2_ID),
                Date = new DateTime(2025, 6, 4),
                SubjectId = Guid.Parse(SUBJECT_LITERATURE_ID),
                StudentId = Guid.Parse(STUDENT_JM_ID),
                TeacherId = Guid.Parse(TEACHER_LITERATURE_ID)
            },
            new Grade
            {
                Id = Guid.Parse("51c9ecc3-f69c-46d1-b8ad-f02cddefdd87"),
                Value = 5.0m,
                SchoolYearId = Guid.Parse(SCHOOL_YEAR_RIVERSIDE_2024_2_ID),
                Date = new DateTime(2025, 6, 5),
                SubjectId = Guid.Parse(SUBJECT_PHYSICS_ID),
                StudentId = Guid.Parse(STUDENT_JM_ID),
                TeacherId = Guid.Parse(TEACHER_PHYSICS_ID)
            }
        ];
    }
}