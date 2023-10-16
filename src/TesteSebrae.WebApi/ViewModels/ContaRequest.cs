using TesteSebrae.Dominio;

namespace TesteSebrae.WebApi.ViewModels
{
    public class ContaRequest
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }


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
