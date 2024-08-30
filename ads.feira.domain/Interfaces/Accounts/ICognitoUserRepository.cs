using ads.feira.domain.Entity.Accounts;
using System.Linq.Expressions;

namespace ads.feira.domain.Interfaces.Accounts
{
    public interface ICognitoUserRepository
    {
        #region Querys
        Task<IEnumerable<CognitoUser>> GetAllAsync();
        Task<CognitoUser> GetByIdAsync(Guid id);
        Task<IEnumerable<CognitoUser>> Find(Expression<Func<CognitoUser, bool>> predicate);
        #endregion

        #region Commands
        Task<CognitoUser> CreateAsync(CognitoUser entity);
        Task<CognitoUser> UpdateAsync(CognitoUser entity);
        Task<CognitoUser> RemoveAsync(CognitoUser entity);
        #endregion
    }
}
