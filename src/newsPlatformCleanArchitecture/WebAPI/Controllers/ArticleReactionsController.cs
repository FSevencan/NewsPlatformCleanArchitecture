using Application.Features.ArticleReactions.Commands.Create;
using Application.Features.ArticleReactions.Commands.Delete;
using Application.Features.ArticleReactions.Commands.Update;
using Application.Features.ArticleReactions.Queries.GetById;
using Application.Features.ArticleReactions.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;
using Nest;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ArticleReactionsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateArticleReactionCommand createArticleReactionCommand)
    {
        CreatedArticleReactionResponse response = await Mediator.Send(createArticleReactionCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateArticleReactionCommand updateArticleReactionCommand)
    {
        UpdatedArticleReactionResponse response = await Mediator.Send(updateArticleReactionCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedArticleReactionResponse response = await Mediator.Send(new DeleteArticleReactionCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdArticleReactionResponse response = await Mediator.Send(new GetByIdArticleReactionQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListArticleReactionQuery getListArticleReactionQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListArticleReactionListItemDto> response = await Mediator.Send(getListArticleReactionQuery);
        return Ok(response);
    }

    [HttpGet("GetByArticleAndVoter")]
    public async Task<IActionResult> GetByArticleAndVoter([FromQuery] GetByArticleAndVoterQuery query)
    {
        GetByArticleAndVoterResponse response = await Mediator.Send(new GetByArticleAndVoterQuery { ArticleId = query.ArticleId , VoterIdentifier = query.VoterIdentifier });
        return Ok(response);
    }
}