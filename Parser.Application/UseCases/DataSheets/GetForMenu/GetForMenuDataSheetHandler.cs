using MediatR;
using Parser.Application.Common.Results;
using Parser.Domain.Interfaces;

namespace Parser.Application.UseCases.DataSheets.GetForMenu;

public class GetForMenuDataSheetHandler : IRequestHandler<GetForMenuDataSheetRequest, Result<IEnumerable<GetForMenuDataSheetResponse>>>
{
    private readonly IDataSheetEfCoreRepository _efCoreRepository;

    public GetForMenuDataSheetHandler(IUnitOfWork unitOfWork)
    {
        _efCoreRepository = unitOfWork.GetRepository<IDataSheetEfCoreRepository>();
    }
    
    public async Task<Result<IEnumerable<GetForMenuDataSheetResponse>>> Handle(GetForMenuDataSheetRequest request, CancellationToken cancellationToken)
    {
        var templates = await _efCoreRepository.GetForMenuAsync<GetForMenuDataSheetResponse>(cancellationToken);
        
        return Result<IEnumerable<GetForMenuDataSheetResponse>>.Success(templates);
    }
}