namespace Parser.Application.Common.Results;

public class Result<TValue> : Result
{
    private Result(TValue data)
    {
        Data = data;
    }

    private Result(Error error) : base(error)
    {
    }

    private Result(IEnumerable<Error> errors) : base(errors)
    {
    }

    public TValue? Data { get; }

    public static Result<TValue> Success(TValue value) => new(value);

    public static Result<TValue> Failure(Error error) => new(error);

    public static Result<TValue> Failure(IEnumerable<Error> errors) => new(errors);

    public static implicit operator Result<TValue>(TValue value) => new(value);

    public static implicit operator Result<TValue>(Error error) => new(error);

    public static implicit operator Result<TValue>(List<Error> errors) => new(errors);
}