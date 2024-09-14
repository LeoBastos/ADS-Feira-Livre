using ads.feira.domain.Entity.Cupons;
using ads.feira.domain.Entity.Reviews;
using ads.feira.domain.Interfaces.Reviews;
using ads.feira.domain.Paginated;
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
        public async Task<Review> GetByIdAsync(string id)
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
        public async Task<PagedResult<Review>> GetAllAsync(int pageNumber, int pageSize)
        {
            var query = _context.Reviews
                .Where(p => p.IsActive == true)
                .AsNoTracking()
                .OrderBy(t => t.Rate);

            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Review>
            {
                Items = items,
                TotalItems = totalItems,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages
            };
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
