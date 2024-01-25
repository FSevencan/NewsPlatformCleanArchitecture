using Application.Features.NewsVideos.Commands.Create;
using Application.Features.NewsVideos.Commands.Delete;
using Application.Features.NewsVideos.Commands.Update;
using Application.Features.NewsVideos.Queries.GetById;
using Application.Features.NewsVideos.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NewsVideosController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateNewsVideoCommand createNewsVideoCommand)
    {
        CreatedNewsVideoResponse response = await Mediator.Send(createNewsVideoCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateNewsVideoCommand updateNewsVideoCommand)
    {
        UpdatedNewsVideoResponse response = await Mediator.Send(updateNewsVideoCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedNewsVideoResponse response = await Mediator.Send(new DeleteNewsVideoCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdNewsVideoResponse response = await Mediator.Send(new GetByIdNewsVideoQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListNewsVideoQuery getListNewsVideoQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListNewsVideoListItemDto> response = await Mediator.Send(getListNewsVideoQuery);
        return Ok(response);
    }
}