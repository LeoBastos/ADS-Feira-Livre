using ads.feira.application.CQRS.Accounts.Commands;
using ads.feira.domain.Entity.Accounts;
using ads.feira.domain.Interfaces.Accounts;
using ads.feira.domain.Interfaces.UnitOfWorks;
using MediatR;

namespace ads.feira.application.CQRS.Accounts.Handlers.Commands
{
    public class CognitoUserUpdateCommandHandler : IRequestHandler<CognitoUserUpdateCommand, CognitoUser>
    {
        private readonly ICognitoUserRepository _context;
        private readonly IUnitOfWork _unitOfWork;

        public CognitoUserUpdateCommandHandler(ICognitoUserRepository context, IUnitOfWork unitOfWork)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<CognitoUser> Handle(CognitoUserUpdateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _context.GetByIdAsync(request.Id);

                if (user == null)
                {
                    throw new InvalidOperationException($"Product with ID {request.Id} not found.");
                }

                user.Update(request.Id, request.Email, request.Name, request.Description, request.Assets, request.TosAccept, request.PrivacyAccept, request.Roles);

                await _context.UpdateAsync(user);
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
