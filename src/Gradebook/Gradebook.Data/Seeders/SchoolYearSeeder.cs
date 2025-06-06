using static Gradebook.Shared.Constants.SeedConstants;

namespace Gradebook.Data.Seeders;

public class SchoolYearSeeder : IDataSeeder
{
    public int Order => 5;

    public async Task Seed(DbContext context)
    {
        var schoolYears = GetSchoolYears();

        foreach (var schoolYear in schoolYears)
        {
            bool exists = await context.Set<SchoolYear>().AnyAsync(x => x.Id == schoolYear.Id);
            if (!exists)
            {
                schoolYear.Start = DateTime.SpecifyKind(schoolYear.Start!.Value, DateTimeKind.Utc);
                schoolYear.End = DateTime.SpecifyKind(schoolYear.End!.Value, DateTimeKind.Utc);

                await context.Set<SchoolYear>().AddAsync(schoolYear);
                await context.SaveChangesAsync();
            }
        }
    }

    private static List<SchoolYear> GetSchoolYears()
    {
        return [
            new SchoolYear
            {
                Id = Guid.Parse(SCHOOL_YEAR_RIVERSIDE_2024_1_ID),
                SchoolId = Guid.Parse(SCHOOL_RIVERSIDE_ID),
                Year = 2024,
                Semester = 1,
                Start = new DateTime(2024, 9, 1),
                End = new DateTime(2025, 1, 15)
            },
            new SchoolYear
            {
                Id = Guid.Parse(SCHOOL_YEAR_RIVERSIDE_2024_2_ID),
                SchoolId = Guid.Parse(SCHOOL_RIVERSIDE_ID),
                Year = 2024,
                Semester = 2,
                Start = new DateTime(2025, 2, 1),
                End = new DateTime(2025, 6, 30)
            }
        ];
    }
}