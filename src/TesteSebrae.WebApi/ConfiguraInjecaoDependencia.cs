using Microsoft.EntityFrameworkCore;
using TesteSebrae.Infra;
using TesteSebrae.Infra.Interfaces;
using TesteSebrae.Servicos;
using TesteSebrae.Servicos.Interfaces;

namespace TesteSebrae.WebApi
{
    public static class ConfiguraInjecaoDependencia
    {
        private const string NomeBaseDados = "TesteSebrae";

        public static IServiceCollection AdicionaInjecaoDependencia(
             this IServiceCollection services)
        {
            services.AddDbContext<Contexto>(options => options.UseInMemoryDatabase(NomeBaseDados));
            services.AdicionaInjecaoDependenciaServicos();
            services.AdicionaInjecaoDependenciaRepositorios();

            return services;
        }

        private static IServiceCollection AdicionaInjecaoDependenciaServicos(
             this IServiceCollection servicos)
        {
            servicos.AddScoped(typeof(IRequisicaoViaCep<>), typeof(RequisicaoViaCep<>));
            servicos.AddScoped<IContaServico, ContaServico>();

            return servicos;
        }

        private static IServiceCollection AdicionaInjecaoDependenciaRepositorios(
             this IServiceCollection servicos)
        {
            servicos.AddScoped(typeof(IRepositorioGenerico<>), typeof(RepositorioGenerico<>));
            servicos.AddScoped<IContaRepositorio, ContaRepositorio>();

            return servicos;
        }
    }
}
