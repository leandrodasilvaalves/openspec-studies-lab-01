namespace FakeDataCli.Tests;

public class CliDispatcherTests
{
    private static (int ExitCode, string Output, string Error) Invoke(params string[] args)
    {
        using var output = new StringWriter();
        using var error = new StringWriter();
        var exitCode = CliDispatcher.Run(args, output, error);
        return (exitCode, output.ToString(), error.ToString());
    }

    private static Dictionary<string, string> ParseLabeled(string output)
    {
        return output
            .Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Select(line => line.Split(": ", 2))
            .ToDictionary(parts => parts[0], parts => parts[1]);
    }

    [Fact]
    public void HelpReturnsSuccessAndListsCommandsAndFlags()
    {
        var result = Invoke("/help");

        Assert.Equal(CliDispatcher.SuccessExitCode, result.ExitCode);
        foreach (var token in new[]
                 {
                     "/person", "/contact", "/cpf", "/cnpj", "/guid", "/help",
                     "--no-punctuation", "--no-dashes", "--uppercase"
                 })
        {
            Assert.Contains(token, result.Output);
        }

        Assert.Equal(string.Empty, result.Error);
    }

    [Fact]
    public void NoArgumentsShowsTheSameUsageAsHelp()
    {
        var help = Invoke("/help");
        var none = Invoke();

        Assert.Equal(CliDispatcher.SuccessExitCode, none.ExitCode);
        Assert.Equal(help.Output, none.Output);
    }

    [Fact]
    public void UnknownCommandReturnsInvalidArgumentsAndWritesUsageToError()
    {
        var result = Invoke("/unknown");

        Assert.Equal(CliDispatcher.InvalidArgumentsExitCode, result.ExitCode);
        Assert.Contains("Unknown command", result.Error);
        Assert.Contains("/help", result.Error);
        Assert.Equal(string.Empty, result.Output);
    }

    [Theory]
    [InlineData("/person", "--uppercase")]
    [InlineData("/contact", "--no-punctuation")]
    [InlineData("/guid", "--no-punctuation")]
    [InlineData("/help", "--no-dashes")]
    public void UnsupportedFlagReturnsInvalidArguments(string command, string flag)
    {
        var result = Invoke(command, flag);

        Assert.Equal(CliDispatcher.InvalidArgumentsExitCode, result.ExitCode);
        Assert.Contains(flag, result.Error);
        Assert.Equal(string.Empty, result.Output);
    }

    [Fact]
    public void PersonPrintsLabelledFieldsWithConsistentFullName()
    {
        var result = Invoke("/person");

        Assert.Equal(CliDispatcher.SuccessExitCode, result.ExitCode);
        var fields = ParseLabeled(result.Output);

        Assert.False(string.IsNullOrWhiteSpace(fields["first_name"]));
        Assert.False(string.IsNullOrWhiteSpace(fields["last_name"]));
        Assert.Equal($"{fields["first_name"]} {fields["last_name"]}", fields["full_name"]);
    }

    [Fact]
    public void ContactPrintsLabeledPhoneAndEmail()
    {
        var result = Invoke("/contact");

        Assert.Equal(CliDispatcher.SuccessExitCode, result.ExitCode);
        var fields = ParseLabeled(result.Output);

        Assert.Matches(@"^(\+55\s)?\(\d{2}\)\s?\d{4,5}-\d{4}$", fields["phone"]);
        Assert.Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", fields["email"]);
    }

    [Fact]
    public void CpfIsPunctuatedByDefaultAndValid()
    {
        var result = Invoke("/cpf");

        Assert.Equal(CliDispatcher.SuccessExitCode, result.ExitCode);
        var value = result.Output.Trim();
        Assert.Matches(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$", value);
        Assert.True(DocumentValidator.IsValidCpf(value));
    }

    [Fact]
    public void CpfWithoutPunctuationPrintsOnlyDigits()
    {
        var result = Invoke("/cpf", "--no-punctuation");

        Assert.Equal(CliDispatcher.SuccessExitCode, result.ExitCode);
        var value = result.Output.Trim();
        Assert.Matches(@"^\d{11}$", value);
        Assert.True(DocumentValidator.IsValidCpf(value));
    }

    [Fact]
    public void CnpjIsPunctuatedByDefaultAndValid()
    {
        var result = Invoke("/cnpj");

        Assert.Equal(CliDispatcher.SuccessExitCode, result.ExitCode);
        var value = result.Output.Trim();
        Assert.Matches(@"^\d{2}\.\d{3}\.\d{3}/\d{4}-\d{2}$", value);
        Assert.True(DocumentValidator.IsValidCnpj(value));
    }

    [Fact]
    public void CnpjWithoutPunctuationPrintsOnlyDigits()
    {
        var result = Invoke("/cnpj", "--no-punctuation");

        Assert.Equal(CliDispatcher.SuccessExitCode, result.ExitCode);
        var value = result.Output.Trim();
        Assert.Matches(@"^\d{14}$", value);
        Assert.True(DocumentValidator.IsValidCnpj(value));
    }

    [Fact]
    public void GuidIsDashedLowercaseByDefault()
    {
        var result = Invoke("/guid");

        Assert.Equal(CliDispatcher.SuccessExitCode, result.ExitCode);
        var value = result.Output.Trim();
        Assert.Matches(
            "^[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}$",
            value);
        Assert.True(Guid.TryParse(value, out _));
    }

    [Fact]
    public void GuidWithoutDashesAndUppercasePrintsCompactUppercaseValue()
    {
        var result = Invoke("/guid", "--no-dashes", "--uppercase");

        Assert.Equal(CliDispatcher.SuccessExitCode, result.ExitCode);
        var value = result.Output.Trim();
        Assert.Matches("^[0-9A-F]{32}$", value);
        Assert.DoesNotContain("-", value);
    }
}
