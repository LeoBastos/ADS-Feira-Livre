using ads.feira.domain.Entity.Cupons;
using MediatR;

namespace ads.feira.application.CQRS.Cupons.Commands
{
    public class AddStoreToCuponCommand : IRequest<Cupon>
    {
        public int CuponId { get; set; }
        public int StoreId { get; set; }

        public AddStoreToCuponCommand(int cuponId, int storeId)
        {
            CuponId = cuponId;
            StoreId = storeId;
        }
    }
}
