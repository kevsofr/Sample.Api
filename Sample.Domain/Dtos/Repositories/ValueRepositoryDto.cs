using Sample.Domain.Dtos.Services;

namespace Sample.Domain.Dtos.Repositories;

public record ValueRepositoryDto(int Id, string Name)
{
    public ValueRepositoryDto(ValueServiceDto value) : this(value.Id, value.Name) { }
}