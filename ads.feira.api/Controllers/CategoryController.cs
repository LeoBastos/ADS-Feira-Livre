using ads.feira.api.Helpers;
using ads.feira.api.Models.Categories;
using ads.feira.application.DTO.Categories;
using ads.feira.application.Interfaces.Categories;
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
        [ProducesResponseType(typeof(CategoryViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<CategoryViewModel>>> GetAllCategories()
        {
            var categories = await _categoryServices.GetAll();
            return Ok(_mapper.Map<IEnumerable<CategoryViewModel>>(categories));
        }

        /// <summary>
        /// - Busca uma categoria por Id.
        /// </summary>
        /// <param name="Id"></param>
        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CategoryViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CategoryViewModel>> GetCategoryById(int id)
        {
            var category = await _categoryServices.GetById(id);
            if (category == null)
                return NotFound();

            return Ok(_mapper.Map<CategoryViewModel>(category));
        }

        /// <summary>
        /// - Cria uma categoria
        /// </summary> 
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ProducesResponseType(typeof(CreateCategoryViewModel), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CreateCategoryViewModel>> CreateCategory([FromForm] CreateCategoryViewModel createViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var category = new CreateCategoryDTO
            {
                Name = createViewModel.Name,
                Description = createViewModel.Description,
                Assets = await FilesExtensions.UploadImage(createViewModel.Assets) ?? "~/images/noimage.png",
                Type = createViewModel.Type,
            };

            //var categoryDTO = _mapper.Map<CreateCategoryDTO>(createViewModel);
            await _categoryServices.Create(category);


            return Ok("Categoria cadastrada.");
        }

        /// <summary>
        /// - Atualiza uma categoria
        /// </summary> 
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(UpdateCategoryViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCategory(int id, [FromForm] UpdateCategoryViewModel updateViewModel)
        {
            if (id == null)
                return BadRequest("ID mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);            

            var categoryDTO = _mapper.Map<UpdateCategoryDTO>(updateViewModel);
            await _categoryServices.Update(categoryDTO);

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
