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
}
