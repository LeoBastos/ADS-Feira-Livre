using ads.feira.domain.Entity.Reviews;
using MediatR;

namespace ads.feira.application.CQRS.Reviews.Commands
{
    public class ReviewRemoveCommand : IRequest<Review>
    {
        public string Id { get; set; }

        public ReviewRemoveCommand(string id)
        {
            Id = id;
        }
    }
}
