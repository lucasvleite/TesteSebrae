using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TesteSebrae.Servicos.Interfaces;
using TesteSebrae.Web.Modelos;

namespace BaltaTest.Pages_Contas
{
    public class InsereModel : PageModel
    {
        private readonly IContaServico _servico;

        public InsereModel(IContaServico servico)
        {
            _servico = servico;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Conta Conta { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || Conta == null)
            {
                return Page();
            }

            await _servico.Adiciona(Conta);

            return RedirectToPage("./Principal");
        }
    }
}
