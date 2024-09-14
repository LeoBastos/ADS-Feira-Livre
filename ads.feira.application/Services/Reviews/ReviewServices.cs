using ads.feira.application.CQRS.Categories.Queries;
using ads.feira.application.CQRS.Reviews.Commands;
using ads.feira.application.CQRS.Reviews.Queries;
using ads.feira.application.DTO.Categories;
using ads.feira.application.DTO.Reviews;
using ads.feira.application.Interfaces.Reviews;
using ads.feira.domain.Interfaces.Reviews;
using ads.feira.domain.Paginated;
using AutoMapper;
using MediatR;

namespace ads.feira.application.Services.Reviews
{
    public class ReviewServices : IReviewServices
    {
        private readonly IReviewRepository _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public ReviewServices(IReviewRepository context, IMapper mapper, IMediator mediator)
        {
            _context = context;
            _mapper = mapper;
            _mediator = mediator;
        }

        #region Queries

        /// <summary>
        /// Busca Review por Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Retorna uma LINQ Expression com um review</returns>
        public async Task<ReviewDTO> GetById(string id)
        {
            var reviewQuery = new GetReviewByIdQuery(id);
            var result = await _mediator.Send(reviewQuery);

            if (result == null)
                throw new NullReferenceException("Review não encontrado com o id fornecido.");

            return _mapper.Map<ReviewDTO>(result);
        }

        /// <summary>
        /// Retorna todas Reviews
        /// </summary>      
        /// <returns>Retorna uma LINQ Expression com todas reviews</returns>
        public async Task<PagedResult<ReviewDTO>> GetAll(int pageNumber, int pageSize)
        {
            var query = new GetAllReviewQuery
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            var result = await _mediator.Send(query);

            var dtos = _mapper.Map<IEnumerable<ReviewDTO>>(result.Items);

            return new PagedResult<ReviewDTO>
            {
                Items = dtos,
                TotalItems = result.TotalItems,
                PageNumber = result.PageNumber,
                PageSize = result.PageSize,
                TotalPages = result.TotalPages
            };

            //var reviewQuery = new GetAllReviewQuery();
            //var result = await _mediator.Send(reviewQuery);
            //return _mapper.Map<IEnumerable<ReviewDTO>>(result);
        }

        #endregion

        #region Commands

        /// <summary>
        /// Add a Entity
        /// </summary>
        /// <param name="entity">Review</param>
        /// <returns></returns>
        public async Task Create(CreateReviewDTO reviewDTO)
        {
            var reviewCreateCommand = _mapper.Map<ReviewCreateCommand>(reviewDTO);

            if (reviewCreateCommand == null)
            {
                throw new Exception("Entity could not be created.");
            }

            var createdProduct = await _mediator.Send(reviewCreateCommand);
        }

        /// <summary>
        /// Update a Entity
        /// </summary>
        /// <param name="entity">Review</param>
        public async Task Update(UpdateReviewDTO reviewDTO)
        {
            var reviewUpdateCommand = _mapper.Map<ReviewUpdateCommand>(reviewDTO);

            if (reviewUpdateCommand == null)
            {
                throw new Exception("Entity could not be updated.");
            }

            await _mediator.Send(reviewUpdateCommand);
        }

        /// <summary>
        /// Remove a Entity
        /// </summary>
        /// <param name="entity">Review</param>
        public async Task Remove(string id)
        {
            var reviewtRemoveCommand = new ReviewRemoveCommand(id);
            await _mediator.Send(reviewtRemoveCommand);
        }

        #endregion
    }
}
