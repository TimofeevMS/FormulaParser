using System.Text.Json.Serialization;

namespace Parser.Application.Common.Results;

public readonly record struct Error
{
    public string Code { get; }

    public string Description { get; }

    [JsonIgnore]
    public ErrorType Type { get; }

    private Error(ErrorType type, string code, string description)
    {
        Type = type;
        Code = code;
        Description = description;
    }

    public static implicit operator string(Error error) => error.Description;

    public static Error Failure(string code = "General.Failure",
                                string description = "A failure error has occurred.") =>
        new(ErrorType.Failure, code, description);

    public static Error Unexpected(string code = "General.Unexpected",
                                   string description = "An unexpected error has occurred.") =>
        new(ErrorType.Unexpected, code, description);

    public static Error Validation(string code = "General.Validation",
                                   string description = "A validation error has occurred.") =>
        new(ErrorType.Validation, code, description);

    public static Error Conflict(string code = "General.Conflict",
                                 string description = "A conflict error has occurred.") =>
        new(ErrorType.Conflict, code, description);

    public static Error NotFound(string code = "General.NotFound",
                                 string description = "A 'Not Found' error has occurred.") =>
        new(ErrorType.NotFound, code, description);

    public static Error Forbidden(string code = "General.Forbidden",
                                  string description = "A 'Forbidden' error has occurred.") =>
        new(ErrorType.Forbidden, code, description);

    public static Error Unauthorized(string code = "General.Unauthorized",
                                     string description = "A 'Unauthorized' error has occurred.") =>
        new(ErrorType.Unauthorized, code, description);

    public static Error Custom(int type,
                               string code,
                               string description) =>
        new((ErrorType)type, code, description);
}