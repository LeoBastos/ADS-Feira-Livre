using ads.feira.application.CQRS.Cupons.Commands;
using ads.feira.domain.Entity.Cupons;
using ads.feira.domain.Interfaces.Cupons;
using ads.feira.domain.Interfaces.UnitOfWorks;
using MediatR;

namespace ads.feira.application.CQRS.Cupons.Handlers.Commands
{
    public class CuponUpdateCommandHandler : IRequestHandler<CuponUpdateCommand, Cupon>
    {
        private readonly ICuponRepository _cuponRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CuponUpdateCommandHandler(ICuponRepository cuponRepository, IUnitOfWork unitOfWork)
        {
            _cuponRepository = cuponRepository ?? throw new ArgumentNullException(nameof(cuponRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Cupon> Handle(CuponUpdateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var cupon = await _cuponRepository.GetByIdAsync(request.Id);

                if (cupon == null)
                {
                    throw new InvalidOperationException($"Category with ID {request.Id} not found.");
                }

                cupon.Update(request.Id, request.Name, request.Code, request.Description, request.Expiration, request.Discount, request.DiscountType);

                await _cuponRepository.UpdateAsync(cupon);
                await _unitOfWork.Commit();

                return cupon;
            }
            catch (Exception)
            {
                await _unitOfWork.Rollback();
                throw;
            }
        }
    }
}
