using AutoMapper;
using MediatR;
using Parser.Application.Common.Results;
using Parser.Domain.Entities;
using Parser.Domain.Interfaces;

namespace Parser.Application.UseCases.Template.Create;

public class CreateTemplateHandler : IRequestHandler<CreateTemplateRequest, Result<Guid>>
{
    private readonly ITemplateRepository _repository;
    private readonly IMapper _mapper;

    public CreateTemplateHandler(ITemplateRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<Result<Guid>> Handle(CreateTemplateRequest templateRequest, CancellationToken cancellationToken)
    {
        var template = new Domain.Entities.Template
        {
            Id = Guid.NewGuid(),
            Name = templateRequest.Name,
            Attributes = _mapper.Map<List<TemplateAttribute>>(templateRequest.Attributes)
        };
        
        await _repository.AddTemplate(template, cancellationToken);
        
        return template.Id;
    }
}