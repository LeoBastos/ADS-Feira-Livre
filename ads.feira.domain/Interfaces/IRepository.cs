using System.Linq.Expressions;

namespace ads.feira.domain.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        #region Querys
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(int id);       
        #endregion

        #region Commands
        Task<TEntity> CreateAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<TEntity> RemoveAsync(TEntity entity);
        #endregion
    }
}
