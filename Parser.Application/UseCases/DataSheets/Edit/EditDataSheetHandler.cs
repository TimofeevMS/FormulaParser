using AutoMapper;
using MediatR;
using Parser.Application.Common;
using Parser.Application.Common.Results;
using Parser.Domain.Entities;
using Parser.Domain.Interfaces;

namespace Parser.Application.UseCases.DataSheets.Edit;

public class EditDataSheetHandler : IRequestHandler<EditDataSheetRequest, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDataSheetRepository _repository;
    private readonly IMapper _mapper;

    public EditDataSheetHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _repository = unitOfWork.GetRepository<IDataSheetRepository>();
        _mapper = mapper;
    }

    public async Task<Result> Handle(EditDataSheetRequest request, CancellationToken cancellationToken)
    {
        if (!request.Id.HasValue)
            return Errors.DataSheet.Identifier;
        
        var dataSheet = await _repository.GetByIdAsync(request.Id.Value, cancellationToken);

        if (dataSheet is null)
            return Errors.DataSheet.NotFound;
        
        dataSheet.Values.RemoveAll(a => request.Body.Values.All(val => val.Id != a.Id));
    
        foreach (var value in request.Body.Values)
        {
            var existingValue = dataSheet.Values.FirstOrDefault(a => a.Id == value.Id);
            
            if (existingValue is not null)
            {
                existingValue.SetValue(value.Value);
            }
            else
            {
                dataSheet.Values.Add(_mapper.Map<DataSheetValue>(value));
            }
        }
        
        await _repository.AddOrUpdateAsync(dataSheet, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}