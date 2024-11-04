using MediatR;
using Parser.Application.Common.Results;

namespace Parser.Application.UseCases.DataSheets.GetTemplates;

public record GetTemplatesRequest : IRequest<Result<GetTemplatesResponse>>
{
    public Guid? Id { get; init; }
}