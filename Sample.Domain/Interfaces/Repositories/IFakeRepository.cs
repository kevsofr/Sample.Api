using Sample.Domain.Dtos.Repositories;

namespace Sample.Domain.Interfaces.Repositories;

public interface IFakeRepository
{
    Task<IEnumerable<ValueRepositoryDto>> GetValuesAsync();
    Task<ValueRepositoryDto?> GetValueByIdAsync(int id);
    Task<ValueRepositoryDto> CreateValueAsync(ValueRepositoryDto value);
    Task<ValueRepositoryDto?> UpdateValueAsync(ValueRepositoryDto value);
    Task<bool> DeleteValueAsync(int id);
}