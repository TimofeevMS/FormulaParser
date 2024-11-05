using MediatR;
using Parser.Application.Common;
using Parser.Application.Common.Results;
using Parser.Domain.Interfaces;

namespace Parser.Application.UseCases.Templates.Delete;

public class DeleteTemplateHandler : IRequestHandler<DeleteTemplateRequest, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITemplateEfCoreRepository _efCoreRepository;

    public DeleteTemplateHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _efCoreRepository = unitOfWork.GetRepository<ITemplateEfCoreRepository>();
    }
    
    public async Task<Result> Handle(DeleteTemplateRequest request, CancellationToken cancellationToken)
    {
        if (!request.Id.HasValue)
            return Errors.Template.Identifier;
    
        var template = await _efCoreRepository.GetByIdAsync(request.Id.Value, cancellationToken);
    
        if (template is null)
            return Errors.Template.NotFound;
        
        _efCoreRepository.Delete(template);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    
        return Result.Success();
    }
}