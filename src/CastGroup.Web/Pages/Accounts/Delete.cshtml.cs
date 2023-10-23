using CastGroup.Web.Models;
using CastGroup.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CastGroup.Web.Pages.Accounts
{
    public class DeleteModel : PageModel
    {
        private readonly ConsumeCastGroupApis _service;

        public DeleteModel(ConsumeCastGroupApis service)
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
            else
            {
                Account = account;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var account = await _service.GetAccount(id.Value);

            if (account != null)
            {
                Account = account;
                await _service.DeleteAccount(Account.Id);
            }

            return RedirectToPage("./Index");
        }
    }
}
