namespace Gradebook.Shared.Models;

/// <summary>
/// Represents a request to log in with user credentials.
/// </summary>
/// <remarks>This record encapsulates the email and password required for authentication. Both properties must be
/// provided and adhere to any validation rules defined by the consuming system.</remarks>
/// <param name="Email">The email address of the user attempting to log in. This value cannot be null or empty.</param>
/// <param name="Password">The password associated with the user's account. This value cannot be null or empty.</param>
public record LoginCredentials(string Email, string Password);