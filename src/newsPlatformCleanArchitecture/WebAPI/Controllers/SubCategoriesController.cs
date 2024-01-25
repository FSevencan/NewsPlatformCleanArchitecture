using Application.Features.SubCategories.Commands.Create;
using Application.Features.SubCategories.Commands.Delete;
using Application.Features.SubCategories.Commands.Update;
using Application.Features.SubCategories.Queries.GetById;
using Application.Features.SubCategories.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SubCategoriesController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateSubCategoryCommand createSubCategoryCommand)
    {
        CreatedSubCategoryResponse response = await Mediator.Send(createSubCategoryCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateSubCategoryCommand updateSubCategoryCommand)
    {
        UpdatedSubCategoryResponse response = await Mediator.Send(updateSubCategoryCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedSubCategoryResponse response = await Mediator.Send(new DeleteSubCategoryCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdSubCategoryResponse response = await Mediator.Send(new GetByIdSubCategoryQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListSubCategoryQuery getListSubCategoryQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListSubCategoryListItemDto> response = await Mediator.Send(getListSubCategoryQuery);
        return Ok(response);
    }
}