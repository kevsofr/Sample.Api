using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sample.Api.Dtos;
using Sample.Api.Dtos.Requests;
using Sample.Api.Dtos.Responses;
using Sample.Domain.Interfaces.Services;

namespace Sample.Api.Controllers;

[Authorize("SampleApiPolicy")]
[Route("api/values")]
[ApiController]
public class ValueController(IValueService valueService, ILogger<ValueController> logger) : ControllerBase
{
    [HttpGet]
    [EndpointDescription("Get values")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ValuesResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetValuesAsync()
    {
        logger.Log(LogLevel.Information, "Get values called");

        var values = await valueService.GetValuesAsync();
        return Ok(new ValuesResponse(values));
    }

    [HttpGet]
    [Route("{id}")]
    [EndpointDescription("Get value by id")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ValueDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetValueByIdAsync([FromRoute] int id)
    {
        var value = await valueService.GetValueByIdAsync(id);

        if (value is null)
        {
            return NotFound();
        }

        return Ok(new ValueDto(value));
    }

    [HttpPost]
    [EndpointDescription("Create a new value")]
    [Consumes(typeof(CreateValueRequest), "application/json")]
    [ProducesResponseType(typeof(ValueDto), StatusCodes.Status201Created, "application/json")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateValueAsync([FromBody] CreateValueRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var value = await valueService.CreateValueAsync(request.ToModel());
        return CreatedAtAction(nameof(GetValueByIdAsync), new { value.Id }, value);
    }

    [HttpPut("{id}")]
    [EndpointDescription("Update a value by id")]
    [Consumes(typeof(UpdateValueRequest), "application/json")]
    [ProducesResponseType(typeof(ValueDto), StatusCodes.Status200OK, "application/json")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateValueAsync([FromRoute] int id, [FromBody] UpdateValueRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var value = await valueService.UpdateValueAsync(request.ToModel(id));

        if (value is null)
        {
            return NotFound();
        }

        return Ok(new ValueDto(value));
    }

    [HttpDelete("{id}")]
    [EndpointDescription("Delete a value by id")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteValueAsync([FromRoute] int id)
    {
        var isDeleted = await valueService.DeleteValueAsync(id);

        if (!isDeleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}