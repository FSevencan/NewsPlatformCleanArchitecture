using Application.Features.Categories.Commands.Create;
using Application.Features.Categories.Commands.Delete;
using Application.Features.Categories.Commands.Update;
using Application.Features.Categories.Queries.GetArticlesByCategory;
using Application.Features.Categories.Queries.GetById;
using Application.Features.Categories.Queries.GetCategoryTree;
using Application.Features.Categories.Queries.GetList;
using Application.Features.Tags.Queries.GetArticlesByTag;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateCategoryCommand createCategoryCommand)
    {
        CreatedCategoryResponse response = await Mediator.Send(createCategoryCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateCategoryCommand updateCategoryCommand)
    {
        UpdatedCategoryResponse response = await Mediator.Send(updateCategoryCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedCategoryResponse response = await Mediator.Send(new DeleteCategoryCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdCategoryResponse response = await Mediator.Send(new GetByIdCategoryQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListCategoryQuery getListCategoryQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListCategoryListItemDto> response = await Mediator.Send(getListCategoryQuery);
        return Ok(response);
    }

    [HttpGet("{categoryName}/articles")]
    public async Task<IActionResult> GetArticlesByCategory(string categoryName, [FromQuery] PageRequest pageRequest)
    {
        var getArticlesByCategoryQuery = new GetArticlesByCategoryQuery
        {
            CategoryName = categoryName,
            PageRequest = pageRequest
        };
        GetListResponse<GetArticleByCategoryListDto> response = await Mediator.Send(getArticlesByCategoryQuery);
        return Ok(response);
    }

    [HttpGet("tree")]
    public async Task<IActionResult> GetCategoriesTree([FromQuery] PageRequest pageRequest)
    {
        GetCategoryTreeQuery getCategoryTreeQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetCategoryDto> response = await Mediator.Send(getCategoryTreeQuery);
        return Ok(response);
    }
}