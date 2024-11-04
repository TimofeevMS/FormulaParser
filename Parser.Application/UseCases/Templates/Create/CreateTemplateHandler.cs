using AutoMapper;
using MediatR;
using Parser.Application.Common.Results;
using Parser.Domain.Entities;
using Parser.Domain.Interfaces;

namespace Parser.Application.UseCases.Templates.Create;

public class CreateTemplateHandler : IRequestHandler<CreateTemplateRequest, Result<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITemplateRepository _repository;
    private readonly IMapper _mapper;

    public CreateTemplateHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _repository = unitOfWork.GetRepository<ITemplateRepository>();
        _mapper = mapper;
    }
    
    public async Task<Result<Guid>> Handle(CreateTemplateRequest request, CancellationToken cancellationToken)
    {
        var template = new Template
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Attributes = _mapper.Map<List<TemplateAttribute>>(request.Attributes)
        };
        
        await _repository.AddOrUpdateAsync(template, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return template.Id;
    }
}