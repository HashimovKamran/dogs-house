using Application.Features.Dogs.Commands.Create;
using Application.Features.Dogs.Commands.Delete;
using Application.Features.Dogs.Commands.Update;
using Application.Features.Dogs.Queries.GetById;
using Application.Features.Dogs.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using NArchitecture.Core.Persistence.Dynamic;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[EnableRateLimiting("GlobalRateLimit")]
public class DogsController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<CreatedDogResponse>> Add([FromBody] CreateDogCommand command)
    {
        CreatedDogResponse response = await Mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { response.Id }, response);
    }

    [HttpPut]
    public async Task<ActionResult<UpdatedDogResponse>> Update([FromBody] UpdateDogCommand command)
    {
        UpdatedDogResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<DeletedDogResponse>> Delete([FromRoute] Guid id)
    {
        DeleteDogCommand command = new() { Id = id };

        DeletedDogResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetByIdDogResponse>> GetById([FromRoute] Guid id)
    {
        GetByIdDogQuery query = new() { Id = id };

        GetByIdDogResponse response = await Mediator.Send(query);

        return Ok(response);
    }

    [HttpPost("GetList")]
    public async Task<ActionResult<GetListDogQuery>> GetList([FromQuery] PageRequest pageRequest, [FromBody] DynamicQuery dynamicQuery)
    {
        GetListDogQuery query = new() { PageRequest = pageRequest, DynamicQuery = dynamicQuery };

        GetListResponse<GetListDogListItemDto> response = await Mediator.Send(query);

        return Ok(response);
    }
}