using ads.feira.application.CQRS.Accounts.Commands;
using ads.feira.domain.Entity.Accounts;
using ads.feira.domain.Interfaces.Accounts;
using ads.feira.domain.Interfaces.UnitOfWorks;
using MediatR;

namespace ads.feira.application.CQRS.Accounts.Handlers.Commands
{
    public class CognitoUserRemoveCommandHandler : IRequestHandler<CognitoUserRemoveCommand, CognitoUser>
    {
        private readonly ICognitoUserRepository _context;
        private readonly IUnitOfWork _unitOfWork;

        public CognitoUserRemoveCommandHandler(ICognitoUserRepository context, IUnitOfWork unitOfWork)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<CognitoUser> Handle(CognitoUserRemoveCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _context.GetByIdAsync(request.Id);

                if (user == null)
                {
                    throw new InvalidOperationException($"Product with ID {request.Id} not found.");
                }

                user.Remove();

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
