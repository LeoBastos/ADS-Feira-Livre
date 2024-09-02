using ads.feira.application.CQRS.Stores.Queries;
using ads.feira.domain.Entity.Stores;
using ads.feira.domain.Interfaces.Stores;
using MediatR;

namespace ads.feira.application.CQRS.Stores.Handlers.Queries
{
    public class GetStoreByIdQueryHandler : IRequestHandler<GetStoreByIdQuery, Store>
    {
        private readonly IStoreRepository _storeRepository;
        public GetStoreByIdQueryHandler(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }

        public async Task<Store> Handle(GetStoreByIdQuery request,
             CancellationToken cancellationToken)
        {
            return await _storeRepository.GetByIdAsync(request.Id);
        }
    }
}
