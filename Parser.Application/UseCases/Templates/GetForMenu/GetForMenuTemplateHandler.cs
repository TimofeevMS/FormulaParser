using MediatR;
using Parser.Application.Common.Results;
using Parser.Domain.Interfaces;

namespace Parser.Application.UseCases.Templates.GetForMenu;

public class GetForMenuTemplateHandler : IRequestHandler<GetForMenuTemplateRequest, Result<IEnumerable<GetForMenuTemplateResponse>>>
{
    private readonly ITemplateDapperRepository _efCoreRepository;

    public GetForMenuTemplateHandler(IUnitOfWork unitOfWork)
    {
        _efCoreRepository = unitOfWork.GetRepository<ITemplateDapperRepository>();
    }
    
    public async Task<Result<IEnumerable<GetForMenuTemplateResponse>>> Handle(GetForMenuTemplateRequest request, CancellationToken cancellationToken)
    {
        var templates = await _efCoreRepository.GetForMenuAsync<GetForMenuTemplateResponse>(cancellationToken);
        
        return Result<IEnumerable<GetForMenuTemplateResponse>>.Success(templates);
    }
}