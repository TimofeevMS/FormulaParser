using MediatR;
using Parser.Application.Common;
using Parser.Application.Common.Results;
using Parser.Domain.Entities;
using Parser.Domain.Interfaces;

namespace Parser.Application.UseCases.DataSheets.Create;

public class CreateDataSheetHandler : IRequestHandler<CreateDataSheetRequest, Result<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDataSheetEfCoreRepository _dataSheetEfCoreRepository;
    private readonly ITemplateEfCoreRepository _templateEfCoreRepository;

    public CreateDataSheetHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _dataSheetEfCoreRepository = unitOfWork.GetRepository<IDataSheetEfCoreRepository>();;
        _templateEfCoreRepository = unitOfWork.GetRepository<ITemplateEfCoreRepository>();;
    }

    public async Task<Result<Guid>> Handle(CreateDataSheetRequest request, CancellationToken cancellationToken)
    {
        var template = await _templateEfCoreRepository.GetByIdAsync(request.TemplateId, cancellationToken);

        if (template is null)
            return Errors.Template.NotFound;

        var values = request.Values
                            .Where(value => template.Attributes.Any(attribute => attribute.Id == value.Id))
                            .Select(value =>
                                    {
                                        var attribute = template.Attributes.Single(x => x.Id == value.Id);
                                        var dataSheetValue = new DataSheetValue
                                                             {
                                                                 Id = Guid.NewGuid(),
                                                                 TemplateAttribute = attribute
                                                             };
                                        dataSheetValue.SetValue(value.Value);
                                        return dataSheetValue;
                                    })
                            .ToList();

        var dataSheet = new DataSheet
                        {
                            Id = Guid.NewGuid(),
                            Name = request.Name,
                            TemplateId = request.TemplateId,
                            Values = values
                        };

        await _dataSheetEfCoreRepository.AddOrUpdateAsync(dataSheet, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return dataSheet.Id;
    }
}