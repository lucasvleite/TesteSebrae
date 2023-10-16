using Microsoft.EntityFrameworkCore;
using TesteSebrae.Dominio;

namespace TesteSebrae.Infra
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options) { }


        public DbSet<Conta> Contas { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(Contexto).Assembly);
            base.OnModelCreating(builder);
        }
    }
}