using ads.feira.domain.Entity.Reviews;
using MediatR;

namespace ads.feira.application.CQRS.Reviews.Commands
{
    public class ReviewCommand : IRequest<Review>
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string ReviewContent { get; set; }
        public int StoreId { get; set; }
        public int Rate { get; set; }
    }
}
