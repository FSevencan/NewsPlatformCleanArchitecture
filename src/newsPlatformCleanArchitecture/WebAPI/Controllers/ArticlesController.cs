using Application.Features.Articles.Commands.Create;
using Application.Features.Articles.Commands.Delete;
using Application.Features.Articles.Commands.Update;
using Application.Features.Articles.Queries.GetById;
using Application.Features.Articles.Queries.GetLatestArticlesByCategory;
using Application.Features.Articles.Queries.GetList;
using Application.Features.Articles.Queries.GetMixedLatestArticles;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ArticlesController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateArticleCommand createArticleCommand)
    {
        CreatedArticleResponse response = await Mediator.Send(createArticleCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateArticleCommand updateArticleCommand)
    {
        UpdatedArticleResponse response = await Mediator.Send(updateArticleCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedArticleResponse response = await Mediator.Send(new DeleteArticleCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{slug}")]
    public async Task<IActionResult> GetBySlug([FromRoute] string slug)
    {
        var response = await Mediator.Send(new GetArticleBySlugQuery { Slug = slug });
        return response != null ? Ok(response) : NotFound();
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListArticleQuery getListArticleQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListArticleListItemDto> response = await Mediator.Send(getListArticleQuery);
        return Ok(response);
    }

    [HttpGet("latest-by-category")]
    public async Task<IActionResult> GetLatestArticlesByCategory([FromQuery] string SubCategoryName, [FromQuery] int maxResults = 5)
    {
        var query = new GetLatestArticlesByCategoryQuery
        {
            SubCategoryName = SubCategoryName,
            MaxResult = maxResults
        };

        var response = await Mediator.Send(query);
        return Ok(response);
    }


    [HttpGet("mixed-latest-articles")]
    public async Task<IActionResult> GetMixedLatestArticles([FromQuery] int maxResults, [FromQuery] string[] excludeCategories)
    {
        var query = new GetMixedLatestArticlesQuery
        {
            MaxResult = maxResults,
            ExcludeCategories = excludeCategories
        };

        var response = await Mediator.Send(query);

         return Ok(response);
    }

}