using MediatR;
using Parser.Application.Common;
using Parser.Application.Common.Results;
using Parser.Application.Interfaces;
using Parser.Domain.Dto;
using Parser.Domain.Interfaces;

namespace Parser.Application.UseCases.DataSheets.Calculate;

public class CalculateFormulaHandler : IRequestHandler<CalculateFormulaRequest, Result<CalculateFormulaResponse>>
{
    private readonly IDataSheetRepository _repository;
    private readonly IFormulaService _formulaService;

    public CalculateFormulaHandler(IUnitOfWork unitOfWork, IFormulaService formulaService)
    {
        _repository = unitOfWork.GetRepository<IDataSheetRepository>();
        _formulaService = formulaService;
    }
    
    public async Task<Result<CalculateFormulaResponse>> Handle(CalculateFormulaRequest request, CancellationToken cancellationToken)
    {
        if (!request.DataSheetId.HasValue || !request.ValueId.HasValue)
            return Errors.DataSheet.Identifier;
        
        var dataSheet = await _repository.GetByIdAsync(request.DataSheetId.Value, cancellationToken);
        
        if (dataSheet == null)
            return Errors.DataSheet.NotFound;
        
        var formulaContext = new FormulaContext
        {
            DataSheet = dataSheet,
            Formula = dataSheet.Values.First(a => a.Id == request.ValueId).TemplateAttribute.Formula!,
            VariableDefinitions = new Dictionary<string, (string FormulaOrValue, bool IsFormula)>()
        };
        
        var result = _formulaService.Evaluate(formulaContext);

        var response = new CalculateFormulaResponse
                       {
                           Result = result
                       };

        return response;
    }
}