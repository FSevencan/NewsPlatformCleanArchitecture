using Application.Features.ArticleTags.Commands.Create;
using Application.Features.ArticleTags.Commands.Delete;
using Application.Features.ArticleTags.Commands.Update;
using Application.Features.ArticleTags.Queries.GetById;
using Application.Features.ArticleTags.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ArticleTagsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateArticleTagCommand createArticleTagCommand)
    {
        CreatedArticleTagResponse response = await Mediator.Send(createArticleTagCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateArticleTagCommand updateArticleTagCommand)
    {
        UpdatedArticleTagResponse response = await Mediator.Send(updateArticleTagCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedArticleTagResponse response = await Mediator.Send(new DeleteArticleTagCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdArticleTagResponse response = await Mediator.Send(new GetByIdArticleTagQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListArticleTagQuery getListArticleTagQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListArticleTagListItemDto> response = await Mediator.Send(getListArticleTagQuery);
        return Ok(response);
    }
}