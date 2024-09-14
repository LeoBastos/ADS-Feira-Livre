using ads.feira.domain.Entity.Categories;
using ads.feira.domain.Entity.Cupons;
using ads.feira.domain.Interfaces.Cupons;
using ads.feira.domain.Paginated;
using ads.feira.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace ads.feira.Infra.Repositories.Cupons
{
    public class CuponRepository : ICuponRepository
    {
        private readonly ApplicationDbContext _context;

        public CuponRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        #region Queries
        /// <summary>
        /// Busca Cupon por Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Retorna uma LINQ Expression com um cupon</returns>
        public async Task<Cupon> GetByIdAsync(string id)
        {
            return await _context.Cupons
                    .Where(p => p.IsActive == true)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(t => t.Id == id);
        }

        /// <summary>
        /// Retorna todos os cupons
        /// </summary>      
        /// <returns>Retorna uma LINQ Expression com todos os cupons</returns>
        public async Task<PagedResult<Cupon>> GetAllAsync(int pageNumber, int pageSize)
        {
            var query = _context.Cupons
                .Where(p => p.IsActive == true)
                .AsNoTracking()
                .OrderBy(t => t.Name);

            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Cupon>
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
        /// <param name="entity">Cupon</param>
        /// <returns></returns>
        public async Task<Cupon> CreateAsync(Cupon entity)
        {
            _context.Add(entity);
            return entity;
        }

        /// <summary>
        /// Update a Entity
        /// </summary>
        /// <param name="entity">Cupon</param>
        /// <returns></returns>
        public async Task<Cupon> UpdateAsync(Cupon entity)
        {
            _context.Update(entity);
            return entity;
        }

        /// <summary>
        /// Remove a Entity
        /// </summary>
        /// <param name="entity">Cupon</param>
        /// <returns></returns>
        public async Task<Cupon> RemoveAsync(Cupon entity)
        {
            _context.Remove(entity);
            return entity;
        }

        #endregion

    }
}
