using ads.feira.api.Helpers;
using ads.feira.api.Models.Products;
using ads.feira.application.DTO.Products;
using ads.feira.application.Interfaces.Products;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ads.feira.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductServices _productServices;
        private readonly IMapper _mapper;

        public ProductController(IProductServices productServices, IMapper mapper)
        {
            _productServices = productServices;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductViewModel>>> GetAll()
        {
            var products = await _productServices.GetAll();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductViewModel>> GetById(int id)
        {
            var product = await _productServices.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromForm] CreateProductViewModel productViewModel)
        {
            if (productViewModel == null)
                return BadRequest();

            await FilesExtensions.UploadImage(productViewModel.Assets);

            await _productServices.Create(_mapper.Map<CreateProductDTO>(productViewModel));

            return Ok("Produto Adicionado.");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateProductViewModel productViewModel)
        {
            if (id != productViewModel.Id)
                return BadRequest();

            await _productServices.Update(_mapper.Map<UpdateProductDTO>(productViewModel));

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Remove(int id)
        {
            await _productServices.Remove(id);
            return NoContent();
        }

        [HttpPost("{productId}/cupons/{cuponId}")]
        public async Task<ActionResult> AddCuponToProduct(int productId, int cuponId)
        {
            await _productServices.AddCuponToProduct(cuponId, productId);
            return NoContent();
        }

        [HttpDelete("{productId}/cupons/{cuponId}")]
        public async Task<ActionResult> RemoveCuponFromProduct(int productId, int cuponId)
        {
            await _productServices.RemoveCuponFromProduct(cuponId, productId);
            return NoContent();
        }
    }
}
