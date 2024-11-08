﻿using Microsoft.AspNetCore.Mvc;
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
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> Create([FromBody] CreateDataSheetRequest request, CancellationToken cancellationToken) => ToResponse(await Sender.Send(request, cancellationToken));
    
    [HttpGet("{Id:guid}")]
    [ProducesResponseType(typeof(Result<GetDataSheetResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> Get([FromRoute] GetDataSheetRequest request, CancellationToken cancellationToken) => ToResponse(await Sender.Send(request, cancellationToken));
    
    [HttpGet("for-menu")]
    [ProducesResponseType(typeof(Result<List<GetForMenuDataSheetResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> GetForMenu(CancellationToken cancellationToken) => ToResponse(await Sender.Send(new GetForMenuDataSheetRequest(), cancellationToken));
    
    [HttpPut("{Id:guid}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> Edit(EditDataSheetRequest request, CancellationToken cancellationToken) => ToResponse(await Sender.Send(request, cancellationToken));
    
    [HttpGet("templates/{Id:guid}")]
    [ProducesResponseType(typeof(Result<List<GetTemplatesResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> GetTemplates([FromRoute] GetTemplatesRequest request, CancellationToken cancellationToken) => ToResponse(await Sender.Send(request, cancellationToken));
    
    [HttpPost("calculate")]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> Calculate([FromBody] CalculateFormulaRequest request, CancellationToken cancellationToken) => ToResponse(await Sender.Send(request, cancellationToken));
}