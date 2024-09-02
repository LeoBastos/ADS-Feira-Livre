using ads.feira.application.CQRS.Stores.Commands;
using ads.feira.domain.Entity.Stores;
using ads.feira.domain.Interfaces.Categories;
using ads.feira.domain.Interfaces.Stores;
using ads.feira.domain.Interfaces.UnitOfWorks;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ads.feira.application.CQRS.Stores.Handlers.Commands
{
    public class StoreUpdateCommandHandler : IRequestHandler<StoreUpdateCommand, Store>
    {
        private readonly IStoreRepository _storeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public StoreUpdateCommandHandler(IStoreRepository storeRepository, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _storeRepository = storeRepository ?? throw new ArgumentNullException(nameof(storeRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public async Task<Store> Handle(StoreUpdateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var store = await _storeRepository.GetByIdAsync(request.Id);

                if (store == null)
                {
                    throw new InvalidOperationException($"Store with ID {request.Id} not found.");
                }

                if(store.HasDebt)
                    throw new InvalidOperationException($"Store hasDebt.");

                var userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid userId))
                {
                    throw new InvalidOperationException("User ID not found or invalid.");
                }


                store.Update(request.Id, request.StoreOwnerId, request.Name, request.CategoryId, request.Description, request.Assets, 
                    request.StoreNumber, request.HasDebt, request.Locations);

                await _storeRepository.UpdateAsync(store);
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
