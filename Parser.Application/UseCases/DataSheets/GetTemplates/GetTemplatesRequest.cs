using MediatR;
using Parser.Application.Common.Results;

namespace Parser.Application.UseCases.DataSheets.GetTemplates;

public record GetTemplatesRequest(Guid? Id) : IRequest<Result<GetTemplatesResponse>>;