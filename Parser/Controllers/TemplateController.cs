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
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> Create([FromBody] CreateTemplateRequest request, CancellationToken cancellationToken) => ToResponse(await Sender.Send(request, cancellationToken));

    [HttpGet("{Id:guid}")]
    [ProducesResponseType(typeof(Result<GetTemplateResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> Get([FromRoute] GetTemplateRequest request, CancellationToken cancellationToken) => ToResponse(await Sender.Send(request, cancellationToken));
    
    [HttpPut("{Id:guid}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> Edit(EditTemplateRequest request, CancellationToken cancellationToken) => ToResponse(await Sender.Send(request, cancellationToken));
    
    [HttpGet("for-menu")]
    [ProducesResponseType(typeof(Result<List<GetForMenuTemplateResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> GetForMenu(CancellationToken cancellationToken) => ToResponse(await Sender.Send(new GetForMenuTemplateRequest(), cancellationToken));
    
    [HttpDelete("{Id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> Delete([FromRoute] DeleteTemplateRequest request, CancellationToken cancellationToken) => ToResponse(await Sender.Send(request, cancellationToken));
}