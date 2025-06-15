namespace Gradebook.Shared.Constants;

public static class ErrorCodes
{
    public const string LOGIN_NO_PROFILES = "LOGIN_100_403";
    public const string LOGIN_NO_ACTIVE_PROFILES = "LOGIN_110_403";
    public const string LOGIN_CREDENTIALS = "LOGIN_120_403";
    public const string LOGIN_PROFILE_NOT_ACTIVE = "LOGIN_130_403";
    public const string LOGIN_PROFILE_TOKEN_INVALID = "LOGIN_140_401";
    public const string LOGIN_PROFILE_TOKEN_EXPIRED = "LOGIN_150_401";
    public const string LOGIN_INACTIVE_USER = "LOGIN_160_403";

    public const string PROFILE_NOT_FOUND = "PROFILE_100_404";
    public const string PROFILE_NOT_BELONG_TO_USER = "PROFILE_110_403";

    public const string USER_NOT_AUTHENTICATED = "USER_100_401";

    public const string ENTITY_NOT_FOUND = "ENTITY_100_404";
    public const string ENTITY_MISMATCH_ID = "ENTITY_100_400";

    public const string ACCESS_NOT_AUTHENTICATED = "ACCESS_100_401";
    public const string ACCESS_NOT_AUTHORIZED = "ACCESS_110_403";

    public const string DB_UNIQUE_VIOLATION = "DB_100_400";
    public const string DB_FOREIGN_KEY_VIOLATION = "DB_110_400";
    public const string DB_UNEXPECTED_ERROR = "DB_120_500";
    public const string DB_NOT_NULL_VIOLATION = "DB_130_400";

    public const string API_UNEXPECTED_ERROR = "API_100_500";
    public const string API_VALIDATION_ERROR = "API_110_400";
    public const string API_INVALID_ARGUMENT = "API_120_400";
}
