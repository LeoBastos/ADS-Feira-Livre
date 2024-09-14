using ads.feira.domain.Entity.Stores;
using MediatR;

namespace ads.feira.application.CQRS.Stores.Commands
{
    public class StoreRemoveCommand : IRequest<Store>
    {
        public string Id { get; set; }

        public StoreRemoveCommand(string id)
        {
            Id = id;
        }
    }
}
