using ads.feira.application.CQRS.Cupons.Commands;
using ads.feira.domain.Entity.Cupons;
using ads.feira.domain.Interfaces.Cupons;
using ads.feira.domain.Interfaces.UnitOfWorks;
using MediatR;

namespace ads.feira.application.CQRS.Cupons.Handlers.Commands
{
    public class CuponCreateCommandHandler : IRequestHandler<CuponCreateCommand, Cupon>
    {
        private readonly ICuponRepository _cuponRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CuponCreateCommandHandler(ICuponRepository cuponRepository, IUnitOfWork unitOfWork)
        {
            _cuponRepository = cuponRepository ?? throw new ArgumentNullException(nameof(cuponRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Cupon> Handle(CuponCreateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var cupon = Cupon.Create(request.Id, request.Name, request.Code, request.Description, request.Expiration, request.Discount, request.DiscountType);

                if (cupon == null)
                {
                    throw new InvalidOperationException("Failed to create cupon.");
                }

                await _cuponRepository.CreateAsync(cupon);
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
