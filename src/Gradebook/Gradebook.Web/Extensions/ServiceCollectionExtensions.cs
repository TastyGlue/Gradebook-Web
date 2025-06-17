namespace Gradebook.Web.Extensions;

public static class ServiceCollectionExtensions
{
    public static WebApplicationBuilder RegisterServices(this WebApplicationBuilder builder)
    {
        // Add UI Component Library
        builder.Services.AddMudServices(config =>
        {
            config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopRight;
            config.SnackbarConfiguration.PreventDuplicates = false;
            config.SnackbarConfiguration.NewestOnTop = false;
            config.SnackbarConfiguration.ShowCloseIcon = true;
            config.SnackbarConfiguration.HideTransitionDuration = 500;
            config.SnackbarConfiguration.ShowTransitionDuration = 500;
            config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
        });
        builder.Services.AddMudLocalization();

        // Add Authentication and Authorization
        builder.Services.AddAuthorizationPolicies();
        builder.Services.AddCascadingAuthenticationState();

        builder.Services.AddHttpClient(Constants.API_CLIENT_NAME, client =>
        {
            client.BaseAddress = new Uri(builder.Configuration.GetSection("ApiAddress").Value!);
        });

        builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

        builder.Services.AddTransient<HttpClientService>();
        builder.Services.AddTransient<TokenService>();

        builder.Services.AddTransient<IApiAbsencesService, ApiAbsencesService>();   
        builder.Services.AddTransient<IApiStudentGradeService, ApiStudentGradeService>();
        builder.Services.AddTransient<IApiAuthService, ApiAuthService>();
        builder.Services.AddTransient<IApiSchoolService, ApiSchoolService>();
        builder.Services.AddTransient<IApiTeacherService, ApiTeacherService>();
        builder.Services.AddTransient<IApiHeadmasterService, ApiHeadmasterService>();
        builder.Services.AddTransient<IApiStudentService, ApiStudentService>();
        builder.Services.AddTransient<IApiParentService, ApiParentService>();

        builder.Services.AddScoped<LoaderService>();
        builder.Services.AddScoped<UserStateContainer>();

        builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

        return builder;
    }

    private static IServiceCollection AddAuthorizationPolicies(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            foreach (var role in Enum.GetValues<RoleType>())
            {
                options.AddPolicy(role.ToString(), policy => policy.RequireClaim(Claims.ROLE, role.ToString()));
            }
        });

        return services;
    }
}
