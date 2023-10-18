using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TesteSebrae.Servicos.Interfaces;
using TesteSebrae.Web.Modelos;

namespace BaltaTest.Pages_Contas
{
    public class EditaModel : PageModel
    {
        private readonly IContaServico _servico;

        public EditaModel(IContaServico servico)
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
            Conta = conta;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //_context.Attach(Conta).State = EntityState.Modified;

            try
            {
                await _servico.Atualiza(Conta.Id, Conta);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ContaExists(Conta.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Principal");
        }

        private async Task<bool> ContaExists(Guid id)
        {
            var conta = await _servico.ProcuraPeloId(id);
            return conta is not null;
        }
    }
}
