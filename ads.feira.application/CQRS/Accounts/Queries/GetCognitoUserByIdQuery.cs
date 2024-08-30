using ads.feira.domain.Entity.Accounts;
using MediatR;

namespace ads.feira.application.CQRS.Accounts.Queries
{
    public class GetCognitoUserByIdQuery : IRequest<CognitoUser>
    {
        public Guid Id { get; set; }

        public GetCognitoUserByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
