namespace Gradebook.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterGradebookServices(this IServiceCollection services)
    {
        services.AddDataSeeders();

        return services;
    }

    private static IServiceCollection AddDataSeeders(this IServiceCollection services)
    {
        services.AddTransient<IDataSeeder, UserSeeder>();
        services.AddTransient<IDataSeeder, AdminSeeder>();
        services.AddTransient<IDataSeeder, SchoolSeeder>();
        services.AddTransient<IDataSeeder, HeadmasterSeeder>();
        services.AddTransient<IDataSeeder, SchoolYearSeeder>();
        services.AddTransient<IDataSeeder, SubjectSeeder>();
        services.AddTransient<IDataSeeder, TeacherSeeder>();
        services.AddTransient<IDataSeeder, ClassSeeder>();
        services.AddTransient<IDataSeeder, StudentSeeder>();
        services.AddTransient<IDataSeeder, GradeSeeder>();
        services.AddTransient<IDataSeeder, ParentSeeder>();
        services.AddTransient<IDataSeeder, TimetableSeeder>();
        services.AddTransient<IDataSeeder, AbsenceSeeder>();
        services.AddTransient<IDataSeeder, StudentParentSeeder>();
        services.AddTransient<IDataSeeder, TeacherSubjectSeeder>();

        return services;
    }
}
