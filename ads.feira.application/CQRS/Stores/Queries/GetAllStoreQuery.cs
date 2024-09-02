using ads.feira.domain.Entity.Stores;
using MediatR;

namespace ads.feira.application.CQRS.Stores.Queries
{
    public class GetAllStoreQuery : IRequest<IEnumerable<Store>>
    {

    }
}
