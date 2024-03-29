using Application.Features.ColumnArticles.Commands.Create;
using Application.Features.ColumnArticles.Commands.Delete;
using Application.Features.ColumnArticles.Commands.Update;
using Application.Features.ColumnArticles.Queries.GetById;
using Application.Features.ColumnArticles.Queries.GetBySlug;
using Application.Features.ColumnArticles.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ColumnArticlesController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateColumnArticleCommand createColumnArticleCommand)
    {
        CreatedColumnArticleResponse response = await Mediator.Send(createColumnArticleCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateColumnArticleCommand updateColumnArticleCommand)
    {
        UpdatedColumnArticleResponse response = await Mediator.Send(updateColumnArticleCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedColumnArticleResponse response = await Mediator.Send(new DeleteColumnArticleCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("id/{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdColumnArticleResponse response = await Mediator.Send(new GetByIdColumnArticleQuery { Id = id });
        return Ok(response);
    }

    [HttpGet("slug/{slug}")]
    public async Task<IActionResult> GetBySlug([FromRoute] string slug)
    {
        GetColumnArticleBySlugResponse response = await Mediator.Send(new GetColumnArticleBySlugQuery { Slug = slug });
        return Ok(response);
    }


    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest, [FromQuery] Guid? categoryId)
    {
       
        GetListColumnArticleQuery query = new() { PageRequest = pageRequest, CategoryId = categoryId };
        GetListResponse<GetListColumnArticleListItemDto> response = await Mediator.Send(query);
        return Ok(response);
    }
}