using ads.feira.application.CQRS.Categories.Commands;
using ads.feira.application.CQRS.Stores.Commands;
using ads.feira.domain.Entity.Categories;
using ads.feira.domain.Entity.Stores;
using ads.feira.domain.Interfaces.Categories;
using ads.feira.domain.Interfaces.Stores;
using ads.feira.domain.Interfaces.UnitOfWorks;
using MediatR;

namespace ads.feira.application.CQRS.Stores.Handlers.Commands
{
    public class StoreRemoveCommandHandler : IRequestHandler<StoreRemoveCommand, Store>
    {
        private readonly IStoreRepository _storeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public StoreRemoveCommandHandler(IStoreRepository storeRepository, IUnitOfWork unitOfWork)
        {
            _storeRepository = storeRepository ?? throw new ArgumentNullException(nameof(storeRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Store> Handle(StoreRemoveCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var store = await _storeRepository.GetByIdAsync(request.Id);

                if (store == null)
                {
                    throw new InvalidOperationException($"Store with ID {request.Id} not found.");
                }

                store.Remove();

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
