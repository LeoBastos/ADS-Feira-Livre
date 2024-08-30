using ads.feira.domain.Entity.Accounts;
using MediatR;

namespace ads.feira.application.CQRS.Accounts.Queries
{
    public class GetAllCognitoUserQuery : IRequest<IEnumerable<CognitoUser>>
    {
    }
}
