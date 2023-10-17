using TesteSebrae.Dominio;

namespace TesteSebrae.Infra.Interfaces
{
    public interface IContaRepositorio : IRepositorioGenerico<Conta>
    {
        Task<IEnumerable<Conta>> BuscaTodos(CancellationToken cancellationToken = default);

        Task<IEnumerable<Conta>> BuscaTodosPaginado(int quantidadeIgnorar, int quantidadePegar,
            CancellationToken cancellationToken = default);
    }
}
