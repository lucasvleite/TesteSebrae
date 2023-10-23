using CastGroup.Web.Models;
using CastGroup.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CastGroup.Web.Pages.Accounts
{
    public class EditModel : PageModel
    {
        private readonly ConsumeCastGroupApis _service;

        public EditModel(ConsumeCastGroupApis service)
        {
            _service = service;
        }

        [BindProperty]
        public AccountResponse Account { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _service.GetAccount(id.Value);
            if (account == null)
            {
                return NotFound();
            }
            Account = account;
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

            try
            {
                await _service.EditAccount(Account);
            }
            catch
            {
                return NotFound();
            }

            return RedirectToPage("./Index");
        }
    }
}
