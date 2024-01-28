using Application.Features.Polls.Commands.Create;
using Application.Features.Polls.Commands.Delete;
using Application.Features.Polls.Commands.Update;
using Application.Features.Polls.Queries.GetById;
using Application.Features.Polls.Queries.GetLatestPoll;
using Application.Features.Polls.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PollsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreatePollCommand createPollCommand)
    {
        CreatedPollResponse response = await Mediator.Send(createPollCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdatePollCommand updatePollCommand)
    {
        UpdatedPollResponse response = await Mediator.Send(updatePollCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedPollResponse response = await Mediator.Send(new DeletePollCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdPollResponse response = await Mediator.Send(new GetByIdPollQuery { Id = id });
        return Ok(response);
    }

    [HttpGet("latest")]
    public async Task<IActionResult> GetLatest()
    {
        var response = await Mediator.Send(new GetLatestPollQuery());
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListPollQuery getListPollQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListPollListItemDto> response = await Mediator.Send(getListPollQuery);
        return Ok(response);
    }
}