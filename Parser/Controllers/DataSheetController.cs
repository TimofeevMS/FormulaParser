using Microsoft.AspNetCore.Mvc;
using Parser.Application.Common.Results;
using Parser.Application.UseCases.DataSheets.Calculate;
using Parser.Application.UseCases.DataSheets.Create;
using Parser.Application.UseCases.DataSheets.Edit;
using Parser.Application.UseCases.DataSheets.Get;
using Parser.Application.UseCases.DataSheets.GetForMenu;
using Parser.Application.UseCases.DataSheets.GetTemplates;
using Parser.Controllers.Common;

namespace Parser.Controllers;

[Route("api/[controller]")]
public class DataSheetController : ApiController
{
    [HttpPost]
    [ProducesResponseType(typeof(Result<Guid>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<Result<Guid>> Create([FromBody] CreateDataSheetRequest request, CancellationToken cancellationToken) => await Sender.Send(request, cancellationToken);
    
    [HttpGet("{Id:guid}")]
    [ProducesResponseType(typeof(Result<GetDataSheetResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<Result<GetDataSheetResponse>> Get([FromRoute] GetDataSheetRequest request, CancellationToken cancellationToken) => await Sender.Send(request, cancellationToken);
    
    [HttpGet("for-menu")]
    [ProducesResponseType(typeof(Result<IEnumerable<GetForMenuDataSheetResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<Result<IEnumerable<GetForMenuDataSheetResponse>>> GetForMenu(CancellationToken cancellationToken) => await Sender.Send(new GetForMenuDataSheetRequest(), cancellationToken);
    
    [HttpPut("{Id:guid}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<Result> Edit([FromBody] EditDataSheetRequest request, CancellationToken cancellationToken) => await Sender.Send(request, cancellationToken);
    
    [HttpGet("templates/{Id:guid}")]
    [ProducesResponseType(typeof(Result<GetTemplatesResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<Result<GetTemplatesResponse>> GetTemplates([FromRoute] GetTemplatesRequest request, CancellationToken cancellationToken) => await Sender.Send(request, cancellationToken);
    
    [HttpPost("calculate")]
    [ProducesResponseType(typeof(Result<CalculateFormulaResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<Result<CalculateFormulaResponse>> Calculate([FromBody] CalculateFormulaRequest request, CancellationToken cancellationToken) => await Sender.Send(request, cancellationToken);
}