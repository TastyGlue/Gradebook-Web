namespace Gradebook.Shared.Exceptions;

public class CustomException : Exception
{
    public ErrorResult? Error { get; set; }

    public CustomException(string message) : base(message)
    {
    }

    public CustomException(ErrorResult error) : base(error.Message) 
    {
        Error = error;
    }

    public CustomException(ErrorResult error, Exception innerException) : base(error.Message, innerException)
    {
        Error = error;
    }
}