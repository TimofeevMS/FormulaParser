using MediatR;
using Parser.Application.Common;
using Parser.Application.Common.Results;
using Parser.Domain.Entities;
using Parser.Domain.Interfaces;

namespace Parser.Application.UseCases.DataSheets.Create;

public class CreateDataSheetHandler : IRequestHandler<CreateDataSheetRequest, Result<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDataSheetRepository _dataSheetRepository;
    private readonly ITemplateRepository _templateRepository;

    public CreateDataSheetHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _dataSheetRepository = unitOfWork.GetRepository<IDataSheetRepository>();;
        _templateRepository = unitOfWork.GetRepository<ITemplateRepository>();;
    }

    public async Task<Result<Guid>> Handle(CreateDataSheetRequest request, CancellationToken cancellationToken)
    {
        var template = await _templateRepository.GetByIdAsync(request.TemplateId, cancellationToken);

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

        await _dataSheetRepository.AddOrUpdateAsync(dataSheet, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return dataSheet.Id;
    }
}