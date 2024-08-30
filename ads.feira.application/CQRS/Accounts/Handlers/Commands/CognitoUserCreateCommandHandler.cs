using ads.feira.application.CQRS.Accounts.Commands;
using ads.feira.domain.Entity.Accounts;
using ads.feira.domain.Interfaces.Accounts;
using ads.feira.domain.Interfaces.UnitOfWorks;
using MediatR;

namespace ads.feira.application.CQRS.Accounts.Handlers.Commands
{
    public class CognitoUserCreateCommandHandler : IRequestHandler<CognitoUserCreateCommand, CognitoUser>
    {
        private readonly ICognitoUserRepository _context;
        private readonly IUnitOfWork _unitOfWork;

        public CognitoUserCreateCommandHandler(ICognitoUserRepository context, IUnitOfWork unitOfWork)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<CognitoUser> Handle(CognitoUserCreateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = CognitoUser.Create(request.Id, request.Email, request.Name, request?.Description, request?.Assets, request.TosAccept, request.PrivacyAccept, request?.Roles);

                if (user == null)
                {
                    throw new InvalidOperationException("Failed to create product.");
                }

                await _context.CreateAsync(user);
                await _unitOfWork.Commit();

                return user;
            }
            catch (Exception)
            {
                await _unitOfWork.Rollback();
                throw;
            }
        }
    }
}
