using ads.feira.application.CQRS.Stores.Queries;
using ads.feira.domain.Entity.Stores;
using ads.feira.domain.Interfaces.Stores;
using ads.feira.domain.Paginated;
using MediatR;

namespace ads.feira.application.CQRS.Stores.Handlers.Queries
{
    public class GetAllStoresQueryHandler : IRequestHandler<GetAllStoreQuery, PagedResult<Store>>
    {
        private readonly IStoreRepository _storeRepository;

        public GetAllStoresQueryHandler(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }

        public async Task<PagedResult<Store>> Handle(GetAllStoreQuery request,
            CancellationToken cancellationToken)
        {
            return await _storeRepository.GetAllAsync(request.PageNumber, request.PageSize);
        }
    }
}


