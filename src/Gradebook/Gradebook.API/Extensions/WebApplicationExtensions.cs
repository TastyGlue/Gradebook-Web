using static Gradebook.Shared.Constants.IndexConstants;

namespace Gradebook.API.Extensions;

public static class WebApplicationExtensions
{
    /// <summary>
    /// Migrates the database to the latest version and seeds it with initial data.
    /// </summary>
    /// <remarks>This method ensures that the database schema is up-to-date by applying any pending
    /// migrations. It also retrieves all registered implementations of <see cref="IDataSeeder"/>, orders them by their
    /// <see cref="IDataSeeder.Order"/> property, and invokes their <see cref="IDataSeeder.Seed"/> method to populate
    /// the database with initial data.</remarks>
    /// <param name="app">The <see cref="WebApplication"/> instance used to access the application's services.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public static async Task MigrateDbAndSeedData(this WebApplication app)
    {
        using var serviceScope = app.Services.CreateScope();
        var serviceProvider = serviceScope.ServiceProvider;
        var dbContext = serviceProvider.GetRequiredService<GradebookDbContext>();

        // Migrate the database to the latest version or create it if it doesn't exist
        await dbContext.Database.MigrateAsync();

        // Seed the database with initial data
        var seeders = serviceProvider.GetServices<IDataSeeder>().OrderBy(s => s.Order);

        foreach (var seeder in seeders)
            await seeder.Seed(dbContext);
    }

    /// <summary>
    /// Creates database indexes for the <see cref="GradebookDbContext.Profiles"/> table based on user roles.
    /// </summary>
    /// <remarks>This method defines and creates indexes for the <see cref="GradebookDbContext.Profiles"/>
    /// table to optimize queries based on the <see cref="Profile.RoleType"/> and other related columns. Each index is
    /// tailored to a specific role type (e.g., Admin, Headmaster, Teacher, Student, Parent) and includes relevant
    /// columns for efficient lookups.</remarks>
    /// <param name="app">The <see cref="WebApplication"/> instance used to access the application's service provider and database
    /// context.</param>
    /// <returns>A task that represents the asynchronous operation of creating the indexes.</returns>
    public static async Task CreateIndexes(this WebApplication app)
    {
        using var serviceScope = app.Services.CreateScope();
        var serviceProvider = serviceScope.ServiceProvider;
        var dbContext = serviceProvider.GetRequiredService<GradebookDbContext>();

        var connection = dbContext.Database.GetDbConnection();
        await using (connection)
        {
            if (connection.State != ConnectionState.Open)
                await connection.OpenAsync();

            await connection.CreateIndex(
                indexName: PROFILE_USERID_ADMIN_INDEX, 
                tableName: nameof(GradebookDbContext.Profiles), 
                condition: $@"""{nameof(Profile.RoleType)}"" = {((int)RoleType.Admin)}", 
                indexedColumns: [nameof(Profile.UserId)]);

            await connection.CreateIndex(
                indexName: PROFILE_USERID_HEADMASTER_INDEX,
                tableName: nameof(GradebookDbContext.Profiles),
                condition: $@"""{nameof(Profile.RoleType)}"" = {((int)RoleType.Headmaster)}",
                indexedColumns: [nameof(Profile.UserId), nameof(Headmaster.SchoolId)]);

            await connection.CreateIndex(
                indexName: PROFILE_USERID_TEACHER_INDEX,
                tableName: nameof(GradebookDbContext.Profiles),
                condition: $@"""{nameof(Profile.RoleType)}"" = {((int)RoleType.Teacher)}",
                indexedColumns: [nameof(Profile.UserId), nameof(Teacher.SchoolId)]);

            await connection.CreateIndex(
                indexName: PROFILE_USERID_STUDENT_INDEX,
                tableName: nameof(GradebookDbContext.Profiles),
                condition: $@"""{nameof(Profile.RoleType)}"" = {((int)RoleType.Student)}",
                indexedColumns: [nameof(Profile.UserId), nameof(Student.SchoolId)]);

            await connection.CreateIndex(
                indexName: PROFILE_USERID_PARENT_INDEX,
                tableName: nameof(GradebookDbContext.Profiles),
                condition: $@"""{nameof(Profile.RoleType)}"" = {((int)RoleType.Parent)}",
                indexedColumns: [nameof(Profile.UserId)]);
        }
    }

    private static async Task CreateIndex(this DbConnection connection, string indexName, string tableName, string condition, string[] indexedColumns)
    {
        // Check if the index already exists
        await using var checkCommand = connection.CreateCommand();
        checkCommand.CommandText = $@"
                SELECT 1
                FROM pg_indexes
                WHERE indexname = '{indexName}'
                  AND tablename = '{tableName}';
            ";

        var result = await checkCommand.ExecuteScalarAsync();

        // If the index does not exist, create it
        if (result == null)
        {
            string indexedColumnsList = string.Join(", ", indexedColumns.Select(c => $"\"{c}\""));

            // Create the index
            await using var createCommand = connection.CreateCommand();
            createCommand.CommandText = $@"
                    CREATE UNIQUE INDEX ""{indexName}""
                    ON ""{tableName}"" ({indexedColumnsList})
                    WHERE {condition};
                ";

            await createCommand.ExecuteNonQueryAsync();
        }
    }
}
