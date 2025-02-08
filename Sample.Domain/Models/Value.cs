using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Sample.Tests")]

namespace Sample.Domain.Models;

internal record Value(int Id, string Name);