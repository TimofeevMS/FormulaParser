using AutoMapper;
using MediatR;
using Parser.Application.Common;
using Parser.Application.Common.Results;
using Parser.Domain.Interfaces;

namespace Parser.Application.UseCases.DataSheets.GetTemplates;

public class GetTemplatesHandler : IRequestHandler<GetTemplatesRequest, Result<GetTemplatesResponse>>
{
    private readonly ITemplateRepository _repository;
    private readonly IMapper _mapper;

    public GetTemplatesHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _repository = unitOfWork.GetRepository<ITemplateRepository>();
        _mapper = mapper;
    }

    public async Task<Result<GetTemplatesResponse>> Handle(GetTemplatesRequest request, CancellationToken cancellationToken)
    {
        if (!request.Id.HasValue)
            return Errors.Template.Identifier;
        
        var template = await _repository.GetByIdAsync(request.Id.Value, cancellationToken);
        
        if (template is null)
            return Errors.Template.NotFound;

        var response = new GetTemplatesResponse()
                       {
                           Values = _mapper.Map<List<GetForDataSheetValue>>(template.Attributes)
                       };
        
        return response;
    }
}