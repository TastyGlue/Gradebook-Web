namespace Gradebook.Shared.Models;

/// <summary>Represents a claim containing information about a user's profile.</summary>
/// <remarks>Used in the authentication token to form the list of available profiles the user can choose from.</remarks>
/// <param name="RoleType">The type of role associated with the claim in its string form, such as <see cref="RoleType.Admin"/> or <see cref="RoleType.Teacher"/></param>
/// <param name="ProfileId">The unique identifier of the profile associated with the claim.</param>
/// <param name="SchoolName">The name of the school associated with the profile, or <see langword="null"/> if no school is specified.</param>
public record ProfileClaim(string RoleType, Guid ProfileId, string? SchoolName);