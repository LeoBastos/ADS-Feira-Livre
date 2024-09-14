using ads.feira.domain.Entity.Accounts;

namespace ads.feira.domain.Interfaces.Accounts
{
    public interface IAccountRepository
    {
        Task<Account> GetByIdAsync(string id);
    }
}
