using MediatR;
using Parser.Application.Common.Results;
using Parser.Domain.Interfaces;

namespace Parser.Application.UseCases.DataSheet.GetForMenu;

public class GetForMenuDataSheetHandler : IRequestHandler<GetForMenuDataSheetRequest, Result<IEnumerable<GetForMenuDataSheetResponse>>>
{
    private readonly IDataSheetRepository _repository;

    public GetForMenuDataSheetHandler(IDataSheetRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<Result<IEnumerable<GetForMenuDataSheetResponse>>> Handle(GetForMenuDataSheetRequest request, CancellationToken cancellationToken)
    {
        var templates = await _repository.GetForMenuTemplates<GetForMenuDataSheetResponse>(cancellationToken);
        
        return Result<IEnumerable<GetForMenuDataSheetResponse>>.Success(templates);
    }
}