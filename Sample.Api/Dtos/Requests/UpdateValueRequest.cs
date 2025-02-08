using System.ComponentModel.DataAnnotations;
using Sample.Domain.Dtos.Services;

namespace Sample.Api.Dtos.Requests;

public class UpdateValueRequest(string name)
{
    [Required]
    public string Name { get; set; } = name;

    public ValueServiceDto ToModel(int id) => new(id, Name);
}