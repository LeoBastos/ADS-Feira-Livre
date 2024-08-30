using ads.feira.domain.Entity.Accounts;
using MediatR;

namespace ads.feira.application.CQRS.Accounts.Commands
{
    public class CognitoUserRemoveCommand : IRequest<CognitoUser>
    {
        public Guid Id { get; set; }

        public CognitoUserRemoveCommand(Guid id)
        {
            Id = id;
        }
    }
}
