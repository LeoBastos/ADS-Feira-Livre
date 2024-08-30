using ads.feira.domain.Entity.Cupons;
using MediatR;

namespace ads.feira.application.CQRS.Cupons.Commands
{
    public class CuponRemoveCommand : IRequest<Cupon>
    {
        public int Id { get; set; }

        public CuponRemoveCommand(int id)
        {
            Id = id;
        }
    }
}
