using ads.feira.domain.Interfaces.Accounts;
using ads.feira.Infra.Context;

namespace ads.feira.Infra.Repositories.Accounts
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationDbContext _context;

        public AccountRepository(ApplicationDbContext context)
        {
            _context = context;
        }


    }
}
