using TesteSebrae.Dominio;

namespace TesteSebrae.WebApi.ViewModels
{
    public class ContaRequest
    {
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;


        public static implicit operator Conta(ContaRequest request)
        {
            return new Conta
            {
                Descricao = request.Descricao,
                Nome = request.Nome
            };
        }
    }
}
