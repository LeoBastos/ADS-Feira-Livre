using ads.feira.domain.Entity.Stores;
using MediatR;

namespace ads.feira.application.CQRS.Stores.Commands
{
    public class StoreRemoveCommand : IRequest<Store>
    {
        public int Id { get; set; }

        public StoreRemoveCommand(int id)
        {
            Id = id;
        }
    }
}
