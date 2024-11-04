using MediatR;
using Microsoft.AspNetCore.Mvc;
using Parser.Application.Common.Results;

namespace Parser.Application.UseCases.Templates.Delete;

public record DeleteTemplateRequest : IRequest<Result>
{
    [FromRoute]
    public Guid? Id { get; init; }
}