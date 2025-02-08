using System.ComponentModel.DataAnnotations;
using Sample.Domain.Dtos.Services;

namespace Sample.Api.Dtos.Requests;

public class CreateValueRequest(int id, string name)
{
    [Range(1, 1_000)]
    public int Id { get; set; } = id;

    [Required]
    public string Name { get; set; } = name;

    public ValueServiceDto ToModel() => new(Id, Name);
}