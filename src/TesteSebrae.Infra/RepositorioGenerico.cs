using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TesteSebrae.Infra.Interfaces;

namespace TesteSebrae.Infra
{
    public class RepositorioGenerico<T> : IRepositorioGenerico<T> where T : class
    {
        internal Contexto Contexto;
        internal DbSet<T> Tabela;

        public RepositorioGenerico(Contexto contexto)
        {
            this.Contexto = contexto;
            this.Tabela = contexto.Set<T>();
        }

        public async Task<T?> ProcuraPeloId(Guid id, CancellationToken cancellationToken = default)
        {
            var chaveId = new object[] { id };
            return await Tabela.FindAsync(chaveId, cancellationToken);
        }

        public async Task<T> Adiciona(T entidade, CancellationToken cancellationToken = default)
        {
            await Tabela.AddAsync(entidade, cancellationToken);
            await Contexto.SaveChangesAsync(cancellationToken);
            return entidade;
        }

        public async Task Deleta(Guid id, CancellationToken cancellationToken = default)
        {
            T? entidade = await ProcuraPeloId(id, cancellationToken);
            if (entidade != null)
            {
                await Deleta(entidade, cancellationToken);
            }
        }

        public async Task Deleta(T entidade, CancellationToken cancellationToken = default)
        {
            if (Contexto.Entry(entidade).State == EntityState.Detached)
            {
                Tabela.Attach(entidade);
            }
            Tabela.Remove(entidade);
            await Contexto.SaveChangesAsync(cancellationToken);
        }

        public async Task Atualiza(T entidade, CancellationToken cancellationToken = default)
        {
            Tabela.Attach(entidade);
            Contexto.Entry(entidade).State = EntityState.Modified;
            await Contexto.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<T>> Procura(Expression<Func<T, bool>>? filtro = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? ordenado = null,
            string incluiPropriedades = "", CancellationToken cancellationToken = default)
        {
            IQueryable<T> query = Tabela;

            if (filtro != null)
            {
                query = query.Where(filtro);
            }

            foreach (var includeProperty in incluiPropriedades.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (ordenado != null)
            {
                return await ordenado(query).ToListAsync(cancellationToken);
            }
            else
            {
                return await query.ToListAsync(cancellationToken);
            }
        }
    }
}
