using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TesteSebrae.Servicos.Interfaces;
using TesteSebrae.Web.Modelos;

namespace BaltaTest.Pages_Contas
{
    public class DeletaModel : PageModel
    {
        private readonly IContaServico _servico;

        public DeletaModel(IContaServico servico)
        {
            _servico = servico;
        }

        [BindProperty]
        public Conta Conta { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var conta = await _servico.ProcuraPeloId(id.Value);

            if (conta == null)
            {
                return NotFound();
            }
            else
            {
                Conta = conta;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            await _servico.Deleta(id.Value);

            return RedirectToPage("./Principal");
        }
    }
}
