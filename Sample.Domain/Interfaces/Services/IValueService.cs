using Sample.Domain.Dtos.Services;

namespace Sample.Domain.Interfaces.Services;

public interface IValueService
{
    Task<IEnumerable<ValueServiceDto>> GetValuesAsync();
    Task<ValueServiceDto?> GetValueByIdAsync(int id);
    Task<ValueServiceDto> CreateValueAsync(ValueServiceDto value);
    Task<ValueServiceDto?> UpdateValueAsync(ValueServiceDto value);
    Task<bool> DeleteValueAsync(int id);
}