using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace TesteSebrae.Infra
{
    public class Contexto : DbContext
    {
        private const string NomeBaseDados = "TesteSebrae";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(NomeBaseDados);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}