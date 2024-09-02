using ads.feira.application.CQRS.Stores.Queries;
using ads.feira.domain.Entity.Stores;
using ads.feira.domain.Interfaces.Stores;
using MediatR;

namespace ads.feira.application.CQRS.Stores.Handlers.Queries
{
    public class GetAllStoresQueryHandler : IRequestHandler<GetAllStoreQuery, IEnumerable<Store>>
    {
        private readonly IStoreRepository _storeRepository;

        public GetAllStoresQueryHandler(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }

        public async Task<IEnumerable<Store>> Handle(GetAllStoreQuery request,
            CancellationToken cancellationToken)
        {
            return await _storeRepository.GetAllAsync();
        }
    }
}


