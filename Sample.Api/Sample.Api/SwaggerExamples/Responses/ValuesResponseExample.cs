using Sample.Api.Dtos;
using Sample.Api.Dtos.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace Sample.Api.SwaggerExamples.Responses;

public class ValuesResponseExample : IExamplesProvider<ValuesResponse>
{
    public ValuesResponse GetExamples() =>
        new(new[]
        {
            new ValueDto(1, "Fake 1"),
            new ValueDto(2, "Fake 2"),
            new ValueDto(3, "Fake 3")
        });
}
