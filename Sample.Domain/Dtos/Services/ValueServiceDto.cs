using Sample.Domain.Models;

namespace Sample.Domain.Dtos.Services;

public record ValueServiceDto(int Id, string Name)
{
    public ValueServiceDto(Value value) : this(value.Id, value.Name) { }
}