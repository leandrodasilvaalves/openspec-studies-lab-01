using System.Globalization;
using System.Text;

using FakeDataCli.Generators;

namespace FakeDataCli.Tests;

public class PersonContactGeneratorTests
{
    [Fact]
    public void PersonGeneratorProducesNonEmptyFieldsAndConsistentFullName()
    {
        var generator = new PersonGenerator(SharedFaker.Instance);

        for (var run = 0; run < 50; run++)
        {
            var person = generator.Generate();

            Assert.False(string.IsNullOrWhiteSpace(person.FirstName));
            Assert.False(string.IsNullOrWhiteSpace(person.LastName));
            Assert.False(string.IsNullOrWhiteSpace(person.FullName));
            Assert.Equal($"{person.FirstName} {person.LastName}", person.FullName);
        }
    }

    [Fact]
    public void ContactGeneratorProducesBrazilianPhoneAndValidEmail()
    {
        var generator = new ContactGenerator(SharedFaker.Instance);

        for (var run = 0; run < 50; run++)
        {
            var contact = generator.Generate();

            Assert.Matches(@"^(\+55\s)?\(\d{2}\)\s?\d{4,5}-\d{4}$", contact.Phone);
            Assert.Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", contact.Email);
        }
    }

    [Fact]
    public void GenerateReturnsEmailLowercaseAndWithoutDiacritics()
    {
        var generator = new ContactGenerator(SharedFaker.Instance);

        for (var run = 0; run < 50; run++)
        {
            var contact = generator.Generate();

            Assert.Equal(contact.Email.ToLowerInvariant(), contact.Email);
            Assert.True(IsOnlyAsciiWithoutDiacritics(contact.Email) && IsAllASCII(contact.Email));
        }
    }

    [Fact]
    public void GenerateForPersonNormalizesEmailToLowercaseWithoutDiacritics()
    {
        var generator = new ContactGenerator(SharedFaker.Instance);
        var person = new PersonData("José", "Conceição", "José Conceição");

        var contact = generator.GenerateForPerson(person);

        Assert.True(contact.Email.StartsWith("jose"));
        Assert.Equal(contact.Email.ToLowerInvariant(), contact.Email);
        Assert.True(IsOnlyAsciiWithoutDiacritics(contact.Email) && IsAllASCII(contact.Email));
        Assert.Matches(@"^jose\d{4}@[^@\s]+\.[^@\s]+$", contact.Email);
    }

    [Fact]
    public void GenerateForPersonWithUppercaseReturnsEmailUppercaseWithoutDiacritics()
    {
        var generator = new ContactGenerator(SharedFaker.Instance);
        var person = new PersonData("José", "Conceição", "José Conceição");

        var contact = generator.GenerateForPerson(person, uppercase: true);

        Assert.True(contact.Email.StartsWith("JOSE"));
        Assert.Equal(contact.Email.ToUpperInvariant(), contact.Email);
        Assert.True(IsOnlyAsciiWithoutDiacritics(contact.Email) && IsAllASCII(contact.Email));
        Assert.Matches(@"^JOSE\d{4}@[^@\s]+\.[^@\s]+$", contact.Email);
    }

    private static bool IsOnlyAsciiWithoutDiacritics(string text)
    {
        foreach (var character in text.Normalize(NormalizationForm.FormD))
        {
            if (CharUnicodeInfo.GetUnicodeCategory(character) == UnicodeCategory.NonSpacingMark)
                return false;
        }
        return true;
    }

    private static bool IsAllASCII(string text)
    {
        foreach (char c in text)
        {
            if (!char.IsAscii(c))
                return false;
        }
        return true;
    }
}
