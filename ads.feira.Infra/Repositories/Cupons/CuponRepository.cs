using ads.feira.domain.Entity.Cupons;
using ads.feira.domain.Interfaces.Cupons;
using ads.feira.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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
        public async Task<Cupon> GetByIdAsync(int id)
        {
            return await _context.Cupons
                   .AsNoTracking()
                   .FirstOrDefaultAsync(t => t.Id == id);
        }

        /// <summary>
        /// Filtra Cupon por Predicado
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>Retorna uma LINQ Expression com predicado por cupon</returns>
        public async Task<IEnumerable<Cupon>> Find(Expression<Func<Cupon, bool>> predicate)
        {
            return await _context.Cupons
               .Where(predicate)
               .ToListAsync();
        }

        /// <summary>
        /// Retorna todos os cupons
        /// </summary>      
        /// <returns>Retorna uma LINQ Expression com todos os cupons</returns>
        public async Task<IEnumerable<Cupon>> GetAllAsync()
        {
            return await _context.Cupons
               .AsNoTracking()
               .OrderBy(t => t.Name)
               .ToListAsync();
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
