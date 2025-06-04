namespace Gradebook.Data.Seeders;

/// <summary>
/// Defines a contract for data seeding operations.
/// Implementations specify their order of execution via the Order property
/// and perform database seeding tasks asynchronously using the Seed method.
/// </summary>
public interface IDataSeeder
{
    int Order { get; }
    Task Seed(DbContext context);
}
