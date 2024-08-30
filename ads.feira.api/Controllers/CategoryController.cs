using ads.feira.api.Helpers;
using ads.feira.api.Models.Categories;
using ads.feira.application.DTO.Categories;
using ads.feira.application.Interfaces.Categories;
using ads.feira.domain.Enums.Products;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;

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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryViewModel>>> GetAllCategories()
        {
            var categories = await _categoryServices.GetAll();
            return Ok(_mapper.Map<IEnumerable<CategoryViewModel>>(categories));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryViewModel>> GetCategoryById(int id)
        {
            var category = await _categoryServices.GetById(id);
            if (category == null)
                return NotFound();

            return Ok(_mapper.Map<CategoryViewModel>(category));
        }

        [HttpGet("Find")]
        public async Task<ActionResult<IEnumerable<CategoryViewModel>>> FindCategory([FromQuery] string name)
        {
            Expression<Func<CategoryDTO, bool>> predicate = c => c.Name.Contains(name);
            var categories = await _categoryServices.Find(predicate);
            return Ok(_mapper.Map<IEnumerable<CategoryViewModel>>(categories));
        }

        [HttpPost]
        public async Task<ActionResult<CreateCategoryViewModel>> CreateCategory([FromForm] CreateCategoryViewModel createViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);          

            await FilesExtensions.UploadImage(createViewModel.Assets);            

            var categoryDTO = _mapper.Map<CreateCategoryDTO>(createViewModel);
            await _categoryServices.Create(categoryDTO, createViewModel.ProductIds);

            var createdCategory = await _categoryServices.GetById(categoryDTO.Id);

          

            return CreatedAtAction(nameof(GetCategoryById), new { id = createdCategory.Id }, _mapper.Map<CategoryViewModel>(createdCategory));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromForm] UpdateCategoryViewModel updateViewModel)
        {
            if (id != updateViewModel.Id)
                return BadRequest("ID mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoryDTO = _mapper.Map<UpdateCategoryDTO>(updateViewModel);
            await _categoryServices.Update(categoryDTO);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveCategory(int id)
        {
            await _categoryServices.Remove(id);
            return NoContent();
        }

        private void LoadViewBag()
        {
            var Type = GetEnumSelectList<TypeCategoryEnum>();
            
        }

        private List<SelectListItem> GetEnumSelectList<TEnum>() where TEnum : Enum
        {
            return Enum.GetValues(typeof(TEnum))
                .Cast<TEnum>()
                .Select(e => new SelectListItem
                {
                    Value = e.ToString(),
                    Text = GetEnumDisplayName(e)
                })
                .ToList();
        }

        private string GetEnumDisplayName(Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = field.GetCustomAttribute<DisplayAttribute>();
            return attribute != null ? attribute.Name : value.ToString();
        }
    }
}
