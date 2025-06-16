namespace Gradebook.Shared.Constants;

public static class Constants
{
    public const string DEFAULT_PASSWORD = "P@ssw0rd";

    public const string SCHOOL_YEAR_FORMAT = "{0}/{1}";

    public const string API_CLIENT_NAME = "GradebookAPIClient";

    public const string ACCESS_TOKEN_KEY = "accessToken";

    public const string EMAIL_FORMAT_REGEX = @"^[a-zA-Z0-9._%±]+@[a-zA-Z0-9.-]+.[a-zA-Z]{2,}$";

    public const string PHONE_FORMAT_REGEX = @"(?:\+359|00359|0)\s?(8[7-9][0-9])[\s\-]?([0-9]{3})[\s\-]?([0-9]{3,4})";

    public const string WEBSITE_FORMAT_REGEX = @"https?:\/\/[^\s/$.?#].[^\s]*";
}
