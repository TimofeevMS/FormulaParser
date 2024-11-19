using MediatR;
using Parser.Application.Common.Results;

namespace Parser.Application.UseCases.Templates.Get;

public record GetTemplateRequest(Guid? Id) : IRequest<Result<GetTemplateResponse>>;