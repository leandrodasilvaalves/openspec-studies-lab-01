using Bogus;
using FakeDataCli.Generators;

namespace FakeDataCli;

public static class CliDispatcher
{
    public const int SuccessExitCode = 0;
    public const int InvalidArgumentsExitCode = 2;

    private static readonly string[] NoFlags = Array.Empty<string>();
    private static readonly string[] GlobalFlags = { "--json", "--number" };
    private static readonly string[] PersonFlags = { "--uppercase", "--contact", "--cpf", "--json", "--number" };
    private static readonly string[] DocumentFlags = { "--no-punctuation", "--number" };
    private static readonly string[] GuidFlags = { "--no-dashes", "--uppercase", "--number" };
    private static readonly string[] ContactFlags = { "--json", "--number" };

    public static int Run(string[] args, TextWriter output, TextWriter error)
    {
        if (args.Length == 0)
        {
            WriteUsage(output);
            return SuccessExitCode;
        }

        var command = args[0];
        var flags = new List<string>();
        int count = 1;

        for (int i = 1; i < args.Length; i++)
        {
            if (args[i] == "-n" || args[i] == "--number")
            {
                if (i + 1 < args.Length && int.TryParse(args[i + 1], out var c))
                {
                    count = c;
                    i++;
                }
                else
                {
                    error.WriteLine("Error: Missing or invalid count for -n/--number.");
                    return InvalidArgumentsExitCode;
                }
            }
            else
            {
                flags.Add(args[i]);
            }
        }

        if (count > 100)
        {
            error.WriteLine("Error: Maximum limit is 100.");
            return InvalidArgumentsExitCode;
        }

        switch (command)
        {
            case "/help":
                if (RejectUnsupportedFlags(command, flags.ToArray(), NoFlags, error))
                {
                    return InvalidArgumentsExitCode;
                }

                WriteUsage(output);
                return SuccessExitCode;

            case "/person":
                if (RejectUnsupportedFlags(command, flags.ToArray(), PersonFlags, error))
                {
                    return InvalidArgumentsExitCode;
                }

                WritePerson(output, flags, count);
                return SuccessExitCode;

            case "/contact":
                if (RejectUnsupportedFlags(command, flags.ToArray(), ContactFlags, error))
                {
                    return InvalidArgumentsExitCode;
                }

                WriteContact(output, flags, count);
                return SuccessExitCode;

            case "/cpf":
                if (RejectUnsupportedFlags(command, flags.ToArray(), DocumentFlags, error))
                {
                    return InvalidArgumentsExitCode;
                }

                WriteDocument(output, BrazilianDocumentGenerator.GenerateCpfDigits,
                    BrazilianDocumentGenerator.FormatCpf, flags.Contains("--no-punctuation"), count);
                return SuccessExitCode;

            case "/cnpj":
                if (RejectUnsupportedFlags(command, flags.ToArray(), DocumentFlags, error))
                {
                    return InvalidArgumentsExitCode;
                }

                WriteDocument(output, BrazilianDocumentGenerator.GenerateCnpjDigits,
                    BrazilianDocumentGenerator.FormatCnpj, flags.Contains("--no-punctuation"), count);
                return SuccessExitCode;

            case "/guid":
                if (RejectUnsupportedFlags(command, flags.ToArray(), GuidFlags, error))
                {
                    return InvalidArgumentsExitCode;
                }

                WriteGuid(output, flags, count);
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

    private static void WritePerson(TextWriter output, IReadOnlyCollection<string> flags, int count)
    {
        var json = flags.Contains("--json");
        var uppercase = flags.Contains("--uppercase");
        
        if (json && count > 1) output.Write("[");
        
        for (int i = 0; i < count; i++)
        {
            var person = new PersonGenerator(new Faker("pt_BR")).Generate(uppercase);
            if (json)
            {
                var jsonOutput = new Dictionary<string, string>
                {
                    { "first_name", person.FirstName },
                    { "last_name", person.LastName },
                    { "full_name", person.FullName }
                };

                if (flags.Contains("--contact"))
                {
                    var contact = new ContactGenerator(SharedFaker.Instance).GenerateForPerson(person, uppercase);
                    jsonOutput.Add("phone", contact.Phone);
                    jsonOutput.Add("email", contact.Email);
                }

                if (flags.Contains("--cpf"))
                {
                    var cpf = BrazilianDocumentGenerator.GenerateCpfDigits();
                    jsonOutput.Add("cpf", BrazilianDocumentGenerator.FormatCpf(cpf));
                }

                var jsonString = System.Text.Json.JsonSerializer.Serialize(jsonOutput);
                output.Write(jsonString);
                if (i < count - 1) output.Write(", ");
            }
            else
            {
                output.WriteLine($"first_name: {person.FirstName}");
                output.WriteLine($"last_name: {person.LastName}");
                output.WriteLine($"full_name: {person.FullName}");

                if (flags.Contains("--contact"))
                {
                    var contact = new ContactGenerator(SharedFaker.Instance).GenerateForPerson(person, uppercase);
                    output.WriteLine($"phone: {contact.Phone}");
                    output.WriteLine($"email: {contact.Email}");
                }

                if (flags.Contains("--cpf"))
                {
                    var cpf = BrazilianDocumentGenerator.GenerateCpfDigits();
                    output.WriteLine($"cpf: {BrazilianDocumentGenerator.FormatCpf(cpf)}");
                }
            }
        }
        
        if (json)
        {
            if (count > 1) output.WriteLine("]");
            else output.WriteLine();
        }
    }

    private static void WriteContact(TextWriter output, IReadOnlyCollection<string> flags, int count)
    {
        var json = flags.Contains("--json");
        
        if (json && count > 1) output.Write("[");

        for (int i = 0; i < count; i++)
        {
            var contact = new ContactGenerator(new Faker("pt_BR")).Generate();
            if (json)
            {
                output.Write($"{{\"phone\": \"{contact.Phone}\", \"email\": \"{contact.Email}\"}}");
                if (i < count - 1) output.Write(", ");
            }
            else
            {
                output.WriteLine($"phone: {contact.Phone}");
                output.WriteLine($"email: {contact.Email}");
            }
        }
        
        if (json)
        {
            if (count > 1) output.WriteLine("]");
            else output.WriteLine();
        }
    }

    private static void WriteDocument(
        TextWriter output,
        Func<string> generateDigits,
        Func<string, string> format,
        bool noPunctuation,
        int count)
    {
        for (int i = 0; i < count; i++)
        {
            var digits = generateDigits();
            output.WriteLine(noPunctuation ? digits : format(digits));
        }
    }

    private static void WriteGuid(TextWriter output, IReadOnlyCollection<string> flags, int count)
    {
        var includeDashes = !flags.Contains("--no-dashes");
        var uppercase = flags.Contains("--uppercase");
        for (int i = 0; i < count; i++)
        {
            output.WriteLine(IdentifierGenerator.Format(Guid.NewGuid(), includeDashes, uppercase));
        }
    }

    private static void WriteUsage(TextWriter writer)
    {
        writer.WriteLine("FakeDataCli - Brazilian fake data generator");
        writer.WriteLine();
        writer.WriteLine("Usage:");
        writer.WriteLine("  /person [--uppercase] [--contact] [--cpf] [--json] [-n|--number <count>]");
        writer.WriteLine("  /contact [--json] [-n|--number <count>]");
        writer.WriteLine("  /cpf [--no-punctuation] [-n|--number <count>]");
        writer.WriteLine("  /cnpj [--no-punctuation] [-n|--number <count>]");
        writer.WriteLine("  /guid [--no-dashes] [--uppercase] [-n|--number <count>]");
        writer.WriteLine("  /help                              Show this help");
    }

    private static int ParseCount(IReadOnlyCollection<string> flags, TextWriter error)
    {
        // This is a simple implementation, it needs to handle -n 5 where 5 is the next element
        return 1;
    }
}
