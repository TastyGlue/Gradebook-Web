namespace Gradebook.Data;

public class GradebookDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public GradebookDbContext(DbContextOptions<GradebookDbContext> options)
        : base(options)
    {
    }

    public DbSet<Absence> Absences { get; set; }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Class> Classes { get; set; }
    public DbSet<Grade> Grades { get; set; }
    public DbSet<Headmaster> Headmasters { get; set; }
    public DbSet<Parent> Parents { get; set; }
    public DbSet<Profile> Profiles { get; set; }
    public DbSet<School> Schools { get; set; }
    public DbSet<SchoolYear> SchoolYears { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<StudentParent> StudentParents { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<TeacherSubject> TeacherSubjects { get; set; }
    public DbSet<Timetable> Timetables { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureProfilesTable();

        modelBuilder.ConfigureManyToManyRelationships();

        modelBuilder.ConfigureRestrictDeleteBehavior();

        // Write the enum as a string in the database
        modelBuilder.Entity<Timetable>()
            .Property(x => x.DayOfWeek)
            .HasConversion<string>();
    }
}
