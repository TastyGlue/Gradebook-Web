namespace Gradebook.Shared.Models;

public class CustomResult<T> : CustomResult
{
    public T? Value { get; set; }

    public CustomResult(T value) : base(null)
    {
        Value = value;
    }

    public CustomResult(ErrorResult error) : base(error)
    {
        Value = default;
    }
}

public class CustomResult
{
    public bool Succeeded => Error is null;

    public ErrorResult? Error { get; set; }

    public CustomResult(ErrorResult? error)
    {
        Error = error;
    }
}
