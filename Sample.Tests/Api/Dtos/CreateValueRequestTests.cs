using System.ComponentModel.DataAnnotations;
using NFluent;
using Sample.Api.Dtos.Requests;

namespace Sample.Tests.Api.Dtos;

public class CreateValueRequestTests
{
    [Fact]
    public void Should_Valid_Request()
    {
        var request = CreateValidRequest();
        var validationContext = new ValidationContext(request);
        var errors = new List<ValidationResult>();

        var result = Validator.TryValidateObject(request, validationContext, errors, true);

        Check.That(result).IsTrue();
        Check.That(errors).HasSize(0);
    }

    [Fact]
    public void Should_Not_Valid_Request_When_No_Id()
    {
        var validRequest = CreateValidRequest();
        var request = new CreateValueRequest(0, validRequest.Name);
        var validationContext = new ValidationContext(request);
        var errors = new List<ValidationResult>();

        var result = Validator.TryValidateObject(request, validationContext, errors, true);

        Check.That(result).IsFalse();
        Check.That(errors).HasSize(1);
        Check.That(errors.Single().MemberNames).HasSize(1);
        Check.That(errors.Single().MemberNames.Single()).IsEqualTo(nameof(CreateValueRequest.Id));
    }

    [Fact]
    public void Should_Not_Valid_Request_When_Id_Greater_Than_1_000()
    {
        var validRequest = CreateValidRequest();
        var request = new CreateValueRequest(1_001, validRequest.Name);
        var validationContext = new ValidationContext(request);
        var errors = new List<ValidationResult>();

        var result = Validator.TryValidateObject(request, validationContext, errors, true);

        Check.That(result).IsFalse();
        Check.That(errors).HasSize(1);
        Check.That(errors.Single().MemberNames).HasSize(1);
        Check.That(errors.Single().MemberNames.Single()).IsEqualTo(nameof(CreateValueRequest.Id));
    }

    [Fact]
    public void Should_Not_Valid_Request_When_No_Name()
    {
        var validRequest = CreateValidRequest();
        var request = new CreateValueRequest(validRequest.Id, string.Empty);
        var validationContext = new ValidationContext(request);
        var errors = new List<ValidationResult>();

        var result = Validator.TryValidateObject(request, validationContext, errors, true);

        Check.That(result).IsFalse();
        Check.That(errors).HasSize(1);
        Check.That(errors.Single().MemberNames).HasSize(1);
        Check.That(errors.Single().MemberNames.Single()).IsEqualTo(nameof(CreateValueRequest.Name));
    }

    private static CreateValueRequest CreateValidRequest() => new(1, "Fake 1");
}