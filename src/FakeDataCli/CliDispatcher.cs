using FakeDataCli.Generators;

namespace FakeDataCli;

public static class CliDispatcher
{
    public const int SuccessExitCode = 0;
    public const int InvalidArgumentsExitCode = 2;

    private static readonly string[] NoFlags = Array.Empty<string>();
    private static readonly string[] DocumentFlags = { "--no-punctuation" };
    private static readonly string[] GuidFlags = { "--no-dashes", "--uppercase" };

    public static int Run(string[] args, TextWriter output, TextWriter error)
    {
        if (args.Length == 0)
        {
            WriteUsage(output);
            return SuccessExitCode;
        }

        var command = args[0];
        var flags = args.Skip(1).ToArray();

        switch (command)
        {
            case "/help":
                if (RejectUnsupportedFlags(command, flags, NoFlags, error))
                {
                    return InvalidArgumentsExitCode;
                }

                WriteUsage(output);
                return SuccessExitCode;

            case "/person":
                if (RejectUnsupportedFlags(command, flags, NoFlags, error))
                {
                    return InvalidArgumentsExitCode;
                }

                WritePerson(output);
                return SuccessExitCode;

            case "/contact":
                if (RejectUnsupportedFlags(command, flags, NoFlags, error))
                {
                    return InvalidArgumentsExitCode;
                }

                WriteContact(output);
                return SuccessExitCode;

            case "/cpf":
                if (RejectUnsupportedFlags(command, flags, DocumentFlags, error))
                {
                    return InvalidArgumentsExitCode;
                }

                WriteDocument(output, BrazilianDocumentGenerator.GenerateCpfDigits,
                    BrazilianDocumentGenerator.FormatCpf, flags.Contains("--no-punctuation"));
                return SuccessExitCode;

            case "/cnpj":
                if (RejectUnsupportedFlags(command, flags, DocumentFlags, error))
                {
                    return InvalidArgumentsExitCode;
                }

                WriteDocument(output, BrazilianDocumentGenerator.GenerateCnpjDigits,
                    BrazilianDocumentGenerator.FormatCnpj, flags.Contains("--no-punctuation"));
                return SuccessExitCode;

            case "/guid":
                if (RejectUnsupportedFlags(command, flags, GuidFlags, error))
                {
                    return InvalidArgumentsExitCode;
                }

                WriteGuid(output, flags);
                return SuccessExitCode;

            default:
                error.WriteLine($"Unknown command: {command}");
                WriteUsage(error);
                return InvalidArgumentsExitCode;
        }
    }

    private static bool RejectUnsupportedFlags(string command, string[] flags, string[] allowedFlags, TextWriter error)
    {
        foreach (var flag in flags)
        {
            if (!allowedFlags.Contains(flag, StringComparer.Ordinal))
            {
                error.WriteLine($"Unknown or unsupported flag '{flag}' for command '{command}'.");
                WriteUsage(error);
                return true;
            }
        }

        return false;
    }

    private static void WritePerson(TextWriter output)
    {
        var person = new PersonGenerator(SharedFaker.Instance).Generate();
        output.WriteLine($"first_name: {person.FirstName}");
        output.WriteLine($"last_name: {person.LastName}");
        output.WriteLine($"full_name: {person.FullName}");
    }

    private static void WriteContact(TextWriter output)
    {
        var contact = new ContactGenerator(SharedFaker.Instance).Generate();
        output.WriteLine($"phone: {contact.Phone}");
        output.WriteLine($"email: {contact.Email}");
    }

    private static void WriteDocument(
        TextWriter output,
        Func<string> generateDigits,
        Func<string, string> format,
        bool noPunctuation)
    {
        var digits = generateDigits();
        output.WriteLine(noPunctuation ? digits : format(digits));
    }

    private static void WriteGuid(TextWriter output, IReadOnlyCollection<string> flags)
    {
        var includeDashes = !flags.Contains("--no-dashes");
        var uppercase = flags.Contains("--uppercase");
        output.WriteLine(IdentifierGenerator.Format(Guid.NewGuid(), includeDashes, uppercase));
    }

    private static void WriteUsage(TextWriter writer)
    {
        writer.WriteLine("FakeDataCli - Brazilian fake data generator");
        writer.WriteLine();
        writer.WriteLine("Usage:");
        writer.WriteLine("  /person                            Generate first_name, last_name and full_name");
        writer.WriteLine("  /contact                           Generate phone and email");
        writer.WriteLine("  /cpf [--no-punctuation]            Generate a valid CPF");
        writer.WriteLine("  /cnpj [--no-punctuation]           Generate a valid CNPJ");
        writer.WriteLine("  /guid [--no-dashes] [--uppercase]  Generate a GUID");
        writer.WriteLine("  /help                              Show this help");
    }
}
