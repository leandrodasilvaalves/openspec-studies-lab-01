namespace FakeDataCli.Tests;

/// <summary>
/// Independent check-digit validator used only by tests, deliberately implemented
/// separately from the production generator.
/// </summary>
internal static class DocumentValidator
{
    public static bool IsValidCpf(string value)
    {
        var digits = OnlyDigits(value);
        if (digits.Length != 11 || AllSame(digits))
        {
            return false;
        }

        var first = CpfCheckDigit(digits, 9);
        var second = CpfCheckDigit(digits, 10);
        return digits[9] - '0' == first && digits[10] - '0' == second;
    }

    public static bool IsValidCnpj(string value)
    {
        var digits = OnlyDigits(value);
        if (digits.Length != 14 || AllSame(digits))
        {
            return false;
        }

        var first = CnpjCheckDigit(digits, 12);
        var second = CnpjCheckDigit(digits, 13);
        return digits[12] - '0' == first && digits[13] - '0' == second;
    }

    private static int CpfCheckDigit(string digits, int length)
    {
        var sum = 0;
        for (var index = 0; index < length; index++)
        {
            sum += (digits[index] - '0') * (length + 1 - index);
        }

        var remainder = sum % 11;
        return remainder < 2 ? 0 : 11 - remainder;
    }

    private static int CnpjCheckDigit(string digits, int length)
    {
        int[] weights = length == 12
            ? new[] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 }
            : new[] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        var sum = 0;
        for (var index = 0; index < length; index++)
        {
            sum += (digits[index] - '0') * weights[index];
        }

        var remainder = sum % 11;
        return remainder < 2 ? 0 : 11 - remainder;
    }

    private static string OnlyDigits(string value) => new(value.Where(char.IsDigit).ToArray());

    private static bool AllSame(string value) => value.Distinct().Count() == 1;
}
