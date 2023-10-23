using CastGroup.Web.Models;
using CastGroup.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CastGroup.Web.Pages.Accounts
{
    public class CreateModel : PageModel
    {
        private readonly ConsumeCastGroupApis _service;

        public CreateModel(ConsumeCastGroupApis service)
        {
            this._service = service;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public AccountResponse Account { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || Account == null)
            {
                return Page();
            }

            await _service.CreateAccount(Account);

            return RedirectToPage("./Index");
        }
    }
}
