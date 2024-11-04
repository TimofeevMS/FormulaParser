using MediatR;
using Parser.Application.Common.Results;

namespace Parser.Application.UseCases.Templates.Get;

public record GetTemplateRequest : IRequest<Result<GetTemplateResponse>>
{
    public Guid? Id { get; init; }
}