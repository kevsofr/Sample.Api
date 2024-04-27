using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sample.Api.Dtos;
using Sample.Api.Dtos.Requests;
using Sample.Api.Dtos.Responses;
using Sample.Api.SwaggerExamples;
using Sample.Api.SwaggerExamples.Requests;
using Sample.Api.SwaggerExamples.Responses;
using Sample.Domain.Interfaces.Services;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Sample.Api.Controllers;

[Authorize("SampleApiPolicy")]
[Route("api/values")]
[ApiController]
public class ValueController(IValueService valueService, ILogger<ValueController> logger) : ControllerBase
{
    /// <summary>
    /// Get values
    /// </summary>
    /// <returns>List of values</returns>
    [HttpGet]
    [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ValuesResponse))]
    [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ValuesResponseExample))]
    [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetValuesAsync()
    {
        logger.Log(LogLevel.Information, "Get values called");

        var values = await valueService.GetValuesAsync();
        return Ok(new ValuesResponse(values));
    }

    /// <summary>
    /// Get value by id
    /// </summary>
    /// <param name="id">Value id</param>
    /// <returns>Value</returns>
    [HttpGet]
    [Route("{id}")]
    [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ValueDto))]
    [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ValueDtoExample))]
    [SwaggerResponse((int)HttpStatusCode.NotFound)]
    [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetValueByIdAsync([FromRoute] int id)
    {
        var value = await valueService.GetValueByIdAsync(id);

        if (value is null)
        {
            return NotFound();
        }

        return Ok(new ValueDto(value));
    }

    /// <summary>
    /// Create a new value
    /// </summary>
    /// <param name="request">Create request</param>
    /// <returns>Created value</returns>
    [HttpPost]
    [SwaggerRequestExample(typeof(CreateValueRequest), typeof(CreateValueRequestExample))]
    [SwaggerResponse((int)HttpStatusCode.Created, Type = typeof(ValueDto))]
    [SwaggerResponseExample((int)HttpStatusCode.Created, typeof(ValueDtoExample))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest)]
    [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> CreateValueAsync([FromBody] CreateValueRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var value = await valueService.CreateValueAsync(request.ToModel());
        return StatusCode((int)HttpStatusCode.Created, new ValueDto(value));
    }

    /// <summary>
    /// Update a value by id
    /// </summary>
    /// <param name="id">Value id</param>
    /// <param name="request">Update request</param>
    /// <returns>Updated value</returns>
    [HttpPut("{id}")]
    [SwaggerRequestExample(typeof(UpdateValueRequest), typeof(UpdateValueRequestExample))]
    [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ValueDto))]
    [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ValueDtoExample))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest)]
    [SwaggerResponse((int)HttpStatusCode.NotFound)]
    [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
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

    /// <summary>
    /// Delete a value by id
    /// </summary>
    /// <param name="id">Value id</param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [SwaggerResponse((int)HttpStatusCode.NoContent)]
    [SwaggerResponse((int)HttpStatusCode.NotFound)]
    [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
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