using Sample.Api.Dtos.Requests;
using Swashbuckle.AspNetCore.Filters;

namespace Sample.Api.SwaggerExamples.Requests;

public class UpdateValueRequestExample : IExamplesProvider<UpdateValueRequest>
{
    public UpdateValueRequest GetExamples() =>
        new("Fake 104");
}