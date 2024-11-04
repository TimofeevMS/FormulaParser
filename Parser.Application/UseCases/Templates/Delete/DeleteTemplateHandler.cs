using MediatR;
using Parser.Application.Common;
using Parser.Application.Common.Results;
using Parser.Domain.Interfaces;

namespace Parser.Application.UseCases.Templates.Delete;

public class DeleteTemplateHandler : IRequestHandler<DeleteTemplateRequest, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITemplateRepository _repository;

    public DeleteTemplateHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _repository = unitOfWork.GetRepository<ITemplateRepository>();
    }
    
    public async Task<Result> Handle(DeleteTemplateRequest request, CancellationToken cancellationToken)
    {
        if (!request.Id.HasValue)
            return Errors.Template.Identifier;
    
        var template = await _repository.GetByIdAsync(request.Id.Value, cancellationToken);
    
        if (template is null)
            return Errors.Template.NotFound;
        
        _repository.Delete(template);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    
        return Result.Success();
    }
}