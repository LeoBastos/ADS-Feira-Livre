using ads.feira.domain.Entity.Cupons;
using MediatR;

namespace ads.feira.application.CQRS.Cupons.Commands
{
    public class RemoveProductFromCuponCommand : IRequest<Cupon>
    {
        public int CuponId { get; set; }
        public int ProductId { get; set; }

        public RemoveProductFromCuponCommand(int cuponId, int productId)
        {
            CuponId = cuponId;
            ProductId = productId;
        }
    }
}
