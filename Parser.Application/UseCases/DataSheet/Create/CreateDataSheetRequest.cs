using MediatR;
using Parser.Application.Common.Results;

namespace Parser.Application.UseCases.DataSheet.Create;

public record CreateDataSheetRequest : IRequest<Result<Guid>>
{
    public Guid TemplateId { get; set; }
 
    public string Name { get; set; }
    
    public IEnumerable<CreateDataSheetAttribute> Values { get; set; }
}

public record CreateDataSheetAttribute
{
    public Guid Id { get; set; }
    
    public string? Value { get; set; }
}