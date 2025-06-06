using static Gradebook.Shared.Constants.SeedConstants;

namespace Gradebook.Data.Seeders;

public class TimetableSeeder : IDataSeeder
{
    public int Order => 12;

    public async Task Seed(DbContext context)
    {
        var timetables = GetTimetables();

        foreach (var timetable in timetables)
        {
            bool exists = await context.Set<Timetable>().AnyAsync(x => x.Id == timetable.Id);
            if (!exists)
            {
                timetable.TimeOfDay = DateTime.SpecifyKind(timetable.TimeOfDay, DateTimeKind.Utc);

                await context.Set<Timetable>().AddAsync(timetable);
                await context.SaveChangesAsync();
            }
        }
    }

    private static List<Timetable> GetTimetables()
    {
        return
        [
            new Timetable
            {
                Id = Guid.Parse(TIMETABLE_8A_CHEMISTRY_MONDAY_ID),
                SchoolYearId = Guid.Parse(SCHOOL_YEAR_RIVERSIDE_2024_1_ID),
                TimeOfDay = new DateTime(1, 1, 1, 8, 0, 0),
                DayOfWeek = DayOfWeek.Monday,
                TeacherId = Guid.Parse(TEACHER_CHEMISTRY_ID),
                ClassId = Guid.Parse(CLASS_8A_ID),
                SubjectId = Guid.Parse(SUBJECT_CHEMISTRY_ID)
            },
            new Timetable
            {
                Id = Guid.Parse(TIMETABLE_8A_HISTORY_MONDAY_ID),
                SchoolYearId = Guid.Parse(SCHOOL_YEAR_RIVERSIDE_2024_1_ID),
                TimeOfDay = new DateTime(1, 1, 1, 10, 0, 0),
                DayOfWeek = DayOfWeek.Monday,
                TeacherId = Guid.Parse(TEACHER_HISTORY_ID),
                ClassId = Guid.Parse(CLASS_8A_ID),
                SubjectId = Guid.Parse(SUBJECT_HISTORY_ID)
            },
            new Timetable
            {
                Id = Guid.Parse(TIMETABLE_8A_LITERATURE_MONDAY_ID),
                SchoolYearId = Guid.Parse(SCHOOL_YEAR_RIVERSIDE_2024_1_ID),
                TimeOfDay = new DateTime(1, 1, 1, 12, 0, 0),
                DayOfWeek = DayOfWeek.Monday,
                TeacherId = Guid.Parse(TEACHER_LITERATURE_ID),
                ClassId = Guid.Parse(CLASS_8A_ID),
                SubjectId = Guid.Parse(SUBJECT_LITERATURE_ID)
            },
            new Timetable
            {
                Id = Guid.Parse("db3bceef-28c2-434b-94a7-62aeb5f75464"),
                SchoolYearId = Guid.Parse(SCHOOL_YEAR_RIVERSIDE_2024_1_ID),
                TimeOfDay = new DateTime(1, 1, 1, 8, 0, 0),
                DayOfWeek = DayOfWeek.Tuesday,
                TeacherId = Guid.Parse(TEACHER_MATH_ID),
                ClassId = Guid.Parse(CLASS_8A_ID),
                SubjectId = Guid.Parse(SUBJECT_MATHEMATICS_ID)
            },
            new Timetable
            {
                Id = Guid.Parse("3d15379d-99f4-470e-bcf7-e49006525049"),
                SchoolYearId = Guid.Parse(SCHOOL_YEAR_RIVERSIDE_2024_1_ID),
                TimeOfDay = new DateTime(1, 1, 1, 10, 0, 0),
                DayOfWeek = DayOfWeek.Tuesday,
                TeacherId = Guid.Parse(TEACHER_PHYSICS_ID),
                ClassId = Guid.Parse(CLASS_8A_ID),
                SubjectId = Guid.Parse(SUBJECT_PHYSICS_ID)
            },
            new Timetable
            {
                Id = Guid.Parse("7d1d6b73-b89e-4de1-a177-c98cc321ed6d"),
                SchoolYearId = Guid.Parse(SCHOOL_YEAR_RIVERSIDE_2024_1_ID),
                TimeOfDay = new DateTime(1, 1, 1, 12, 0, 0),
                DayOfWeek = DayOfWeek.Tuesday,
                TeacherId = Guid.Parse(TEACHER_CHEMISTRY_ID),
                ClassId = Guid.Parse(CLASS_8A_ID),
                SubjectId = Guid.Parse(SUBJECT_CHEMISTRY_ID)
            },
            new Timetable
            {
                Id = Guid.Parse("e879053c-c413-420e-b318-6f5dfb7f14f5"),
                SchoolYearId = Guid.Parse(SCHOOL_YEAR_RIVERSIDE_2024_1_ID),
                TimeOfDay = new DateTime(1, 1, 1, 8, 0, 0),
                DayOfWeek = DayOfWeek.Wednesday,
                TeacherId = Guid.Parse(TEACHER_HISTORY_ID),
                ClassId = Guid.Parse(CLASS_8A_ID),
                SubjectId = Guid.Parse(SUBJECT_HISTORY_ID)
            },
            new Timetable
            {
                Id = Guid.Parse("4c6f2d35-d10a-412e-a469-90a5172cd01a"),
                SchoolYearId = Guid.Parse(SCHOOL_YEAR_RIVERSIDE_2024_1_ID),
                TimeOfDay = new DateTime(1, 1, 1, 10, 0, 0),
                DayOfWeek = DayOfWeek.Wednesday,
                TeacherId = Guid.Parse(TEACHER_LITERATURE_ID),
                ClassId = Guid.Parse(CLASS_8A_ID),
                SubjectId = Guid.Parse(SUBJECT_LITERATURE_ID)
            },
            new Timetable
            {
                Id = Guid.Parse("6d2f8128-c4f1-4b01-97f5-99b76c4b97bc"),
                SchoolYearId = Guid.Parse(SCHOOL_YEAR_RIVERSIDE_2024_1_ID),
                TimeOfDay = new DateTime(1, 1, 1, 12, 0, 0),
                DayOfWeek = DayOfWeek.Wednesday,
                TeacherId = Guid.Parse(TEACHER_MATH_ID),
                ClassId = Guid.Parse(CLASS_8A_ID),
                SubjectId = Guid.Parse(SUBJECT_MATHEMATICS_ID)
            },
            new Timetable
            {
                Id = Guid.Parse("10a3d942-e1cd-4638-91a0-2a275e8aebf4"),
                SchoolYearId = Guid.Parse(SCHOOL_YEAR_RIVERSIDE_2024_1_ID),
                TimeOfDay = new DateTime(1, 1, 1, 8, 0, 0),
                DayOfWeek = DayOfWeek.Thursday,
                TeacherId = Guid.Parse(TEACHER_PHYSICS_ID),
                ClassId = Guid.Parse(CLASS_8A_ID),
                SubjectId = Guid.Parse(SUBJECT_PHYSICS_ID)
            },
            new Timetable
            {
                Id = Guid.Parse("303e2cb2-9364-4efb-b2e9-637c59f366e8"),
                SchoolYearId = Guid.Parse(SCHOOL_YEAR_RIVERSIDE_2024_1_ID),
                TimeOfDay = new DateTime(1, 1, 1, 10, 0, 0),
                DayOfWeek = DayOfWeek.Thursday,
                TeacherId = Guid.Parse(TEACHER_CHEMISTRY_ID),
                ClassId = Guid.Parse(CLASS_8A_ID),
                SubjectId = Guid.Parse(SUBJECT_CHEMISTRY_ID)
            },
            new Timetable
            {
                Id = Guid.Parse("f7a42df3-37c3-4687-8f33-23d7edb30ab0"),
                SchoolYearId = Guid.Parse(SCHOOL_YEAR_RIVERSIDE_2024_1_ID),
                TimeOfDay = new DateTime(1, 1, 1, 12, 0, 0),
                DayOfWeek = DayOfWeek.Thursday,
                TeacherId = Guid.Parse(TEACHER_HISTORY_ID),
                ClassId = Guid.Parse(CLASS_8A_ID),
                SubjectId = Guid.Parse(SUBJECT_HISTORY_ID)
            },
            new Timetable
            {
                Id = Guid.Parse("635bc0f3-3b48-4939-b640-2dfe1c1d6b01"),
                SchoolYearId = Guid.Parse(SCHOOL_YEAR_RIVERSIDE_2024_1_ID),
                TimeOfDay = new DateTime(1, 1, 1, 8, 0, 0),
                DayOfWeek = DayOfWeek.Friday,
                TeacherId = Guid.Parse(TEACHER_LITERATURE_ID),
                ClassId = Guid.Parse(CLASS_8A_ID),
                SubjectId = Guid.Parse(SUBJECT_LITERATURE_ID)
            },
            new Timetable
            {
                Id = Guid.Parse("2d9d8ef1-76e7-4ef7-a4bb-38a28d9fe6d2"),
                SchoolYearId = Guid.Parse(SCHOOL_YEAR_RIVERSIDE_2024_1_ID),
                TimeOfDay = new DateTime(1, 1, 1, 10, 0, 0),
                DayOfWeek = DayOfWeek.Friday,
                TeacherId = Guid.Parse(TEACHER_MATH_ID),
                ClassId = Guid.Parse(CLASS_8A_ID),
                SubjectId = Guid.Parse(SUBJECT_MATHEMATICS_ID)
            },
            new Timetable
            {
                Id = Guid.Parse("999de3e5-944c-4953-b2ce-c6f60ff3523c"),
                SchoolYearId = Guid.Parse(SCHOOL_YEAR_RIVERSIDE_2024_1_ID),
                TimeOfDay = new DateTime(1, 1, 1, 12, 0, 0),
                DayOfWeek = DayOfWeek.Friday,
                TeacherId = Guid.Parse(TEACHER_PHYSICS_ID),
                ClassId = Guid.Parse(CLASS_8A_ID),
                SubjectId = Guid.Parse(SUBJECT_PHYSICS_ID)
            }
        ];
    }
}