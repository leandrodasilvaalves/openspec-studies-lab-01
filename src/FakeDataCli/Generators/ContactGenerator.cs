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
        var email = _faker.Internet.Email();
        return new ContactData(phone, email);
    }

    public ContactData GenerateForPerson(PersonData person)
    {
        if (person is null)
        {
            throw new ArgumentNullException(nameof(person));
        }

        var phone = _faker.Phone.PhoneNumber();
        var number = _faker.Random.Number(0, 9999).ToString("D4");
        var domain = _faker.Internet.DomainName();
        var email = $"{person.FirstName}{number}@{domain}";
        return new ContactData(phone, email);
    }
}
