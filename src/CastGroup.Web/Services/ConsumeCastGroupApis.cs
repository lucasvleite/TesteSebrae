using CastGroup.Web.Models;

namespace CastGroup.Web.Services
{
    public class ConsumeCastGroupApis
    {
        public async Task<AccountResponse> GetAccount(Guid id)
        {
            return new();
        }

        public async Task<IEnumerable<AccountResponse>> GetAllAccount()
        {
            List<AccountResponse> result = new();
            return result;
        }

        public async Task<AccountResponse> CreateAccount(AccountResponse account)
        {
            return new();
        }

        public async Task<AccountResponse> DeleteAccount(Guid id)
        {
            return new();
        }

        public async Task<AccountResponse> EditAccount(Guid id, AccountResponse account)
        {
            return new();
        }

        public async Task<AccountResponse> EditAccount(AccountResponse account)
        {
            return new();
        }
    }
}
