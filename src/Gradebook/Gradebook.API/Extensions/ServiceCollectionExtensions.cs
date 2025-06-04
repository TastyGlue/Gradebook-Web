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
        
        return services;
    }
}
