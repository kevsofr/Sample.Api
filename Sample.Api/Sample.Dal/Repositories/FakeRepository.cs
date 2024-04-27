using Sample.Dal.Entities;
using Sample.Domain.Interfaces.Repositories;
using Sample.Domain.Models;

namespace Sample.Dal.Repositories;

public class FakeRepository : IFakeRepository
{
    private static readonly IList<ValueEntity> Values =
    [
        new ValueEntity
        {
            Id = 100,
            Name = "Fake 100"
        },
        new ValueEntity
        {
            Id = 101,
            Name = "Fake 101"
        }
    ];

    public async Task<Value?> GetValueByIdAsync(int id)
    {
        var entity = await Task.FromResult(Values.SingleOrDefault(v => v.Id == id));

        if (entity is null)
        {
            return null;
        }

        return new Value(entity.Id, entity.Name);
    }

    public async Task<IEnumerable<Value>> GetValuesAsync()
    {
        var values = await Task.FromResult(Values);

        return values.Select(v => new Value(v.Id, v.Name));
    }

    public async Task<Value> CreateValueAsync(Value value)
    {
        await Task.Run(() => Values.Add(new ValueEntity
        {
            Id = value.Id,
            Name = value.Name
        }));

        return value;
    }

    public async Task<Value?> UpdateValueAsync(Value value)
    {
        var entity = await Task.Run(() =>
        {
            var entity = Values.SingleOrDefault(v => v.Id == value.Id);

            if (entity is not null)
            {
                entity.Name = value.Name;
            }

            return entity;
        });

        if (entity is null)
        {
            return null;
        }

        return new Value(value.Id, value.Name);
    }

    public async Task<bool> DeleteValueAsync(int id) =>
        await Task.Run(() =>
        {
            var entity = Values.SingleOrDefault(v => v.Id == id);

            if (entity is null)
            {
                return false;
            }

            Values.Remove(entity);
            return true;
        });
}