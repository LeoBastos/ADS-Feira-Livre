using ads.feira.domain.Entity.Reviews;
using MediatR;

namespace ads.feira.application.CQRS.Reviews.Commands
{
    public class ReviewRemoveCommand : IRequest<Review>
    {
        public int Id { get; set; }

        public ReviewRemoveCommand(int id)
        {
            Id = id;
        }
    }
}
