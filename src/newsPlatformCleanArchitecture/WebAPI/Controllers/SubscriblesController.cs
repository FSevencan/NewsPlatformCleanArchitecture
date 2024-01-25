using Application.Features.Subscribles.Commands.Create;
using Application.Features.Subscribles.Commands.Delete;
using Application.Features.Subscribles.Commands.Update;
using Application.Features.Subscribles.Queries.GetById;
using Application.Features.Subscribles.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SubscriblesController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateSubscribleCommand createSubscribleCommand)
    {
        CreatedSubscribleResponse response = await Mediator.Send(createSubscribleCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateSubscribleCommand updateSubscribleCommand)
    {
        UpdatedSubscribleResponse response = await Mediator.Send(updateSubscribleCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedSubscribleResponse response = await Mediator.Send(new DeleteSubscribleCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdSubscribleResponse response = await Mediator.Send(new GetByIdSubscribleQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListSubscribleQuery getListSubscribleQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListSubscribleListItemDto> response = await Mediator.Send(getListSubscribleQuery);
        return Ok(response);
    }
}