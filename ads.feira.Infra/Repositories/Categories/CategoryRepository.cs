using ads.feira.domain.Entity.Categories;
using ads.feira.domain.Interfaces.Categories;
using ads.feira.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ads.feira.Infra.Repositories.Categories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        #region Queries

        /// <summary>
        /// Busca Categoria por Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Retorna uma LINQ Expression com um categoria</returns>
        public async Task<Category> GetByIdAsync(int id)
        {
            return await _context.Categories
                    .Include(c => c.Products)
                    .Include(c => c.Stores)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(t => t.Id == id);
        }

        /// <summary>
        /// Filtra Categoria por Predicado
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>Retorna uma LINQ Expression com predicado por categoria</returns>
        public async Task<IEnumerable<Category>> Find(Expression<Func<Category, bool>> predicate)
        {
            return await _context.Categories
                .Where(predicate)
                .ToListAsync();
        }

        /// <summary>
        /// Retorna todas categorias
        /// </summary>      
        /// <returns>Retorna uma LINQ Expression com todas categorias</returns>
        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories
                .Include(c => c.Products)
                .Include(c => c.Stores)
               .AsNoTracking()
               .OrderBy(t => t.Name)
               .ToListAsync();
        }

        #endregion

        #region Commands

        /// <summary>
        /// Add a Entity
        /// </summary>
        /// <param name="entity">Category</param>
        /// <returns></returns>
        public async Task<Category> CreateAsync(Category entity)
        {
            _context.Add(entity);
            return entity;
        }

        /// <summary>
        /// Update a Entity
        /// </summary>
        /// <param name="entity">Category</param>
        public async Task<Category> UpdateAsync(Category entity)
        {
            _context.Update(entity);

            return entity;
        }

        /// <summary>
        /// Remove a Entity
        /// </summary>
        /// <param name="entity">Category</param>
        public async Task<Category> RemoveAsync(Category entity)
        {
            _context.Remove(entity);           
            return entity;
        }

        #endregion
    }
}
