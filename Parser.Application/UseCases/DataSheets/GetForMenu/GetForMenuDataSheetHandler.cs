﻿using MediatR;
using Parser.Application.Common.Results;
using Parser.Domain.Interfaces;

namespace Parser.Application.UseCases.DataSheets.GetForMenu;

public class GetForMenuDataSheetHandler : IRequestHandler<GetForMenuDataSheetRequest, Result<IEnumerable<GetForMenuDataSheetResponse>>>
{
    private readonly IDataSheetRepository _repository;

    public GetForMenuDataSheetHandler(IUnitOfWork unitOfWork)
    {
        _repository = unitOfWork.GetRepository<IDataSheetRepository>();
    }
    
    public async Task<Result<IEnumerable<GetForMenuDataSheetResponse>>> Handle(GetForMenuDataSheetRequest request, CancellationToken cancellationToken)
    {
        var templates = await _repository.GetForMenuAsync<GetForMenuDataSheetResponse>(cancellationToken);
        
        return Result<IEnumerable<GetForMenuDataSheetResponse>>.Success(templates);
    }
}