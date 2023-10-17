using Microsoft.EntityFrameworkCore;
using TesteSebrae.Dominio;
using TesteSebrae.Infra.Interfaces;

namespace TesteSebrae.Infra
{
    public class ContaRepositorio : RepositorioGenerico<Conta>, IContaRepositorio
    {
        public ContaRepositorio(Contexto contexto) : base(contexto)
        {
        }

        public async Task<IEnumerable<Conta>> BuscaTodos(CancellationToken cancellationToken = default)
        {
            return await Tabela.ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Conta>> BuscaTodosPaginado(int quantidadeIgnorar, int quantidadePegar,
            CancellationToken cancellationToken = default)
        {
            return await Tabela.Skip(quantidadeIgnorar).Take(quantidadePegar)
                .ToListAsync(cancellationToken);
        }
    }
}
