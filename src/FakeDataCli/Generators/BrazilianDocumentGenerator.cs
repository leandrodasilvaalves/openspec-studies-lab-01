namespace FakeDataCli.Generators;

public static class BrazilianDocumentGenerator
{
    private static readonly int[] CnpjFirstWeights = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
    private static readonly int[] CnpjSecondWeights = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

    public static string GenerateCpfDigits()
    {
        while (true)
        {
            var digits = new char[11];
            FillRandomDigits(digits, 0, 9);
            digits[9] = ToChar(ComputeCpfCheckDigit(digits, 9));
            digits[10] = ToChar(ComputeCpfCheckDigit(digits, 10));

            var cpf = new string(digits);
            if (!IsRepeatedSequence(cpf))
            {
                return cpf;
            }
        }
    }

    public static string GenerateCnpjDigits()
    {
        while (true)
        {
            var digits = new char[14];
            FillRandomDigits(digits, 0, 12);
            digits[12] = ToChar(ComputeCnpjCheckDigit(digits, 12));
            digits[13] = ToChar(ComputeCnpjCheckDigit(digits, 13));

            var cnpj = new string(digits);
            if (!IsRepeatedSequence(cnpj))
            {
                return cnpj;
            }
        }
    }

    public static string FormatCpf(string digits)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(digits);
        return $"{digits[..3]}.{digits[3..6]}.{digits[6..9]}-{digits[9..]}";
    }

    public static string FormatCnpj(string digits)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(digits);
        return $"{digits[..2]}.{digits[2..5]}.{digits[5..8]}/{digits[8..12]}-{digits[12..]}";
    }

    private static void FillRandomDigits(char[] digits, int start, int end)
    {
        for (var index = start; index < end; index++)
        {
            digits[index] = ToChar(Random.Shared.Next(10));
        }
    }

    private static int ComputeCpfCheckDigit(char[] digits, int length)
    {
        var sum = 0;
        for (var index = 0; index < length; index++)
        {
            sum += ToDigit(digits[index]) * (length + 1 - index);
        }

        var remainder = sum % 11;
        return remainder < 2 ? 0 : 11 - remainder;
    }

    private static int ComputeCnpjCheckDigit(char[] digits, int length)
    {
        var weights = length == 12 ? CnpjFirstWeights : CnpjSecondWeights;
        var sum = 0;
        for (var index = 0; index < length; index++)
        {
            sum += ToDigit(digits[index]) * weights[index];
        }

        var remainder = sum % 11;
        return remainder < 2 ? 0 : 11 - remainder;
    }

    private static bool IsRepeatedSequence(string value)
    {
        return value.Distinct().Count() == 1;
    }

    private static int ToDigit(char value) => value - '0';

    private static char ToChar(int value) => (char)('0' + value);
}
