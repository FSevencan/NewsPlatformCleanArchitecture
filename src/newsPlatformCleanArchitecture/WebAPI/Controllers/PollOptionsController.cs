using Application.Features.PollOptions.Commands.Create;
using Application.Features.PollOptions.Commands.Delete;
using Application.Features.PollOptions.Commands.Update;
using Application.Features.PollOptions.Queries.GetById;
using Application.Features.PollOptions.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PollOptionsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreatePollOptionCommand createPollOptionCommand)
    {
        CreatedPollOptionResponse response = await Mediator.Send(createPollOptionCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdatePollOptionCommand updatePollOptionCommand)
    {
        UpdatedPollOptionResponse response = await Mediator.Send(updatePollOptionCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedPollOptionResponse response = await Mediator.Send(new DeletePollOptionCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("by-poll/{pollId}")]
    public async Task<IActionResult> GetByPollId([FromRoute] Guid pollId)
    {
        var response = await Mediator.Send(new GetByIdPollOptionQuery { PollId = pollId });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListPollOptionQuery getListPollOptionQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListPollOptionListItemDto> response = await Mediator.Send(getListPollOptionQuery);
        return Ok(response);
    }
}