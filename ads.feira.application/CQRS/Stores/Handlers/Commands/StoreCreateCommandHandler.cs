using ads.feira.application.CQRS.Stores.Commands;
using ads.feira.domain.Entity.Stores;
using ads.feira.domain.Interfaces.Stores;
using ads.feira.domain.Interfaces.UnitOfWorks;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ads.feira.application.CQRS.Stores.Handlers.Commands
{
    public class StoreCreateCommandHandler : IRequestHandler<StoreCreateCommand, Store>
    {
        private readonly IStoreRepository _storeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public StoreCreateCommandHandler(IStoreRepository storeRepository, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _storeRepository = storeRepository ?? throw new ArgumentNullException(nameof(storeRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public async Task<Store> Handle(StoreCreateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid userId))
                {
                    throw new InvalidOperationException("User ID not found or invalid.");
                }


                if (request.StoreOwnerId.Equals(userId.ToString()))
                {
                    throw new InvalidOperationException("Only one Store for Owner.");
                }



                var store = Store.Create(request.Id, request.StoreOwnerId, request.Name, request.CategoryId,
                                        request.Description, request.Assets, request.StoreNumber, request.HasDebt, request.Locations);

                if (store == null)
                {
                    throw new InvalidOperationException("Failed to create Store.");
                }

                if (store.HasDebt)
                    throw new InvalidOperationException("Has Debt.");

                await _storeRepository.CreateAsync(store);
                await _unitOfWork.Commit();

                return store;
            }
            catch (Exception)
            {
                await _unitOfWork.Rollback();
                throw;
            }
        }
    }
}
