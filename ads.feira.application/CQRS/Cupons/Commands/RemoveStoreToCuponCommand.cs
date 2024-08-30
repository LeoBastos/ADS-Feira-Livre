using ads.feira.domain.Entity.Cupons;
using MediatR;

namespace ads.feira.application.CQRS.Cupons.Commands
{
    public class RemoveStoreFromCuponCommand : IRequest<Cupon>
    {
        public int CuponId { get; set; }
        public int StoreId { get; set; }

        public RemoveStoreFromCuponCommand(int cuponId, int storeId)
        {
            CuponId = cuponId;
            StoreId = storeId;
        }
    }
}
