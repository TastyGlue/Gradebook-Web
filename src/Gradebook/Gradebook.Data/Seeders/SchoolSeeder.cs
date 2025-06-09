using static Gradebook.Shared.Constants.SeedConstants;

namespace Gradebook.Data.Seeders;

public class SchoolSeeder : IDataSeeder
{
    public int Order => 3;

    public async Task Seed(DbContext context)
    {
        var schools = GetSchools();

        foreach (var school in schools)
        {
            bool exists = await context.Set<School>().AnyAsync(x => x.Id == school.Id);
            if (!exists)
            {
                await context.Set<School>().AddAsync(school);
                await context.SaveChangesAsync();
            }
        }
    }

    private static List<School> GetSchools()
    {
        return [
            new School
            {
                Id = Guid.Parse(SCHOOL_RIVERSIDE_ID),
                Name = "Riverside High School",
                Address = "101 River Road, Education Town, Country",
                Website = "https://riversidehigh.example.com"
            },
            new School
            {
                Id = Guid.Parse(SCHOOL_HILLCREST_ID),
                Name = "Hillcrest Academy",
                Address = "202 Hilltop Avenue, Suburbia, Country",
                Website = "https://hillcrestacademy.example.com"
            }
        ];
    }
}