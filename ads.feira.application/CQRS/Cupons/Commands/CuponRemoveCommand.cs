using ads.feira.domain.Entity.Cupons;
using MediatR;

namespace ads.feira.application.CQRS.Cupons.Commands
{
    public class CuponRemoveCommand : IRequest<Cupon>
    {
        public string Id { get; set; }

        public CuponRemoveCommand(string id)
        {
            Id = id;
        }
    }
}
