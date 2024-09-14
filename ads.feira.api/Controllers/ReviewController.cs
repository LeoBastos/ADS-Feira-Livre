using ads.feira.application.DTO.Reviews;
using ads.feira.application.Interfaces.Reviews;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace ads.feira.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ReviewController : ControllerBase
    {
        private readonly IReviewServices _reviewServices;        

        public ReviewController(IReviewServices reviewServices)
        {
            _reviewServices = reviewServices;           
        }

        /// <summary>
        /// - Retorna todas as Reviews
        /// </summary>       
        [Authorize]
        [HttpGet]
        [OutputCache(Duration = 15)]
        [ProducesResponseType(typeof(ReviewDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ReviewDTO>>> GetAllReviews([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20)
        {
            var categories = await _reviewServices.GetAll(pageNumber, pageSize);
            return Ok(categories);
        }

        /// <summary>
        /// - Busca um review por Id.
        /// </summary>
        /// <param name="Id"></param>
        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ReviewDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ReviewDTO>> GetReviewById(string id)
        {
            var category = await _reviewServices.GetById(id);
            if (category == null)
                return NotFound();

            return Ok(category);
        }

        /// <summary>
        /// - Cria uma Review
        /// </summary> 
        [Authorize(Roles = "Admin, Customer")]
        [HttpPost]
        [ProducesResponseType(typeof(CreateReviewDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CreateReviewDTO>> CreateReview([FromForm] CreateReviewDTO createReviewDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _reviewServices.Create(createReviewDto);


            return Ok("Review cadastrada.");
        }

        /// <summary>
        /// - Atualiza uma review
        /// </summary> 
        [Authorize(Roles = "Admin, Customer")]
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(UpdateReviewDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateReview(string id, [FromForm] UpdateReviewDTO updateReviewDto)
        {
            if (id == null)
                return BadRequest("ID mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            await _reviewServices.Update(updateReviewDto);

            return Ok("Categoria atualizada");
        }

        /// <summary>
        /// - Remove uma Review
        /// </summary> 
        /// <param name="Id"></param>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveReview(string id)
        {
            await _reviewServices.Remove(id);
            return NoContent();
        }
    }
}
