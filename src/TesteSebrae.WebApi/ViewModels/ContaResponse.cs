using TesteSebrae.Dominio;

namespace TesteSebrae.WebApi.ViewModels
{
    public class ContaResponse : ContaRequest
    {
        public Guid Id { get; set; }

        public static implicit operator Conta(ContaResponse response)
        {
            return new Conta
            {
                Id = response.Id,
                Descricao = response.Descricao,
                Nome = response.Nome
            };
        }

        public static implicit operator ContaResponse(Conta conta)
        {
            return new ContaResponse
            {
                Id = conta.Id,
                Descricao = conta.Descricao,
                Nome = conta.Nome
            };
        }
    }
}
