using AutoFixture.Xunit2;
using Microsoft.AspNetCore.Mvc;
using NFluent;
using NSubstitute;
using Sample.Api.Controllers;
using Sample.Api.Dtos.Requests;
using Sample.Domain.Interfaces.Services;
using Sample.Domain.Models;

namespace Sample.Tests.Api.Controllers;

public class ValueControllerTests
{
    private readonly ValueController _valueController;
    private readonly IValueService _valueService = Substitute.For<IValueService>();

    public ValueControllerTests() =>
        _valueController = new ValueController(_valueService);

    [Theory, AutoData]
    public async Task Should_Get_Values(IEnumerable<Value> values)
    {
        _valueService.GetValuesAsync().Returns(values);

        var response = await _valueController.GetValuesAsync();

        Check.That(response).IsInstanceOf<OkObjectResult>();
    }

    [Theory, AutoData]
    public async Task Should_Get_Value_When_Value_Exists(int id, Value value)
    {
        _valueService.GetValueByIdAsync(id).Returns(value);

        var response = await _valueController.GetValueByIdAsync(id);

        Check.That(response).IsInstanceOf<OkObjectResult>();
    }

    [Theory, AutoData]
    public async Task Should_Not_Get_Value_And_Return_404_When_Value_Doesnt_Exist(int id)
    {
        var response = await _valueController.GetValueByIdAsync(id);

        Check.That(response).IsInstanceOf<NotFoundResult>();
    }

    [Theory, AutoData]
    public async Task Should_Create_Value(CreateValueRequest request, Value value)
    {
        _valueService.CreateValueAsync(default!).ReturnsForAnyArgs(value);

        var response = await _valueController.CreateValueAsync(request);

        Check.That(response).IsInstanceOf<ObjectResult>();
        Check.That(((ObjectResult)response).StatusCode).IsEqualTo(201);
    }

    [Fact]
    public async Task Should_Not_Create_Value_And_Return_400_When_Bad_Request()
    {
        var request = new CreateValueRequest(1, string.Empty);

        var response = await _valueController.CreateValueAsync(request);

        Check.That(response).IsInstanceOf<BadRequestResult>();
    }

    [Theory, AutoData]
    public async Task Should_Update_Value(int id, UpdateValueRequest request, Value value)
    {
        _valueService.UpdateValueAsync(default!).ReturnsForAnyArgs(value);

        var response = await _valueController.UpdateValueAsync(id, request);

        Check.That(response).IsInstanceOf<OkObjectResult>();
    }

    [Theory, AutoData]
    public async Task Should_Not_Update_Value_And_Return_400_When_Bad_Request(int id)
    {
        var request = new UpdateValueRequest(string.Empty);

        var response = await _valueController.UpdateValueAsync(id, request);

        Check.That(response).IsInstanceOf<BadRequestResult>();
    }

    [Theory, AutoData]
    public async Task Should_Not_Update_Value_And_Return_404_When_Value_Doesnt_Exist(int id, UpdateValueRequest request)
    {
        var response = await _valueController.UpdateValueAsync(id, request);

        Check.That(response).IsInstanceOf<NotFoundResult>();
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
