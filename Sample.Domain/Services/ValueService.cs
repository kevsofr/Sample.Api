using Sample.Domain.Dtos.Repositories;
using Sample.Domain.Dtos.Services;
using Sample.Domain.Interfaces.Repositories;
using Sample.Domain.Interfaces.Services;
using Sample.Domain.Models;

namespace Sample.Domain.Services;

public class ValueService(IFakeRepository fakeRepository) : IValueService
{
    public async Task<IEnumerable<ValueServiceDto>> GetValuesAsync()
    {
        var valuesDtos = await fakeRepository.GetValuesAsync();
        var values = valuesDtos.Select(v => new Value(v.Id, v.Name));
        return values.Select(v => new ValueServiceDto(v));
    }

    public async Task<ValueServiceDto?> GetValueByIdAsync(int id)
    {
        var valueDto = await fakeRepository.GetValueByIdAsync(id);

        if (valueDto is null)
        {
            return null;
        }
        
        var value = new Value(valueDto.Id, valueDto.Name);
        return new ValueServiceDto(value);
    }

    public async Task<ValueServiceDto> CreateValueAsync(ValueServiceDto valueServiceDto)
    {
        var valueRepositoryDto = new ValueRepositoryDto(valueServiceDto);
        var valueDto = await fakeRepository.CreateValueAsync(valueRepositoryDto);
        var value = new Value(valueDto.Id, valueDto.Name);
        return new ValueServiceDto(value);
    }

    public async Task<ValueServiceDto?> UpdateValueAsync(ValueServiceDto valueServiceDto)
    {
        var valueRepositoryDto = new ValueRepositoryDto(valueServiceDto);
        var valueDto = await fakeRepository.UpdateValueAsync(valueRepositoryDto);
        
        if (valueDto is null)
        {
            return null;
        }

        var value = new Value(valueDto.Id, valueDto.Name);
        return new ValueServiceDto(value);
    }

    public async Task<bool> DeleteValueAsync(int id) =>
        await fakeRepository.DeleteValueAsync(id);
}