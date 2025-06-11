namespace Gradebook.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterGradebookServices(this IServiceCollection services, WebApplicationBuilder builder)
    {
        builder.Services.AddIdentity<User, IdentityRole<Guid>>(options =>
        {
            options.SignIn.RequireConfirmedAccount = false;
            options.SignIn.RequireConfirmedEmail = false;
            options.Lockout.MaxFailedAccessAttempts = 0;
            options.Password.RequiredLength = 8;
            options.Password.RequireNonAlphanumeric = false;
            options.User.RequireUniqueEmail = true;
        })
            .AddRoles<IdentityRole<Guid>>()
            .AddEntityFrameworkStores<GradebookDbContext>()
            .AddDefaultTokenProviders();

        builder.Services.AddDbContext<GradebookDbContext>(options =>
        {
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddAuthenticationSchemes(builder);

        services.AddDataSeeders();

        services.AddTransient<ITokenService, TokenService>();
        services.AddTransient<IAuthService, AuthService>();
        services.AddTransient<IProfileContexService, ProfileContextService>();

        return services;
    }

    private static IServiceCollection AddAuthenticationSchemes(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

        JwtSettings jwtSettings = new();
        builder.Configuration.GetRequiredSection("JwtSettings").Bind(jwtSettings);

        builder.Services.AddAuthentication()
            .AddJwtBearer(AUTH_SCHEME, options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.AuthSecurityKey))
                };
            })
            .AddJwtBearer(ACCESS_SCHEME, options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.AccessSecurityKey))
                };
            })
            .AddJwtBearer(PROFILE_TOKEN_SCHEME, options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.AccessSecurityKey))
                };
            });

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
