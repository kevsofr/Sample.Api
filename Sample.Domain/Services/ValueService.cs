using Sample.Domain.Interfaces.Repositories;
using Sample.Domain.Interfaces.Services;
using Sample.Domain.Models;

namespace Sample.Domain.Services;

public class ValueService(IFakeRepository fakeRepository) : IValueService
{
    public async Task<IEnumerable<Value>> GetValuesAsync() =>
        await fakeRepository.GetValuesAsync();

    public async Task<Value?> GetValueByIdAsync(int id) =>
        await fakeRepository.GetValueByIdAsync(id);

    public async Task<Value> CreateValueAsync(Value value) =>
        await fakeRepository.CreateValueAsync(value);

    public async Task<Value?> UpdateValueAsync(Value value) =>
        await fakeRepository.UpdateValueAsync(value);

    public async Task<bool> DeleteValueAsync(int id) =>
        await fakeRepository.DeleteValueAsync(id);
}