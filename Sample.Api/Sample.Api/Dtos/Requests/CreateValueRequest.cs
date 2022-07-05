using Sample.Domain.Models;

namespace Sample.Api.Dtos.Requests;

public record CreateValueRequest(int Id, string Name)
{
    public bool IsValid()
    {
        if (Id == 0)
        {
            return false;
        }

        if (string.IsNullOrEmpty(Name))
        {
            return false;
        }

        return true;
    }

    public Value ToModel() => new()
    {
        Id = Id,
        Name = Name
    };
}
