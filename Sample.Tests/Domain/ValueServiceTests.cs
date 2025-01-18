using AutoFixture.Xunit2;
using NFluent;
using NSubstitute;
using Sample.Domain.Interfaces.Repositories;
using Sample.Domain.Interfaces.Services;
using Sample.Domain.Models;
using Sample.Domain.Services;

namespace Sample.Tests.Domain;

public class ValueServiceTests
{
    private readonly IValueService _valueService;
    private readonly IFakeRepository _fakeRepository = Substitute.For<IFakeRepository>();

    public ValueServiceTests() =>
        _valueService = new ValueService(_fakeRepository);

    [Theory, AutoData]
    public async Task Shoud_Get_Values(IEnumerable<Value> values)
    {
        _fakeRepository.GetValuesAsync().Returns(values);

        var result = await _valueService.GetValuesAsync();

        Check.That(result).IsNotNull();
    }

    [Theory, AutoData]
    public async Task Shoud_Get_Value(int id, Value value)
    {
        _fakeRepository.GetValueByIdAsync(id).Returns(value);

        var result = await _valueService.GetValueByIdAsync(id);

        Check.That(result).IsNotNull();
    }

    [Theory, AutoData]
    public async Task Shoud_Create_Value(Value value)
    {
        _fakeRepository.CreateValueAsync(value).Returns(value);

        var result = await _valueService.CreateValueAsync(value);

        Check.That(result).IsNotNull();
    }

    [Theory, AutoData]
    public async Task Shoud_Update_Value(Value value)
    {
        _fakeRepository.UpdateValueAsync(value).Returns(value);

        var result = await _valueService.UpdateValueAsync(value);

        Check.That(result).IsNotNull();
    }

    [Theory, AutoData]
    public async Task Shoud_Delete_Value(int id)
    {
        _fakeRepository.DeleteValueAsync(id).Returns(true);

        var result = await _valueService.DeleteValueAsync(id);

        Check.That(result).IsTrue();
    }
}