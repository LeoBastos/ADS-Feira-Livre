using ads.feira.domain.Entity.Accounts;
using ads.feira.domain.Interfaces.Accounts;
using ads.feira.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace ads.feira.Infra.Repositories.Accounts
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationDbContext _context;

        public AccountRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Account> GetByIdAsync(string id)
        {
            return await _context.Accounts
                   .AsNoTracking()   
                   .Where(p => p.Id == id && p.IsActive == true)
                   .FirstOrDefaultAsync();
        }
    }
}

