using Gradebook.API.Handlers;
using Microsoft.AspNetCore.Authentication.JwtBearer;

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
        services.AddAuthorizationPolicies();

        services.AddDataSeeders();

     
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<ITimetableService, TimetableService>();
        services.AddTransient<ISubjectService, SubjectService>();
        services.AddTransient<IStudentService, StudentService>();
        services.AddTransient<ISchoolYearService, SchoolYearService>();
        services.AddTransient<IParentService, ParentService>();
        services.AddTransient<IHeadmasterService, HeadmasterService>();
        services.AddTransient<IClassService, ClassService>();
        services.AddTransient<ITeacherService, TeacherService>();
        services.AddTransient<ISchoolService, SchoolService>();
        services.AddTransient<IAbsenceService, AbsenceService>();
        services.AddTransient<IGradeService, GradeService>();
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
                options.Events = CustomAuthFailureEvent;

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
                options.Events = CustomAuthFailureEvent;

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
                options.Events = CustomAuthFailureEvent;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.AccessSecurityKey))
                };
            });

        builder.Services.AddSingleton<IAuthorizationMiddlewareResultHandler, CustomAuthorizationMiddlewareResultHandler>();

        return services;
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

    private static readonly JwtBearerEvents CustomAuthFailureEvent = new()
    {
        OnChallenge = context =>
        {
            context.HandleResponse();

            context.Response.StatusCode = 401;
            context.Response.ContentType = "application/json";

            var response = new ErrorResult("Request is not authenticated", ErrorCodes.ACCESS_NOT_AUTHENTICATED);

            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    };
}
