using AutoMapper;
using MediatR;
using Parser.Application.Common;
using Parser.Application.Common.Results;
using Parser.Domain.Interfaces;

namespace Parser.Application.UseCases.DataSheet.Get;

public class GetDataSheetHandler : IRequestHandler<GetDataSheetRequest, Result<GetDataSheetResponse>>
{
    private readonly IDataSheetRepository _repository;
    private readonly IMapper _mapper;

    public GetDataSheetHandler(IDataSheetRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<GetDataSheetResponse>> Handle(GetDataSheetRequest request, CancellationToken cancellationToken)
    {
        if (!request.Id.HasValue)
            return Errors.DataSheet.Identifier;
        
        var dataSheet = await _repository.GetDataSheet(request.Id.Value, cancellationToken);
        
        if (dataSheet is null)
            return Errors.DataSheet.NotFound;
        
        var response = _mapper.Map<GetDataSheetResponse>(dataSheet);
        
        return response;
    }
}