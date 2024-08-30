using ads.feira.application.DTO.Accounts;
using ads.feira.domain.Entity.Accounts;
using MediatR;
using System.Linq.Expressions;

namespace ads.feira.application.CQRS.Accounts.Queries
{
    public class FindCognitoUserQuery : IRequest<IEnumerable<CognitoUser>>
    {
        public Expression<Func<CognitoUserDTO, bool>> Predicate { get; }

        public FindCognitoUserQuery(Expression<Func<CognitoUserDTO, bool>> predicate)
        {
            Predicate = predicate;
        }
    }
}
