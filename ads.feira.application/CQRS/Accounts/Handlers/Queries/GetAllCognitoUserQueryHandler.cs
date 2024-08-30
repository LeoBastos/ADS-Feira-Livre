using ads.feira.application.CQRS.Accounts.Queries;
using ads.feira.domain.Entity.Accounts;
using ads.feira.domain.Interfaces.Accounts;
using MediatR;

namespace ads.feira.application.CQRS.Accounts.Handlers.Queries
{
    public class GetAllCognitoUserQueryHandler : IRequestHandler<GetAllCognitoUserQuery, IEnumerable<CognitoUser>>
    {
        private readonly ICognitoUserRepository _context;

        public GetAllCognitoUserQueryHandler(ICognitoUserRepository context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CognitoUser>> Handle(GetAllCognitoUserQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.GetAllAsync();
        }
    }
}
