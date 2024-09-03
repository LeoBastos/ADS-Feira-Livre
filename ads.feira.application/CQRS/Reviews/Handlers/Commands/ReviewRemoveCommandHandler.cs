using ads.feira.application.CQRS.Reviews.Commands;
using ads.feira.domain.Entity.Reviews;
using ads.feira.domain.Interfaces.Reviews;
using ads.feira.domain.Interfaces.UnitOfWorks;
using MediatR;

namespace ads.feira.application.CQRS.Reviews.Handlers.Commands
{
    public class ReviewRemoveCommandHandler : IRequestHandler<ReviewRemoveCommand, Review>
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ReviewRemoveCommandHandler(IReviewRepository reviewRepository, IUnitOfWork unitOfWork)
        {
            _reviewRepository = reviewRepository ?? throw new ArgumentNullException(nameof(reviewRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Review> Handle(ReviewRemoveCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var review = await _reviewRepository.GetByIdAsync(request.Id);

                if (review == null)
                {
                    throw new InvalidOperationException($"Category with ID {request.Id} not found.");
                }

                review.Remove();

                await _reviewRepository.UpdateAsync(review);
                await _unitOfWork.Commit();

                return review;
            }
            catch (Exception)
            {
                await _unitOfWork.Rollback();
                throw;
            }
        }
    }
}
