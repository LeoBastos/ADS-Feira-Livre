using ads.feira.domain.Entity.Identity;
using ads.feira.domain.Interfaces.Identities;
using ads.feira.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ads.feira.Infra.Repositories.Identities
{
    public class CognitoUserRepository : ICognitoUserRepository
    {
        private readonly ApplicationDbContext _context;

        public CognitoUserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CognitoUser> GetByIdAsync(int id)
        {
            return await _context.Users
                  .AsNoTracking()
                  .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<CognitoUser>> Find(Expression<Func<CognitoUser, bool>> predicate)
        {
            return await _context.Users
              .Where(predicate)
              .ToListAsync();
        }

        public async Task<IEnumerable<CognitoUser>> GetAllAsync()
        {
            return await _context.Users
              .AsNoTracking()
              .OrderBy(t => t.Name)
              .ToListAsync();
        }

        public async Task<CognitoUser> CreateAsync(CognitoUser entity)
        {
            _context.Add(entity);
            return entity;
        }

        public async Task<CognitoUser> UpdateAsync(CognitoUser entity)
        {
            _context.Update(entity);
            return entity;
        }

        public async Task<CognitoUser> RemoveAsync(CognitoUser entity)
        {
            _context.Remove(entity);
            return entity;
        }
    }
}
