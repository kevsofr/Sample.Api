using System.ComponentModel.DataAnnotations;
using Sample.Domain.Models;

namespace Sample.Api.Dtos.Requests;

public class UpdateValueRequest(string name)
{
    [Required]
    public string Name { get; set; } = name;

    public Value ToModel(int id) => new(id, Name);
}