using ads.feira.domain.Entity.Cupons;
using MediatR;

namespace ads.feira.application.CQRS.Cupons.Commands
{
    public class AddProductToCuponCommand : IRequest<Cupon>
    {
        public int CuponId { get; set; }
        public int ProductId { get; set; }

        public AddProductToCuponCommand(int cuponId, int productId)
        {
            CuponId = cuponId;
            ProductId = productId;
        }
    }
}
