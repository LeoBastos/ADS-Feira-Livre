using ads.feira.domain.Entity.Reviews;
using ads.feira.domain.Interfaces.Reviews;
using ads.feira.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ads.feira.Infra.Repositories.Reviews
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ApplicationDbContext _context;

        public ReviewRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        #region Queries
        /// <summary>
        /// Busca Review por Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Retorna uma LINQ Expression com um Review</returns>
        public async Task<Review> GetByIdAsync(int id)
        {
            return await _context.Reviews
                    .AsNoTracking()
                    .Where(p => p.IsActive == true)
                    .FirstOrDefaultAsync(t => t.Id == id);
        }

        /// <summary>
        /// Retorna todos os Reviews
        /// </summary>      
        /// <returns>Retorna uma LINQ Expression com todos os reviews</returns>
        public async Task<IEnumerable<Review>> GetAllAsync()
        {
            return await _context.Reviews
                    .AsNoTracking()
                    .OrderBy(t => t.Rate)
                    .Where(p => p.IsActive == true)
                    .ToListAsync();
        }

        #endregion

        #region Commands
        /// <summary>
        /// Add a Entity
        /// </summary>
        /// <param name="entity">Review</param>
        /// <returns></returns>
        public async Task<Review> CreateAsync(Review entity)
        {
            _context.Add(entity);
            return entity;
        }

        /// <summary>
        /// Update a Entity
        /// </summary>
        /// <param name="entity">Review</param>
        /// <returns></returns>
        public async Task<Review> UpdateAsync(Review entity)
        {
            _context.Update(entity);
            return entity;
        }

        /// <summary>
        /// Remove a Entity
        /// </summary>
        /// <param name="entity">Review</param>
        /// <returns></returns>
        public async Task<Review> RemoveAsync(Review entity)
        {
            _context.Remove(entity);
            return entity;
        }
        #endregion
    }
}
