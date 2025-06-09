namespace Gradebook.Shared.Enums;

/// <summary>
/// Represents all the roles the system supports.
/// </summary>
/// <remarks>It is used as the discriminator property on the "Profiles" table</remarks>
public enum RoleType
{
    Admin = 10,
    Headmaster = 20,
    Teacher = 30,
    Student = 40,
    Parent = 50
}
