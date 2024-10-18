using MediatR;
using Parser.Application.Common;
using Parser.Application.Common.Results;
using Parser.Domain.Entities;
using Parser.Domain.Interfaces;

namespace Parser.Application.UseCases.DataSheet.Create;

public class CreateDataSheetHandler : IRequestHandler<CreateDataSheetRequest, Result<Guid>>
{
    private readonly IDataSheetRepository _dataSheetRepository;
    private readonly ITemplateRepository _templateRepository;

    public CreateDataSheetHandler(IDataSheetRepository dataSheetRepository, ITemplateRepository templateRepository)
    {
        _dataSheetRepository = dataSheetRepository;
        _templateRepository = templateRepository;
    }

    public async Task<Result<Guid>> Handle(CreateDataSheetRequest request, CancellationToken cancellationToken)
    {
        var template = await _templateRepository.GetTemplate(request.TemplateId, cancellationToken);

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

        var dataSheet = new Domain.Entities.DataSheet
                        {
                            Id = Guid.NewGuid(),
                            Name = request.Name,
                            Template = template,
                            Values = values
                        };

        await _dataSheetRepository.AddDataSheet(dataSheet, cancellationToken);

        return dataSheet.Id;
    }
}