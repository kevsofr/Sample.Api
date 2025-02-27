﻿using AutoFixture.Xunit2;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NFluent;
using NSubstitute;
using Sample.Api.Controllers;
using Sample.Api.Dtos.Requests;
using Sample.Domain.Dtos.Services;
using Sample.Domain.Interfaces.Services;

namespace Sample.Tests.Api.Controllers;

public class ValueControllerTests
{
    private readonly ValueController _valueController;
    private readonly IValueService _valueService = Substitute.For<IValueService>();
    private readonly ILogger<ValueController> _logger = Substitute.For<ILogger<ValueController>>();

    public ValueControllerTests() =>
        _valueController = new ValueController(_valueService, _logger);

    [Theory, AutoData]
    public async Task Should_Get_Values(IEnumerable<ValueServiceDto> values)
    {
        _valueService.GetValuesAsync().Returns(values);

        var response = await _valueController.GetValuesAsync();

        Check.That(response.Result).IsInstanceOf<OkObjectResult>();
    }

    [Theory, AutoData]
    public async Task Should_Get_Value_When_Value_Exists(int id, ValueServiceDto value)
    {
        _valueService.GetValueByIdAsync(id).Returns(value);

        var response = await _valueController.GetValueByIdAsync(id);

        Check.That(response.Result).IsInstanceOf<OkObjectResult>();
    }

    [Theory, AutoData]
    public async Task Should_Not_Get_Value_And_Return_404_When_Value_Doesnt_Exist(int id)
    {
        var response = await _valueController.GetValueByIdAsync(id);

        Check.That(response.Result).IsInstanceOf<NotFoundResult>();
    }

    [Theory, AutoData]
    public async Task Should_Create_Value(CreateValueRequest request, ValueServiceDto value)
    {
        _valueService.CreateValueAsync(default!).ReturnsForAnyArgs(value);

        var response = await _valueController.CreateValueAsync(request);

        Check.That(response.Result).IsInstanceOf<CreatedAtActionResult>();
    }

    [Fact]
    public async Task Should_Not_Create_Value_And_Return_400_When_Bad_Request()
    {
        var request = new CreateValueRequest(1, string.Empty);
        _valueController.ModelState.AddModelError(string.Empty, string.Empty);

        var response = await _valueController.CreateValueAsync(request);

        Check.That(response.Result).IsInstanceOf<BadRequestResult>();
    }

    [Theory, AutoData]
    public async Task Should_Update_Value(int id, UpdateValueRequest request, ValueServiceDto value)
    {
        _valueService.UpdateValueAsync(default!).ReturnsForAnyArgs(value);

        var response = await _valueController.UpdateValueAsync(id, request);

        Check.That(response.Result).IsInstanceOf<OkObjectResult>();
    }

    [Theory, AutoData]
    public async Task Should_Not_Update_Value_And_Return_400_When_Bad_Request(int id)
    {
        var request = new UpdateValueRequest(string.Empty);
        _valueController.ModelState.AddModelError(string.Empty, string.Empty);

        var response = await _valueController.UpdateValueAsync(id, request);

        Check.That(response.Result).IsInstanceOf<BadRequestResult>();
    }

    [Theory, AutoData]
    public async Task Should_Not_Update_Value_And_Return_404_When_Value_Doesnt_Exist(int id, UpdateValueRequest request)
    {
        var response = await _valueController.UpdateValueAsync(id, request);

        Check.That(response.Result).IsInstanceOf<NotFoundResult>();
    }

    [Theory, AutoData]
    public async Task Should_Delete_Value_When_Value_Exists(int id)
    {
        _valueService.DeleteValueAsync(id).Returns(true);

        var response = await _valueController.DeleteValueAsync(id);

        Check.That(response).IsInstanceOf<NoContentResult>();
    }

    [Theory, AutoData]
    public async Task Should_Not_Deletet_Value_And_Return_404_When_Value_Doesnt_Exist(int id)
    {
        var response = await _valueController.DeleteValueAsync(id);

        Check.That(response).IsInstanceOf<NotFoundResult>();
    }
}