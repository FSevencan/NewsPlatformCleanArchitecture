using Application.Features.Columnists.Commands.Create;
using Application.Features.Columnists.Commands.Delete;
using Application.Features.Columnists.Commands.Update;
using Application.Features.Columnists.Queries.GetById;
using Application.Features.Columnists.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ColumnistsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateColumnistCommand createColumnistCommand)
    {
        CreatedColumnistResponse response = await Mediator.Send(createColumnistCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateColumnistCommand updateColumnistCommand)
    {
        UpdatedColumnistResponse response = await Mediator.Send(updateColumnistCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedColumnistResponse response = await Mediator.Send(new DeleteColumnistCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdColumnistResponse response = await Mediator.Send(new GetByIdColumnistQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListColumnistQuery getListColumnistQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListColumnistListItemDto> response = await Mediator.Send(getListColumnistQuery);
        return Ok(response);
    }
}