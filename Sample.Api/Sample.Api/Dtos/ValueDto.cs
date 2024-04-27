using Sample.Domain.Models;

namespace Sample.Api.Dtos;

public record ValueDto(int Id, string Name)
{
    public ValueDto(Value value) : this(value.Id, value.Name) { }
}