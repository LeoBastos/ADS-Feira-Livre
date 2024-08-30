using ads.feira.application.CQRS.Accounts.Queries;
using ads.feira.domain.Entity.Accounts;
using ads.feira.domain.Interfaces.Accounts;
using AutoMapper;
using MediatR;
using System.Linq.Expressions;

namespace ads.feira.application.CQRS.Accounts.Handlers.Queries
{
    public class FindCognitoUserQueryHandler : IRequestHandler<FindCognitoUserQuery, IEnumerable<CognitoUser>>
    {
        private readonly ICognitoUserRepository _context;
        private readonly IMapper _mapper;

        public FindCognitoUserQueryHandler(ICognitoUserRepository context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CognitoUser>> Handle(FindCognitoUserQuery request, CancellationToken cancellationToken)
        {
            // Convert the DTO predicate to an entity predicate
            var entityPredicate = _mapper.Map<Expression<Func<CognitoUser, bool>>>(request.Predicate);

            // Use the repository to find users based on the predicate
            var users = await _context.Find(entityPredicate);

            return users;
        }
    }
}
