using Application.Features.Advertisements.Commands.Create;
using Application.Features.Advertisements.Commands.Delete;
using Application.Features.Advertisements.Commands.Update;
using Application.Features.Advertisements.Queries.GetById;
using Application.Features.Advertisements.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AdvertisementsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateAdvertisementCommand createAdvertisementCommand)
    {
        CreatedAdvertisementResponse response = await Mediator.Send(createAdvertisementCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateAdvertisementCommand updateAdvertisementCommand)
    {
        UpdatedAdvertisementResponse response = await Mediator.Send(updateAdvertisementCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedAdvertisementResponse response = await Mediator.Send(new DeleteAdvertisementCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdAdvertisementResponse response = await Mediator.Send(new GetByIdAdvertisementQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListAdvertisementQuery getListAdvertisementQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListAdvertisementListItemDto> response = await Mediator.Send(getListAdvertisementQuery);
        return Ok(response);
    }
}