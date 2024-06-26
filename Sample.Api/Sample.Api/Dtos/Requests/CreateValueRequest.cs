﻿using System.ComponentModel.DataAnnotations;
using Sample.Domain.Models;

namespace Sample.Api.Dtos.Requests;

public class CreateValueRequest(int id, string name)
{
    [Range(1, 1_000)]
    public int Id { get; private set; } = id;

    [Required]
    public string Name { get; private set; } = name;

    public Value ToModel() => new(Id, Name);
}