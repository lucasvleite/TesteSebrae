using System.Linq.Expressions;

namespace TesteSebrae.Infra.Interfaces
{
    public interface IRepositorioGenerico<T> where T : class
    {
        Task<T?> ProcuraPeloId(Guid id, CancellationToken cancellationToken = default);

        Task<T> Adiciona(T entidade, CancellationToken cancellationToken = default);

        Task<bool> Deleta(Guid id, CancellationToken cancellationToken = default);

        Task<bool> Deleta(T entidade, CancellationToken cancellationToken = default);

        Task Atualiza(T entidade, CancellationToken cancellationToken = default);

        Task<IEnumerable<T>> Procura(Expression<Func<T, bool>>? filtro = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? ordenado = null,
            string incluiPropriedades = "", CancellationToken cancellationToken = default);
    }
}
