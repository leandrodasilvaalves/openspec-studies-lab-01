using Bogus;

namespace FakeDataCli;

public static class SharedFaker
{
    public static Faker Instance { get; } = new("pt_BR");
}
