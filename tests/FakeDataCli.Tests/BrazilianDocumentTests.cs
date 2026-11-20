using FakeDataCli.Generators;

namespace FakeDataCli.Tests;

public class BrazilianDocumentTests
{
    [Theory]
    [InlineData("52998224725")]
    [InlineData("529.982.247-25")]
    public void KnownValidCpfIsAccepted(string cpf)
    {
        Assert.True(DocumentValidator.IsValidCpf(cpf));
    }

    [Theory]
    [InlineData("52998224724")]
    [InlineData("11111111111")]
    [InlineData("00000000000")]
    public void KnownInvalidCpfIsRejected(string cpf)
    {
        Assert.False(DocumentValidator.IsValidCpf(cpf));
    }

    [Theory]
    [InlineData("11222333000181")]
    [InlineData("11.222.333/0001-81")]
    public void KnownValidCnpjIsAccepted(string cnpj)
    {
        Assert.True(DocumentValidator.IsValidCnpj(cnpj));
    }

    [Theory]
    [InlineData("11222333000180")]
    [InlineData("11111111111111")]
    [InlineData("00000000000000")]
    public void KnownInvalidCnpjIsRejected(string cnpj)
    {
        Assert.False(DocumentValidator.IsValidCnpj(cnpj));
    }

    [Fact]
    public void GeneratedCpfDigitsAreValidAndNotRepeated()
    {
        for (var run = 0; run < 500; run++)
        {
            var cpf = BrazilianDocumentGenerator.GenerateCpfDigits();

            Assert.Equal(11, cpf.Length);
            Assert.All(cpf, character => Assert.True(char.IsDigit(character)));
            Assert.True(DocumentValidator.IsValidCpf(cpf));
            Assert.False(cpf.Distinct().Count() == 1);
        }
    }

    [Fact]
    public void GeneratedCnpjDigitsAreValidAndNotRepeated()
    {
        for (var run = 0; run < 500; run++)
        {
            var cnpj = BrazilianDocumentGenerator.GenerateCnpjDigits();

            Assert.Equal(14, cnpj.Length);
            Assert.All(cnpj, character => Assert.True(char.IsDigit(character)));
            Assert.True(DocumentValidator.IsValidCnpj(cnpj));
            Assert.False(cnpj.Distinct().Count() == 1);
        }
    }

    [Fact]
    public void FormatCpfAddsPunctuation()
    {
        Assert.Equal("529.982.247-25", BrazilianDocumentGenerator.FormatCpf("52998224725"));
    }

    [Fact]
    public void FormatCnpjAddsPunctuation()
    {
        Assert.Equal("11.222.333/0001-81", BrazilianDocumentGenerator.FormatCnpj("11222333000181"));
    }

    [Fact]
    public void FormattingPreservesCheckDigitValidity()
    {
        var cpf = BrazilianDocumentGenerator.GenerateCpfDigits();
        var cnpj = BrazilianDocumentGenerator.GenerateCnpjDigits();

        Assert.True(DocumentValidator.IsValidCpf(BrazilianDocumentGenerator.FormatCpf(cpf)));
        Assert.True(DocumentValidator.IsValidCnpj(BrazilianDocumentGenerator.FormatCnpj(cnpj)));
    }
}
