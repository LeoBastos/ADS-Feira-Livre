using ads.feira.domain.Entity.Cupons;
using MediatR;

namespace ads.feira.application.CQRS.Cupons.Commands
{
    public class RemoveProductFromCuponCommand : IRequest<Cupon>
    {
        public string CuponId { get; set; }
        public string ProductId { get; set; }

        public RemoveProductFromCuponCommand(string cuponId, string productId)
        {
            CuponId = cuponId;
            ProductId = productId;
        }
    }
}
