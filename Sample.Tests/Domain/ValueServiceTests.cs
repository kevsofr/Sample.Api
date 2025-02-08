using AutoFixture.Xunit2;
using NFluent;
using NSubstitute;
using Sample.Domain.Dtos.Repositories;
using Sample.Domain.Dtos.Services;
using Sample.Domain.Interfaces.Repositories;
using Sample.Domain.Services;

namespace Sample.Tests.Domain;

public class ValueServiceTests
{
    private readonly ValueService _valueService;
    private readonly IFakeRepository _fakeRepository = Substitute.For<IFakeRepository>();

    public ValueServiceTests() =>
        _valueService = new ValueService(_fakeRepository);

    [Theory, AutoData]
    public async Task Shoud_Get_Values(IEnumerable<ValueRepositoryDto> values)
    {
        _fakeRepository.GetValuesAsync().Returns(values);

        var result = await _valueService.GetValuesAsync();

        Check.That(result).IsNotNull();
    }

    [Theory, AutoData]
    public async Task Shoud_Get_Value_When_Value_Exists(int id, ValueRepositoryDto value)
    {
        _fakeRepository.GetValueByIdAsync(id).Returns(value);

        var result = await _valueService.GetValueByIdAsync(id);

        Check.That(result).IsNotNull();
    }

    [Theory, AutoData]
    public async Task Shoud_Not_Get_Value_And_Return_Null_When_Value_Doesnt_Exist(int id)
    {
        _fakeRepository.GetValueByIdAsync(id).Returns((ValueRepositoryDto?)null);

        var result = await _valueService.GetValueByIdAsync(id);

        Check.That(result).IsNull();
    }

    [Theory, AutoData]
    public async Task Shoud_Create_Value(ValueServiceDto valueServiceDto, ValueRepositoryDto valueRepositoryDto)
    {
        _fakeRepository.CreateValueAsync(default!).ReturnsForAnyArgs(valueRepositoryDto);

        var result = await _valueService.CreateValueAsync(valueServiceDto);

        Check.That(result).IsNotNull();
    }

    [Theory, AutoData]
    public async Task Shoud_Update_Value_When_Value_Exists(ValueServiceDto valueServiceDto, ValueRepositoryDto valueRepositoryDto)
    {
        _fakeRepository.UpdateValueAsync(default!).ReturnsForAnyArgs(valueRepositoryDto);

        var result = await _valueService.UpdateValueAsync(valueServiceDto);

        Check.That(result).IsNotNull();
    }

    [Theory, AutoData]
    public async Task Shoud_Not_Update_Value_And_Return_Value_When_Value_Doesnt_Exist(ValueServiceDto value)
    {
        _fakeRepository.UpdateValueAsync(default!).ReturnsForAnyArgs((ValueRepositoryDto?)null);

        var result = await _valueService.UpdateValueAsync(value);

        Check.That(result).IsNull();
    }

    [Theory, AutoData]
    public async Task Shoud_Delete_Value(int id)
    {
        _fakeRepository.DeleteValueAsync(id).Returns(true);

        var result = await _valueService.DeleteValueAsync(id);

        Check.That(result).IsTrue();
    }
}