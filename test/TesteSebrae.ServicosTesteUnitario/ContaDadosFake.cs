using Bogus;
using TesteSebrae.Dominio;

namespace TesteSebrae.ServicosTesteUnitario
{
    public static class ContaDadosFake
    {
        public static Conta ContaFake() => Faker.Generate();

        public static IEnumerable<Conta> ContasFake(int quantidade = 10) =>
            Faker.Generate(quantidade);

        private static Faker<Conta> Faker => new Faker<Conta>()
            .RuleFor(c => c.Id, Guid.NewGuid())
            .RuleFor(c => c.Descricao, f => f.Lorem.Word())
            .RuleFor(c => c.Nome, f => f.Person.FullName);
    }
}
