using Sample.Api.Dtos.Requests;
using Swashbuckle.AspNetCore.Filters;

namespace Sample.Api.SwaggerExamples.Requests;

public class CreateValueRequestExample : IExamplesProvider<CreateValueRequest>
{
    public CreateValueRequest GetExamples() =>
        new(103, "Fake 103");
}