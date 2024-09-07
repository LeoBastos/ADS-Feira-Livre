using ads.feira.api.Models.Reviews;
using ads.feira.application.DTO.Reviews;
using ads.feira.application.Interfaces.Reviews;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ads.feira.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ReviewController : ControllerBase
    {
        private readonly IReviewServices _reviewServices;
        private readonly IMapper _mapper;

        public ReviewController(IReviewServices reviewServices, IMapper mapper)
        {
            _reviewServices = reviewServices;
            _mapper = mapper;
        }

        /// <summary>
        /// - Retorna todas as Reviews
        /// </summary>       
        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(ReviewViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ReviewViewModel>>> GetAllCategories()
        {
            var categories = await _reviewServices.GetAll();
            return Ok(_mapper.Map<IEnumerable<ReviewDTO>>(categories));
        }

        /// <summary>
        /// - Busca um review por Id.
        /// </summary>
        /// <param name="Id"></param>
        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ReviewViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ReviewViewModel>> GetCategoryById(int id)
        {
            var category = await _reviewServices.GetById(id);
            if (category == null)
                return NotFound();

            return Ok(_mapper.Map<ReviewDTO>(category));
        }

        /// <summary>
        /// - Cria uma Review
        /// </summary> 
        [Authorize(Roles = "Admin, Customer")]
        [HttpPost]
        [ProducesResponseType(typeof(CreateReviewViewModel), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CreateReviewViewModel>> CreateCategory([FromForm] CreateReviewViewModel createViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reviewDTO = _mapper.Map<CreateReviewDTO>(createViewModel);
            await _reviewServices.Create(reviewDTO);


            return Ok("Review cadastrada.");
        }

        /// <summary>
        /// - Atualiza uma review
        /// </summary> 
        [Authorize(Roles = "Admin, Customer")]
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(UpdateReviewViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCategory(int id, [FromForm] UpdateReviewViewModel updateViewModel)
        {
            if (id == null)
                return BadRequest("ID mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reviewDTO = _mapper.Map<UpdateReviewDTO>(updateViewModel);
            await _reviewServices.Update(reviewDTO);

            return Ok("Categoria atualizada");
        }

        /// <summary>
        /// - Remove uma Review
        /// </summary> 
        /// <param name="Id"></param>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveCategory(int id)
        {
            await _reviewServices.Remove(id);
            return NoContent();
        }
    }
}
