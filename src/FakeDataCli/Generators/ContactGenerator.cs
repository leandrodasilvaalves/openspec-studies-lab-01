using System.Globalization;
using System.Text;
using Bogus;

namespace FakeDataCli.Generators;

public sealed record ContactData(string Phone, string Email);

public sealed class ContactGenerator
{
    private readonly Faker _faker;

    public ContactGenerator(Faker faker)
    {
        _faker = faker ?? throw new ArgumentNullException(nameof(faker));
    }

    public ContactData Generate()
    {
        var phone = _faker.Phone.PhoneNumber();
        var email = NormalizeEmail(_faker.Internet.Email(), uppercase: false);
        return new ContactData(phone, email);
    }

    public ContactData GenerateForPerson(PersonData person, bool uppercase = false)
    {
        if (person is null)
        {
            throw new ArgumentNullException(nameof(person));
        }

        var phone = _faker.Phone.PhoneNumber();
        var number = _faker.Random.Number(0, 9999).ToString("D4");
        var domain = _faker.Internet.DomainName();
        var email = NormalizeEmail($"{person.FirstName}{number}@{domain}", uppercase);
        return new ContactData(phone, email);
    }

    private static string NormalizeEmail(string email, bool uppercase)
    {
        var decomposed = email.Normalize(NormalizationForm.FormD);
        var builder = new StringBuilder(decomposed.Length);
        foreach (var character in decomposed)
        {
            if (CharUnicodeInfo.GetUnicodeCategory(character) != UnicodeCategory.NonSpacingMark)
            {
                builder.Append(character);
            }
        }

        var withoutDiacritics = builder.ToString().Normalize(NormalizationForm.FormC);
        return uppercase ? withoutDiacritics.ToUpperInvariant() : withoutDiacritics.ToLowerInvariant();
    }
}
