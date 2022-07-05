﻿using Sample.Domain.Models;

namespace Sample.Domain.Interfaces.Services;

public interface IValueService
{
    Task<IEnumerable<Value>> GetValuesAsync();
    Task<Value?> GetValueByIdAsync(int id);
    Task<Value> CreateValueAsync(Value value);
    Task<Value?> UpdateValueAsync(Value value);
    Task<bool> DeleteValueAsync(int id);
}
