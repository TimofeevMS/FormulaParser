namespace Parser.Application.Common.Results;

public enum ErrorType
{
    Failure,
    Unexpected,
    Validation,
    Conflict,
    NotFound,
    Forbidden,
    Unauthorized
}