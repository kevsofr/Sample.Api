using NFluent;
using Sample.Domain.Models;

namespace Sample.Tests.Domain.Models;

public class ValueTests
{
    [Fact]
    public void Shoud_Create_Value()
    {
        const int id = 100;
        const string name = "Fake 100";

        var value = new Value(id, name);

        Check.That(value.Id).Is(id);
        Check.That(value.Name).Is(name);
    }
}