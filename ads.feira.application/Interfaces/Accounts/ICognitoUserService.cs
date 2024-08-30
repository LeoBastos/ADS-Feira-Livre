using ads.feira.application.DTO.Accounts;
using System.Linq.Expressions;

namespace ads.feira.application.Interfaces.Accounts
{
    public interface ICognitoUserService
    {
        Task<IEnumerable<CognitoUserDTO>> GetAll();
        Task<CognitoUserDTO> GetById(Guid id);
        Task<IEnumerable<CognitoUserDTO>> Find(Expression<Func<CognitoUserDTO, bool>> predicate);

        Task Create(CreateCognitoUserDTO cognitoDTO);
        Task Update(CognitoUserDTO cognitoDTO);
        Task Remove(Guid id);
    }
}
