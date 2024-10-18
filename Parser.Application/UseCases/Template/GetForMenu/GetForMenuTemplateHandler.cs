using MediatR;
using Parser.Application.Common.Results;
using Parser.Domain.Interfaces;

namespace Parser.Application.UseCases.Template.GetForMenu;

public class GetForMenuTemplateHandler : IRequestHandler<GetForMenuTemplateRequest, Result<IEnumerable<GetForMenuTemplateResponse>>>
{
    private readonly ITemplateRepository _repository;

    public GetForMenuTemplateHandler(ITemplateRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<Result<IEnumerable<GetForMenuTemplateResponse>>> Handle(GetForMenuTemplateRequest request, CancellationToken cancellationToken)
    {
        var templates = await _repository.GetForMenuTemplates<GetForMenuTemplateResponse>(cancellationToken);
        
        return Result<IEnumerable<GetForMenuTemplateResponse>>.Success(templates);
    }
}