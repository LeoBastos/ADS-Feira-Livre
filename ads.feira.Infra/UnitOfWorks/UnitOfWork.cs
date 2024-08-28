using ads.feira.domain.Interfaces.UnitOfWorks;
using ads.feira.Infra.Context;

namespace ads.feira.Infra.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Commit()
        {

            var success = await _context.SaveChangesAsync() > 0;
            return success;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public Task Rollback()
        {
            return Task.CompletedTask;
        }
    }
}
