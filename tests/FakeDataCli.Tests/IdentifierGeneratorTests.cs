using FakeDataCli.Generators;

namespace FakeDataCli.Tests;

public class IdentifierGeneratorTests
{
    [Fact]
    public void DefaultFormatKeepsDashesAndLowercase()
    {
        var value = IdentifierGenerator.Format(Guid.NewGuid());

        Assert.Matches(
            "^[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}$",
            value);
        Assert.True(Guid.TryParse(value, out _));
    }

    [Fact]
    public void NoDashesProducesThirtyTwoHexCharacters()
    {
        var value = IdentifierGenerator.Format(Guid.NewGuid(), includeDashes: false);

        Assert.Matches("^[0-9a-f]{32}$", value);
        Assert.DoesNotContain("-", value);
    }

    [Fact]
    public void UppercaseProducesUppercaseLettersKeepingDashesByDefault()
    {
        var value = IdentifierGenerator.Format(Guid.NewGuid(), uppercase: true);

        Assert.Equal(value.ToUpperInvariant(), value);
        Assert.Equal(36, value.Length);
        Assert.Matches(
            "^[0-9A-F]{8}-[0-9A-F]{4}-[0-9A-F]{4}-[0-9A-F]{4}-[0-9A-F]{12}$",
            value);
    }

    [Fact]
    public void NoDashesAndUppercaseCanBeCombined()
    {
        var value = IdentifierGenerator.Format(Guid.NewGuid(), includeDashes: false, uppercase: true);

        Assert.Equal(32, value.Length);
        Assert.DoesNotContain("-", value);
        Assert.Equal(value.ToUpperInvariant(), value);
        Assert.Matches("^[0-9A-F]{32}$", value);
    }
}
