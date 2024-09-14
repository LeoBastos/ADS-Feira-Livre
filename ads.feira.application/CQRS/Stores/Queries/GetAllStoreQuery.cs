using ads.feira.domain.Entity.Stores;
using ads.feira.domain.Paginated;
using MediatR;

namespace ads.feira.application.CQRS.Stores.Queries
{
    public class GetAllStoreQuery : IRequest<PagedResult<Store>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
