using TesteSebrae.Dominio;
using TesteSebrae.Infra.Interfaces;
using TesteSebrae.Servicos.Interfaces;

namespace TesteSebrae.Servicos
{
    public class ContaServico : IContaServico
    {
        private readonly IContaRepositorio _repositorio;

        public ContaServico(IContaRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<Conta> Adiciona(Conta conta)
        {
            conta.Id = Guid.NewGuid();
            return await _repositorio.Adiciona(conta);
        }

        public async Task Atualiza(Guid id, Conta conta)
        {
            conta.Id = id;
            await _repositorio.Atualiza(conta);
        }

        public async Task<IEnumerable<Conta>> BuscaTodos()
        {
            return await _repositorio.BuscaTodos();
        }

        public async Task<IEnumerable<Conta>> BuscaTodosPaginado(int quandiadeIgnorar, int quantidadePegar)
        {
            return await _repositorio.BuscaTodosPaginado(quandiadeIgnorar, quantidadePegar);
        }

        public async Task<bool> Deleta(Guid id)
        {
            return await _repositorio.Deleta(id);
        }

        public async Task<bool> Deleta(Conta conta)
        {
            return await _repositorio.Deleta(conta);
        }

        public async Task<Conta?> ProcuraPeloId(Guid id)
        {
            return await _repositorio.ProcuraPeloId(id);
        }
    }
}
