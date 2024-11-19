using MediatR;
using Parser.Application.Common.Results;

namespace Parser.Application.UseCases.DataSheets.Create;

public record CreateDataSheetRequest(Guid TemplateId, string Name, IEnumerable<CreateDataSheetAttribute> Values) : IRequest<Result<Guid>>;

public record CreateDataSheetAttribute(Guid Id, string? Value);