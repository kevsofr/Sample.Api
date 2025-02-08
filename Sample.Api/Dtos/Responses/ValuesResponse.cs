using Sample.Domain.Dtos.Services;

namespace Sample.Api.Dtos.Responses;

public record ValuesResponse(IEnumerable<ValueResponse> Values)
{
    public ValuesResponse(IEnumerable<ValueServiceDto> values) : this(values.Select(v => new ValueResponse(v))) { }
}