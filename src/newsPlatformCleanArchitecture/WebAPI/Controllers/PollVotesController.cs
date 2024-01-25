using Application.Features.PollVotes.Commands.Create;
using Application.Features.PollVotes.Commands.Delete;
using Application.Features.PollVotes.Commands.Update;
using Application.Features.PollVotes.Queries.GetById;
using Application.Features.PollVotes.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PollVotesController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreatePollVoteCommand createPollVoteCommand)
    {
        CreatedPollVoteResponse response = await Mediator.Send(createPollVoteCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdatePollVoteCommand updatePollVoteCommand)
    {
        UpdatedPollVoteResponse response = await Mediator.Send(updatePollVoteCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedPollVoteResponse response = await Mediator.Send(new DeletePollVoteCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdPollVoteResponse response = await Mediator.Send(new GetByIdPollVoteQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListPollVoteQuery getListPollVoteQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListPollVoteListItemDto> response = await Mediator.Send(getListPollVoteQuery);
        return Ok(response);
    }
}