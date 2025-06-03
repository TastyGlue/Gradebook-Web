namespace Gradebook.Shared.Models;

/// <summary>
/// Represents a violation related to a database index, including the name of the index and an associated error message.
/// </summary>
/// <param name="IndexName">The name of the index where the violation occurred. Cannot be null or empty.</param>
/// <param name="ErrorMessage">A descriptive message explaining the nature of the violation. Cannot be null or empty.</param>
public record IndexViolation(string IndexName, string ErrorMessage);
