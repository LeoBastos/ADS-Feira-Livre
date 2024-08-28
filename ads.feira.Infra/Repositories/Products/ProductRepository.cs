﻿using ads.feira.domain.Entity.Products;
using ads.feira.domain.Interfaces.Products;
using ads.feira.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ads.feira.Infra.Repositories.Products
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        #region Queries
        /// <summary>
        /// Busca Produto por Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Retorna uma LINQ Expression com um Produto</returns>
        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products
                   .AsNoTracking()
                   .FirstOrDefaultAsync(t => t.Id == id);
        }

        /// <summary>
        /// Filtra Produto por Predicado
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>Retorna uma LINQ Expression com predicado por produto</returns>
        public async Task<IEnumerable<Product>> Find(Expression<Func<Product, bool>> predicate)
        {
            return await _context.Products
                .AsNoTracking()
                .Where(predicate)
                .ToListAsync();
        }

        /// <summary>
        /// Retorna todos os Produtos
        /// </summary>      
        /// <returns>Retorna uma LINQ Expression com todos os produtos</returns>
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products
               .AsNoTracking()
               .OrderBy(t => t.Name)
               .ToListAsync();
        }

        #endregion

        #region Commands
        /// <summary>
        /// Add a Entity
        /// </summary>
        /// <param name="entity">Produto</param>
        /// <returns></returns>
        public async Task<Product> CreateAsync(Product entity)
        {
            _context.Add(entity);
            return entity;
        }

        /// <summary>
        /// Update a Entity
        /// </summary>
        /// <param name="entity">Produto</param>
        /// <returns></returns>
        public async Task<Product> UpdateAsync(Product entity)
        {
            _context.Update(entity);
            return entity;
        }

        /// <summary>
        /// Remove a Entity
        /// </summary>
        /// <param name="entity">Produto</param>
        /// <returns></returns>
        public async Task<Product> RemoveAsync(Product entity)
        {
            _context.Remove(entity);
            return entity;
        }
        #endregion
    }
}
