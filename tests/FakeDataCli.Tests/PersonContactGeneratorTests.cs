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
}
