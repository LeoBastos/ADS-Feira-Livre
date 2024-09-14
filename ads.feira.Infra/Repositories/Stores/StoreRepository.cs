using ads.feira.domain.Entity.Reviews;
using ads.feira.domain.Entity.Stores;
using ads.feira.domain.Interfaces.Stores;
using ads.feira.domain.Paginated;
using ads.feira.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace ads.feira.Infra.Repositories.Stores
{
    public class StoreRepository : IStoreRepository
    {
        private readonly ApplicationDbContext _context;

        public StoreRepository(ApplicationDbContext context)
        {
            _context = context;  
        }

        #region Queries
        /// <summary>
        /// Busca Loja por Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Retorna uma LINQ Expression com um Loja</returns>
        public async Task<Store> GetByIdAsync(string id)
        {
            return await _context.Stores
                    .AsNoTracking()
                    .Where(p => p.IsActive == true)
                    .FirstOrDefaultAsync(t => t.Id == id);
        }


        /// <summary>
        /// Retorna todos os Lojas
        /// </summary>      
        /// <returns>Retorna uma LINQ Expression com todos os lojas</returns>
        public async Task<PagedResult<Store>> GetAllAsync(int pageNumber, int pageSize)
        {
            var query = _context.Stores
                .Where(p => p.IsActive == true)
                .AsNoTracking()
                .OrderBy(t => t.Name);

            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Store>
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
        /// <param name="entity">Lojas</param>
        /// <returns></returns>
        public async Task<Store> CreateAsync(Store entity)
        {
            _context.Add(entity);
            return entity;
        }
        /// <summary>
        /// Update a Entity
        /// </summary>
        /// <param name="entity">Lojas</param>
        /// <returns></returns>
        public async Task<Store> UpdateAsync(Store entity)
        {
            _context.Update(entity);
            return entity;
        }
        /// <summary>
        /// Remove a Entity
        /// </summary>
        /// <param name="entity">Lojas</param>
        /// <returns></returns>
        public async Task<Store> RemoveAsync(Store entity)
        {
            _context.Remove(entity);
            return entity;
        }
        #endregion
    }
}
