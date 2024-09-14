using ads.feira.domain.Entity.Cupons;
using MediatR;

namespace ads.feira.application.CQRS.Cupons.Commands
{
    public class AddStoreToCuponCommand : IRequest<Cupon>
    {
        public string CuponId { get; set; }
        public string StoreId { get; set; }

        public AddStoreToCuponCommand(string cuponId, string storeId)
        {
            CuponId = cuponId;
            StoreId = storeId;
        }
    }
}
