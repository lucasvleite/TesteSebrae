using CastGroup.Web.Models;
using CastGroup.Web.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CastGroup.Web.Pages.Accounts
{
    public class IndexModel : PageModel
    {
        private readonly ConsumeCastGroupApis _service;

        public IndexModel(ConsumeCastGroupApis service)
        {
            _service = service;
        }

        public IList<AccountResponse> Account { get; set; } = default!;

        public async Task OnGetAsync()
        {
            var result = await _service.GetAllAccount();
            Account = result.ToList();
        }
    }
}
