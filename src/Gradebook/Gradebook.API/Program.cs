namespace Gradebook.API;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.RegisterGradebookServices(builder);

        builder.Services.AddHttpContextAccessor();

        builder.Services.AddControllers();
        
        MapperConfig.ConfigureMappings();

        builder.Services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddSingleton<MapsterMapper.IMapper, MapsterMapper.Mapper>();   

        // Option B: With custom config
     

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        else
        {
            app.UseHttpsRedirection();
        }

        // Configure the HTTP request pipeline.

        app.UseAuthentication();
        app.UseAuthorization();


        app.MapControllers();

        await app.MigrateDbAndSeedData();

        await app.CreateIndexes();

        app.Run();
    }
}
