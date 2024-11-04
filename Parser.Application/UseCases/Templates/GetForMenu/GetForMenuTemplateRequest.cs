using MediatR;
using Parser.Application.Common.Results;

namespace Parser.Application.UseCases.Templates.GetForMenu;

public record GetForMenuTemplateRequest : IRequest<Result<IEnumerable<GetForMenuTemplateResponse>>>;