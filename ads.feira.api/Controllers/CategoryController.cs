using ads.feira.api.Helpers;
using ads.feira.api.Models.Categories;
using ads.feira.application.DTO.Categories;
using ads.feira.application.DTO.Stores;
using ads.feira.application.Interfaces.Categories;
using ads.feira.domain.Entity.Categories;
using ads.feira.domain.Entity.Stores;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ads.feira.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryServices _categoryServices;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryServices categoryServices, IMapper mapper)
        {
            _categoryServices = categoryServices;
            _mapper = mapper;
        }

        /// <summary>
        /// - Retorna todas as Categorias
        /// </summary>       
        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(CategoryDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetAllCategories()
        {
            var categories = await _categoryServices.GetAll();           
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
        public async Task<ActionResult<CategoryDTO>> GetCategoryById(int id)
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
        public async Task<IActionResult> UpdateCategory(int id, [FromForm] UpdateCategoryDTO updateDTO)
        {
            if (id == null)
                return BadRequest("ID mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);            
           
            await _categoryServices.Update(updateDTO);

            return Ok("Categoria atualizada");
        }

        /// <summary>
        /// - Remove uma categoria
        /// </summary> 
        /// <param name="Id"></param>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveCategory(int id)
        {
            await _categoryServices.Remove(id);
            return NoContent();
        }
    }
}
