using Sample.Domain.Interfaces.Repositories;
using Sample.Domain.Interfaces.Services;
using Sample.Domain.Models;

namespace Sample.Domain.Services;

public class ValueService : IValueService
{
    private readonly IFakeRepository _fakeRepository;

    public ValueService(IFakeRepository fakeRepository) =>
        _fakeRepository = fakeRepository;

    public async Task<IEnumerable<Value>> GetValuesAsync() =>
        await _fakeRepository.GetValuesAsync();

    public async Task<Value?> GetValueByIdAsync(int id) =>
        await _fakeRepository.GetValueByIdAsync(id);

    public async Task<Value> CreateValueAsync(Value value) =>
        await _fakeRepository.CreateValueAsync(value);

    public async Task<Value?> UpdateValueAsync(Value value) =>
        await _fakeRepository.UpdateValueAsync(value);

    public async Task<bool> DeleteValueAsync(int id) =>
        await _fakeRepository.DeleteValueAsync(id);
}
