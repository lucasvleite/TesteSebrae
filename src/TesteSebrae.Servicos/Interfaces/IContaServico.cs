using TesteSebrae.Dominio;

namespace TesteSebrae.Servicos.Interfaces
{
    public interface IContaServico
    {
        Task<Conta> Adiciona(Conta conta);

        Task Atualiza(Guid id, Conta conta);

        Task<IEnumerable<Conta>> BuscaTodos();

        Task<IEnumerable<Conta>> BuscaTodosPaginado(int quandiadeIgnorar, int quantidadePegar);

        Task<bool> Deleta(Guid id);

        Task<bool> Deleta(Conta conta);

        Task<Conta> ProcuraPeloId(Guid id);
    }
}
