using ads.feira.domain.Entity.Stores;
using MediatR;

namespace ads.feira.application.CQRS.Stores.Queries
{
    public class GetStoreByIdQuery : IRequest<Store>
    {
        public string Id { get; set; }

        public GetStoreByIdQuery(string id)
        {
            Id = id;
        }
    }
}
