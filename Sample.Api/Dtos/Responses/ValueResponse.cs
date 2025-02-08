using Sample.Domain.Dtos.Services;

namespace Sample.Api.Dtos.Responses;

public record ValueResponse(int Id, string Name)
{
    public ValueResponse(ValueServiceDto value) : this(value.Id, value.Name) { }
}