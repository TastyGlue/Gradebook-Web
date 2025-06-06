using static Gradebook.Shared.Constants.SeedConstants;

namespace Gradebook.Data.Seeders;

public class AbsenceSeeder : IDataSeeder
{
    public int Order => 13;

    public async Task Seed(DbContext context)
    {
        var absences = GetAbsences();

        foreach (var absence in absences)
        {
            bool exists = await context.Set<Absence>().AnyAsync(x => x.Id == absence.Id);
            if (!exists)
            {
                absence.Date = DateTime.SpecifyKind(absence.Date, DateTimeKind.Utc);

                await context.Set<Absence>().AddAsync(absence);
                await context.SaveChangesAsync();
            }
        }
    }

    private static List<Absence> GetAbsences()
    {
        return
        [
            new Absence
            {
                Id = Guid.Parse("b4d3ea21-dcf9-4f6d-bd3e-82b8d1e3ac01"),
                Date = new DateTime(2024, 9, 2),
                Excused = true,
                SchoolYearId = Guid.Parse(SCHOOL_YEAR_RIVERSIDE_2024_1_ID),
                StudentId = Guid.Parse(STUDENT_JM_ID),
                TimetableId = Guid.Parse(TIMETABLE_8A_CHEMISTRY_MONDAY_ID)
            },
            new Absence
            {
                Id = Guid.Parse("25f2a4f9-f3a9-413f-b524-d5db8a76eaa6"),
                Date = new DateTime(2024, 9, 2),
                Excused = true,
                SchoolYearId = Guid.Parse(SCHOOL_YEAR_RIVERSIDE_2024_1_ID),
                StudentId = Guid.Parse(STUDENT_JM_ID),
                TimetableId = Guid.Parse(TIMETABLE_8A_HISTORY_MONDAY_ID)
            },
            new Absence
            {
                Id = Guid.Parse("8329a9bc-c6b7-4c0b-987e-c408f2f7fbc9"),
                Date = new DateTime(2024, 9, 2),
                Excused = true,
                SchoolYearId = Guid.Parse(SCHOOL_YEAR_RIVERSIDE_2024_1_ID),
                StudentId = Guid.Parse(STUDENT_JM_ID),
                TimetableId = Guid.Parse(TIMETABLE_8A_LITERATURE_MONDAY_ID)
            }
        ];
    }
}