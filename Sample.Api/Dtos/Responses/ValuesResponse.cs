using Sample.Domain.Models;

namespace Sample.Api.Dtos.Responses;

public record ValuesResponse(IEnumerable<ValueDto> Values)
{
    public ValuesResponse(IEnumerable<Value> values) : this(values.Select(v => new ValueDto(v))) { }
}