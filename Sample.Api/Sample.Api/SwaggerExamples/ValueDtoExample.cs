using Sample.Api.Dtos;
using Swashbuckle.AspNetCore.Filters;

namespace Sample.Api.SwaggerExamples;

public class ValueDtoExample : IExamplesProvider<ValueDto>
{
    public ValueDto GetExamples() =>
        new(1, "Fake 1");
}
