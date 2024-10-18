using AutoMapper;
using MediatR;
using Parser.Application.Common;
using Parser.Application.Common.Results;
using Parser.Domain.Interfaces;

namespace Parser.Application.UseCases.Template.GetForDataSheet;

public class GetForDataSheetHandler : IRequestHandler<GetForDataSheetRequest, Result<GetForDataSheetResponse>>
{
    private readonly ITemplateRepository _repository;
    private readonly IMapper _mapper;

    public GetForDataSheetHandler(ITemplateRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<GetForDataSheetResponse>> Handle(GetForDataSheetRequest request, CancellationToken cancellationToken)
    {
        if (!request.Id.HasValue)
            return Errors.Template.Identifier;
        
        var template = await _repository.GetTemplate(request.Id.Value, cancellationToken);
        
        if (template is null)
            return Errors.Template.NotFound;

        var response = new GetForDataSheetResponse()
                       {
                           Values = _mapper.Map<List<GetForDataSheetValue>>(template.Attributes)
                       };
        
        return response;
    }
}