using ads.feira.domain.Entity.Stores;
using MediatR;

namespace ads.feira.application.CQRS.Stores.Queries
{
    public class GetStoreByIdQuery : IRequest<Store>
    {
        public int Id { get; set; }

        public GetStoreByIdQuery(int id)
        {
            Id = id;
        }
    }
}
