namespace FakeDataCli.Generators;

public static class IdentifierGenerator
{
    public static string Format(Guid guid, bool includeDashes = true, bool uppercase = false)
    {
        var value = guid.ToString(includeDashes ? "D" : "N");
        return uppercase ? value.ToUpperInvariant() : value.ToLowerInvariant();
    }
}
