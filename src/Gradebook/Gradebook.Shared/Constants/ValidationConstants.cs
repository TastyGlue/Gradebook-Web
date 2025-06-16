namespace Gradebook.Shared.Constants;

public static class ValidationConstants
{
    public const string REQUIRED = "This field is required.";
    public const string EMAIL = "Please enter a valid email address.";
    public const string PHONE_NUMBER = "Please enter a valid phone number.";
    public const string WEBSITE = "Please enter a valid URL address.";
    public const string MIN_LENGTH = "The field must be at least {1} characters long.";
    public const string MAX_LENGTH = "The field must be at most {1} characters long.";

    public const int TEXT_FIELD_MAX_LENGTH = 100;
    public const int TEXT_FIELD_LARGE_MAX_LENGTH = 256;
}
