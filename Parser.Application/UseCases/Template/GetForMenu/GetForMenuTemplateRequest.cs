using MediatR;
using Parser.Application.Common.Results;

namespace Parser.Application.UseCases.Template.GetForMenu;

public record GetForMenuTemplateRequest : IRequest<Result<IEnumerable<GetForMenuTemplateResponse>>>;