using Microsoft.AspNetCore.Mvc.RazorPages;
using TesteSebrae.Servicos.Interfaces;
using TesteSebrae.Web.Modelos;

namespace BaltaTest.Pages_Contas
{
    public class PrincipalModel : PageModel
    {
        private readonly IContaServico _servico;

        public PrincipalModel(IContaServico servico)
        {
            _servico = servico;
        }

        public IList<Conta> Conta { get; set; } = default!;

        public async Task OnGetAsync()
        {
            var contas = await _servico.BuscaTodos();
            Conta = contas.Select(c => (Conta)c).ToList();
        }
    }
}
