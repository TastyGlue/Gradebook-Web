namespace Gradebook.API.Interfaces;

/// <summary>
/// Defines methods for generating various types of tokens used for authentication and authorization.
/// </summary>
/// <remarks>This service provides functionality to generate authentication tokens, access tokens, and refresh
/// tokens. It is intended to be used in scenarios where secure token generation is required for user authentication and
/// session management.</remarks>
public interface ITokenService
{
    /// <summary>
    /// Generates an authentication token for the specified user and their associated profiles.
    /// </summary>
    /// <remarks>The generated token is issued on initial authentication of the user and provides a list of all the 
    /// profiles the user can log into. The token is not meant for authorization.</remarks>
    /// <param name="user">The user for whom the authentication token is being generated. Cannot be null.</param>
    /// <param name="profiles">A list of profiles associated with the user. Cannot be null or empty.</param>
    /// <returns>A string representing the generated authentication token.</returns>
    string GenerateAuthToken(User user, IList<Profile> profiles);

    /// <summary>
    /// Generates an access token for the specified user profile.
    /// </summary>
    /// <param name="profile">The user profile for which the access token is generated. This parameter cannot be null.</param>
    /// <returns>A string representing the generated access token. The token is unique to the provided profile and may be used
    /// for authorization purposes.</returns>
    string GenerateAccessToken(Profile profile);

    /// <summary>
    /// Generates a new refresh token.
    /// </summary>
    /// <returns>A string representing the newly generated refresh token. The token is typically used for authentication or
    /// session management.</returns>
    string GenerateRefreshToken();
}