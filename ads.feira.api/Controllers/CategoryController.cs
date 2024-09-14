using ads.feira.api.Helpers.Images;
using ads.feira.application.DTO.Categories;
using ads.feira.application.Interfaces.Categories;
using ads.feira.domain.Paginated;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace ads.feira.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryServices _categoryServices;
        // private readonly S3Service _s3Service;       

        public CategoryController(ICategoryServices categoryServices/*, S3Service s3Service*/)
        {
            _categoryServices = categoryServices;
            //_s3Service = s3Service;
        }

        /// <summary>
        /// - Retorna todas as Categorias
        /// </summary>       
        [Authorize]
        [HttpGet]
        [OutputCache(Duration = 15)]
        [ProducesResponseType(typeof(CategoryDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PagedResult<CategoryDTO>>> GetAllCategories([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20)
        {
            var categories = await _categoryServices.GetAll(pageNumber, pageSize);
            return Ok(categories);
        }

        /// <summary>
        /// - Busca uma categoria por Id.
        /// </summary>
        /// <param name="Id"></param>
        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CategoryDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CategoryDTO>> GetCategoryById(string id)
        {

            var category = await _categoryServices.GetById(id);
            if (category == null)
                return NotFound();

            return Ok(category);
        }

        /// <summary>
        /// - Cria uma categoria
        /// </summary> 
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ProducesResponseType(typeof(CreateCategoryDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CreateCategoryDTO>> CreateCategory([FromForm] CreateCategoryDTO createDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (createDTO.Assets != null)
            {
                createDTO.AssetsPath = await FilesExtensions.UploadImage(createDTO.Assets);
            }

            await _categoryServices.Create(createDTO);

            return Ok("Categoria cadastrada.");
        }

        /// <summary>
        /// - Atualiza uma categoria
        /// </summary> 
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(UpdateCategoryDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCategory(string id, [FromForm] UpdateCategoryDTO updateDTO)
        {
            if (id == null)
                return BadRequest("ID mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            if (updateDTO.Assets != null)
            {
                updateDTO.AssetsPath = await FilesExtensions.UploadImage(updateDTO.Assets);
            }

            await _categoryServices.Update(updateDTO);

            return Ok("Categoria atualizada");
        }

        /// <summary>
        /// - Remove uma categoria
        /// </summary> 
        /// <param name="Id"></param>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveCategory(string id)
        {
            await _categoryServices.Remove(id);
            return NoContent();
        }
    }
}
