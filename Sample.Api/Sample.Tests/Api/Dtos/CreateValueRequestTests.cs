using NFluent;
using Sample.Api.Dtos.Requests;

namespace Sample.Tests.Api.Dtos;

public class CreateValueRequestTests
{
    [Fact]
    public void Should_Valid_Request()
    {
        var request = CreateValidRequest();

        var result = request.IsValid();

        Check.That(result).IsTrue();
    }

    [Fact]
    public void Should_Not_Valid_Request_When_No_Id()
    {
        var validRequest = CreateValidRequest();
        var request = validRequest with { Id = 0 };

        var result = request.IsValid();

        Check.That(result).IsFalse();
    }

    [Fact]
    public void Should_Not_Valid_Request_When_No_Name()
    {
        var validRequest = CreateValidRequest();
        var request = validRequest with { Name = string.Empty };

        var result = request.IsValid();

        Check.That(result).IsFalse();
    }

    private static CreateValueRequest CreateValidRequest() => new(1, "Fake 1");
}
