using Bogus;

namespace FakeDataCli.Generators;

public sealed record PersonData(string FirstName, string LastName, string FullName);

public sealed class PersonGenerator
{
    private readonly Faker _faker;

    public PersonGenerator(Faker faker)
    {
        _faker = faker ?? throw new ArgumentNullException(nameof(faker));
    }

    public PersonData Generate()
    {
        var firstName = _faker.Person.FirstName;
        var lastName = _faker.Person.LastName;
        return new PersonData(firstName, lastName, $"{firstName} {lastName}");
    }
}
