namespace Gradebook.Shared.Exceptions;

public class LoginException : CustomException
{
    public LoginException(ErrorResult error, Exception? innerException = null) : base(error, innerException) { }
}