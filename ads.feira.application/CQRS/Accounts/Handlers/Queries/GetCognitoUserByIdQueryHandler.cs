using ads.feira.application.CQRS.Accounts.Queries;
using ads.feira.domain.Entity.Accounts;
using ads.feira.domain.Interfaces.Accounts;
using MediatR;

namespace ads.feira.application.CQRS.Accounts.Handlers.Queries
{
    public class GetCognitoUserByIdQueryHandler : IRequestHandler<GetCognitoUserByIdQuery, CognitoUser>
    {
        private readonly ICognitoUserRepository _context;
        public GetCognitoUserByIdQueryHandler(ICognitoUserRepository context)
        {
            _context = context;
        }

        public async Task<CognitoUser> Handle(GetCognitoUserByIdQuery request,
             CancellationToken cancellationToken)
        {
            return await _context.GetByIdAsync(request.Id);
        }
    }
}
