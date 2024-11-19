using Microsoft.AspNetCore.Mvc;
using Parser.Application.Common.Results;
using Parser.Application.UseCases.Templates.Create;
using Parser.Application.UseCases.Templates.Delete;
using Parser.Application.UseCases.Templates.Edit;
using Parser.Application.UseCases.Templates.Get;
using Parser.Application.UseCases.Templates.GetForMenu;
using Parser.Controllers.Common;

namespace Parser.Controllers;

[Route("api/[controller]")]
public class TemplateController : ApiController
{
    [HttpPost]
    [ProducesResponseType(typeof(Result<Guid>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<Result<Guid>> Create([FromBody] CreateTemplateRequest request, CancellationToken cancellationToken) => await Sender.Send(request, cancellationToken);

    [HttpGet("{Id:guid}")]
    [ProducesResponseType(typeof(Result<GetTemplateResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<Result<GetTemplateResponse>> Get([FromRoute] GetTemplateRequest request, CancellationToken cancellationToken) => await Sender.Send(request, cancellationToken);
    
    [HttpPut("{Id:guid}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<Result> Edit([FromBody] EditTemplateRequest request, CancellationToken cancellationToken) => await Sender.Send(request, cancellationToken);
    
    [HttpGet("for-menu")]
    [ProducesResponseType(typeof(Result<IEnumerable<GetForMenuTemplateResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<Result<IEnumerable<GetForMenuTemplateResponse>>> GetForMenu(CancellationToken cancellationToken) => await Sender.Send(new GetForMenuTemplateRequest(), cancellationToken);
    
    [HttpDelete("{Id:guid}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<Result> Delete([FromRoute] DeleteTemplateRequest request, CancellationToken cancellationToken) => await Sender.Send(request, cancellationToken);
}