using System.Reflection;

namespace Gradebook.Data.Extensions;

public static class ModelBuilderExtensions
{
    /// <summary>
    /// Configures the database table mappings and relationships for the <see cref="Profile"/> entity and its derived
    /// types.
    /// </summary>
    /// <remarks>This method sets up a discriminator column to distinguish between different derived types of
    /// <see cref="Profile"/> based on the <see cref="RoleType"/> property. It also configures specific column mappings
    /// for properties in the derived types, such as <see cref="SchoolId"/>, <see cref="BusinessEmail"/>, <see
    /// cref="BusinessPhoneNumber"/>, and <see cref="ClassId"/>.  Use this method as part of the Entity Framework Core
    /// model configuration to ensure proper table structure and relationships for the <see cref="Profile"/>
    /// hierarchy.</remarks>
    /// <param name="modelBuilder">The <see cref="ModelBuilder"/> instance used to configure the entity mappings.</param>
    public static void ConfigureProfilesTable(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Profile>()
            .HasDiscriminator(x => x.RoleType)
            .HasValue<Admin>(RoleType.Admin)
            .HasValue<Headmaster>(RoleType.Headmaster)
            .HasValue<Teacher>(RoleType.Teacher)
            .HasValue<Student>(RoleType.Student)
            .HasValue<Parent>(RoleType.Parent);

        modelBuilder.Entity<Teacher>()
            .HasOne(t => t.Class)
            .WithOne(c => c.ClassTeacher);

        modelBuilder.Entity<Class>()
            .HasOne(c => c.ClassTeacher)
            .WithOne(t => t.Class)
            .HasForeignKey<Class>(c => c.ClassTeacherId)
            .IsRequired(false);

        modelBuilder.ConfigureProfileRepeatingColumns();

        modelBuilder.ConfigureProfileNullableColumns();

        modelBuilder.Entity<Headmaster>()
            .Ignore(x => x.SchoolName);

        modelBuilder.Entity<Teacher>()
            .Ignore(x => x.SchoolName);

        modelBuilder.Entity<Student>()
            .Ignore(x => x.SchoolName);
    }

    /// <summary>
    /// Configures many-to-many relationships for the specified <see cref="ModelBuilder"/>.
    /// </summary>
    /// <remarks>This method sets up many-to-many relationships between entities in the model, including:
    /// <list type="bullet"> <item> <description>Students and Parents, using the <see cref="StudentParent"/> join
    /// entity.</description> </item> <item> <description>Teachers and Subjects, using the <see cref="TeacherSubject"/>
    /// join entity.</description> </item> </list> Each relationship is configured with appropriate foreign keys to
    /// ensure proper navigation and data integrity.</remarks>
    /// <param name="modelBuilder">The <see cref="ModelBuilder"/> instance used to configure the entity relationships.</param>
    public static void ConfigureManyToManyRelationships(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>()
            .HasMany(s => s.Parents)
            .WithMany(p => p.Students)
            .UsingEntity<StudentParent>();

        modelBuilder.Entity<Teacher>()
            .HasMany(s => s.Subjects)
            .WithMany(p => p.Teachers)
            .UsingEntity<TeacherSubject>();
    }

    /// <summary>
    /// Configures repeating column mappings for Profile entity types.
    /// </summary>
    /// <remarks>This method sets consistent column names for shared properties across Profile entities. It
    /// ensures that properties such as <c>SchoolId</c>, <c>BusinessEmail</c>, <c>BusinessPhoneNumber</c>,  and
    /// <c>ClassId</c> are mapped to the same column names in the database schema.</remarks>
    /// <param name="modelBuilder">The <see cref="ModelBuilder"/> instance used to configure the entity mappings.</param>
    private static void ConfigureProfileRepeatingColumns(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Headmaster>()
            .Property(x => x.SchoolId)
            .HasColumnName("SchoolId");

        modelBuilder.Entity<Teacher>()
            .Property(x => x.SchoolId)
            .HasColumnName("SchoolId");

        modelBuilder.Entity<Student>()
            .Property(x => x.SchoolId)
            .HasColumnName("SchoolId");

        modelBuilder.Entity<Admin>()
            .Property(x => x.BusinessEmail)
            .HasColumnName("BusinessEmail");

        modelBuilder.Entity<Headmaster>()
            .Property(x => x.BusinessEmail)
            .HasColumnName("BusinessEmail");

        modelBuilder.Entity<Teacher>()
            .Property(x => x.BusinessEmail)
            .HasColumnName("BusinessEmail");

        modelBuilder.Entity<Headmaster>()
            .Property(x => x.BusinessPhoneNumber)
            .HasColumnName("BusinessPhoneNumber");

        modelBuilder.Entity<Teacher>()
            .Property(x => x.BusinessPhoneNumber)
            .HasColumnName("BusinessPhoneNumber");

        modelBuilder.Entity<Student>()
            .Property(x => x.ClassId)
            .HasColumnName("ClassId");

        modelBuilder.Entity<Teacher>()
            .Property(x => x.ClassId)
            .HasColumnName("ClassId");
    }

    /// <summary>
    /// Configures nullable columns for all derived types of the <see cref="Profile"/> class.
    /// </summary>
    /// <remarks>This method identifies all derived types of the <see cref="Profile"/> class within the
    /// current application domain. For each derived type, it marks scalar properties that are not inherited from the
    /// base <see cref="Profile"/> class and are not navigation properties as optional in the database schema.</remarks>
    /// <param name="modelBuilder">The <see cref="ModelBuilder"/> instance used to configure the entity types and their properties.</param>
    private static void ConfigureProfileNullableColumns(this ModelBuilder modelBuilder)
    {
        var baseEntityType = typeof(Profile);
        var model = modelBuilder.Model;

        // Get all derived types of Profile
        var derivedTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(baseEntityType));

        foreach (var derivedType in derivedTypes)
        {
            // Get the base Profile properties to check against
            var baseProperties = new HashSet<string>(
                baseEntityType.GetProperties().Select(p => p.Name)
            );

            var entityType = model.FindEntityType(derivedType);
            if (entityType == null) continue;

            // Get navigation properties for the derived type
            var navigationNames = entityType
                .GetNavigations()
                .Select(n => n.Name)
                .ToHashSet();

            var entityBuilder = modelBuilder.Entity(derivedType);

            foreach (var prop in derivedType.GetProperties())
            {
                if (baseProperties.Contains(prop.Name))
                    continue;

                if (navigationNames.Contains(prop.Name))
                    continue;

                if (!prop.IsScalarProperty())
                    continue;

                // Mark as optional in DB
                entityBuilder.Property(prop.Name).IsRequired(false);
            }
        }
    }

    /// <summary>
    /// Configures the delete behavior for all foreign key relationships in the model to use <see
    /// cref="DeleteBehavior.Restrict"/>.
    /// </summary>
    /// <remarks>This method iterates through all foreign key relationships in the model and sets their delete
    /// behavior to  <see cref="DeleteBehavior.Restrict"/>, ensuring that dependent entities are not automatically
    /// deleted when  their principal entities are removed. Use this method to enforce referential integrity constraints
    /// in the database.</remarks>
    /// <param name="modelBuilder">The <see cref="ModelBuilder"/> used to configure the entity model.</param>
    public static void ConfigureRestrictDeleteBehavior(this ModelBuilder modelBuilder)
    {
        foreach (var foreignKey in modelBuilder.Model
            .GetEntityTypes()
            .SelectMany(e => e.GetForeignKeys()))
        {
            foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }

    /// <summary>
    /// Determines whether the specified property represents a scalar value.
    /// </summary>
    /// <remarks>A scalar property is one that holds a single value, as opposed to a collection or complex
    /// type.</remarks>
    /// <param name="prop">The <see cref="PropertyInfo"/> instance representing the property to evaluate.</param>
    /// <returns><see langword="true"/> if the property is a scalar type</returns>
    private static bool IsScalarProperty(this PropertyInfo prop)
    {
        var type = prop.PropertyType;

        // Accept string, primitives, enums, DateTime, decimal, etc.
        return (type.IsPrimitive || type.IsEnum ||
            type == typeof(string) || type == typeof(decimal) ||
            type == typeof(DateTime) || type == typeof(DateTimeOffset));
    }
}
