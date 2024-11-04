using AutoMapper;
using MediatR;
using Parser.Application.Common;
using Parser.Application.Common.Results;
using Parser.Domain.Entities;
using Parser.Domain.Interfaces;

namespace Parser.Application.UseCases.Templates.Edit;

public class EditTemplateHandler : IRequestHandler<EditTemplateRequest, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITemplateRepository _repository;
    private readonly IMapper _mapper;

    public EditTemplateHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _repository = unitOfWork.GetRepository<ITemplateRepository>();
        _mapper = mapper;
    }
    
    public async Task<Result> Handle(EditTemplateRequest request, CancellationToken cancellationToken)
    {
        if (!request.Id.HasValue)
            return Errors.Template.Identifier;
    
        var template = await _repository.GetByIdAsync(request.Id.Value, cancellationToken);
    
        if (template is null)
            return Errors.Template.NotFound;
        
        template.Attributes.RemoveAll(a => request.Body.Attributes.All(attr => attr.Id != a.Id));
    
        foreach (var attribute in request.Body.Attributes)
        {
            var existingAttribute = template.Attributes.FirstOrDefault(a => a.Id == attribute.Id);
            if (existingAttribute != null)
            {
                existingAttribute.Name = attribute.Name;
                existingAttribute.Description = attribute.Description;
                existingAttribute.Type = attribute.Type;
                existingAttribute.Formula = attribute.Formula;
            }
            else
            {
                template.Attributes.Add(_mapper.Map<TemplateAttribute>(attribute));
            }
        }
        
        await _repository.AddOrUpdateAsync(template, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    
        return Result.Success();
    }
}