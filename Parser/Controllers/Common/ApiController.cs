using MediatR;
using Microsoft.AspNetCore.Mvc;
using Parser.Application.Common.Results;

namespace Parser.Controllers.Common;

[ApiController]
public abstract class ApiController : ControllerBase
{
    protected ISender Sender => HttpContext.RequestServices.GetRequiredService<ISender>();
}