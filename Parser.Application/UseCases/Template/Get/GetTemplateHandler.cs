using AutoMapper;
using MediatR;
using Parser.Application.Common;
using Parser.Application.Common.Results;
using Parser.Domain.Interfaces;

namespace Parser.Application.UseCases.Template.Get;

public class GetTemplateHandler : IRequestHandler<GetTemplateRequest, Result<GetTemplateResponse>>
{
    private readonly ITemplateRepository _repository;
    private readonly IMapper _mapper;

    public GetTemplateHandler(ITemplateRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<Result<GetTemplateResponse>> Handle(GetTemplateRequest request, CancellationToken cancellationToken)
    {
        if (!request.Id.HasValue)
            return Errors.Template.Identifier;
        
        var template = await _repository.GetTemplate(request.Id.Value, cancellationToken);
        
        if (template is null)
            return Errors.Template.NotFound;
        
        var response = _mapper.Map<GetTemplateResponse>(template);

        return Result<GetTemplateResponse>.Success(response);
    }
}