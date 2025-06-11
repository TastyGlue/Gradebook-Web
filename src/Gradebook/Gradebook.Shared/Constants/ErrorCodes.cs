namespace Gradebook.Shared.Constants;

public static class ErrorCodes
{
    public const string LOGIN_NO_PROFILES = "LOGIN_100_403";
    public const string LOGIN_NO_ACTIVE_PROFILES = "LOGIN_110_403";
    public const string LOGIN_CREDENTIALS = "LOGIN_120_403";
    public const string LOGIN_PROFILE_NOT_ACTIVE = "LOGIN_130_403";
    public const string LOGIN_PROFILE_TOKEN_INVALID = "LOGIN_140_401";
    public const string LOGIN_PROFILE_TOKEN_EXPIRED = "LOGIN_150_401";

    public const string PROFILE_NOT_FOUND = "PROFILE_100_404";
    public const string PROFILE_NOT_BELONG_TO_USER = "PROFILE_110_403";

    public const string USER_NOT_AUTHENTICATED = "USER_100_401";
}
