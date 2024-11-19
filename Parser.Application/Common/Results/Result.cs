namespace Parser.Application.Common.Results;

public class Result
{
    protected Result()
    {
        Succeeded = true;
        Errors = Array.Empty<Error>();
    }

    protected Result(Error error)
    {
        Succeeded = false;
        Errors = [error];
    }

    protected Result(IEnumerable<Error> errors)
    {
        Succeeded = false;
        Errors = errors.ToArray();
    }
    
    public bool Succeeded { get; }

    public Error[] Errors { get; }

    public static Result Combine(params Result[] results)
    {
        var errors = results.Where(r => !r.Succeeded).SelectMany(r => r.Errors).ToArray();
        return errors.Length > 0 ? Failure(errors) : Success();
    }
    
    public static Result Success() => new();

    public static Result Failure(Error error) => new(error);
    
    public static Result Failure(IEnumerable<Error> error) => new(error);

    public static implicit operator Result(Error error) => new(error);
}