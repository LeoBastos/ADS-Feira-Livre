using ads.feira.domain.Paginated;
using System.Linq.Expressions;

namespace ads.feira.domain.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        #region Querys
        Task<PagedResult<TEntity>> GetAllAsync(int pageNumber, int pageSize);
        Task<TEntity> GetByIdAsync(string id);       
        #endregion

        #region Commands
        Task<TEntity> CreateAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<TEntity> RemoveAsync(TEntity entity);
        #endregion
    }
}
