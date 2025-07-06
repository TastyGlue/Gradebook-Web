namespace Gradebook.Shared.Enums;

public enum UserCreationType
{
    [Display(Name = "From existing User")]
    Existing = 0,

    [Display(Name = "Create new user")]
    New = 1
}
