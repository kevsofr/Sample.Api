using Sample.Domain.Models;

namespace Sample.Domain.Dtos.Services;

public record ValueServiceDto(int Id, string Name)
{
    internal ValueServiceDto(Value value) : this(value.Id, value.Name) { }
}