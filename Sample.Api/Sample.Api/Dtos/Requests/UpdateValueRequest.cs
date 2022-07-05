using Sample.Domain.Models;

namespace Sample.Api.Dtos.Requests;

public record UpdateValueRequest(string Name)
{
    public bool IsValid() => !string.IsNullOrEmpty(Name);

    public Value ToModel(int id) => new()
    {
        Id = id,
        Name = Name
    };
}
