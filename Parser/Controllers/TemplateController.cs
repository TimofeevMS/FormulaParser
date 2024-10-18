using Microsoft.AspNetCore.Mvc;
using Parser.Application.Common.Results;
using Parser.Application.UseCases.Template.Create;
using Parser.Application.UseCases.Template.Edit;
using Parser.Application.UseCases.Template.Get;
using Parser.Application.UseCases.Template.GetForDataSheet;
using Parser.Application.UseCases.Template.GetForMenu;
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
    
    [HttpGet("for-datasheet/{Id:guid}")]
    [ProducesResponseType(typeof(Result<List<GetForDataSheetResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> GetForDataSheet([FromRoute] GetForDataSheetRequest request, CancellationToken cancellationToken) => ToResponse(await Sender.Send(request, cancellationToken));
}