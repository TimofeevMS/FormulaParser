using MediatR;
using Parser.Application.Common.Results;
using Parser.Domain.Interfaces;

namespace Parser.Application.UseCases.Templates.GetForMenu;

public class GetForMenuTemplateHandler : IRequestHandler<GetForMenuTemplateRequest, Result<IEnumerable<GetForMenuTemplateResponse>>>
{
    private readonly ITemplateRepository _repository;

    public GetForMenuTemplateHandler(IUnitOfWork unitOfWork)
    {
        _repository = unitOfWork.GetRepository<ITemplateRepository>();
    }
    
    public async Task<Result<IEnumerable<GetForMenuTemplateResponse>>> Handle(GetForMenuTemplateRequest request, CancellationToken cancellationToken)
    {
        var templates = await _repository.GetForMenuAsync<GetForMenuTemplateResponse>(cancellationToken);
        
        return Result<IEnumerable<GetForMenuTemplateResponse>>.Success(templates);
    }
}