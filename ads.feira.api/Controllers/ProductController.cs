using ads.feira.application.DTO.Products;
using ads.feira.application.Interfaces.Products;
using Microsoft.AspNetCore.Mvc;

namespace ads.feira.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductServices _productServices;

        public ProductController(IProductServices productServices)
        {
            _productServices = productServices;
        }

        /// <summary>
        /// - Retorna todos Produtos
        /// </summary>  
        [HttpGet]
        [ProducesResponseType(typeof(ProductDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAll()
        {
            var products = await _productServices.GetAll();
            return Ok(products);
        }

        /// <summary>
        /// - Busca um Produto por Id.
        /// </summary>
        /// <param name="Id"></param>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ProductDTO>> GetById(int id)
        {
            var product = await _productServices.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        /// <summary>
        /// - Retorna todos Produtos de uma loja
        /// </summary>  
        /// <param name="storeId"></param>
        [HttpGet("ProductsByStoreId/{storeId}")]
        [ProducesResponseType(typeof(ProductStoreDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ProductStoreDTO>>> GetProductByStoresId(int storeId)
        {
            var products = await _productServices.GetProductsForStoreId(storeId);
            if (products == null || !products.Any())
                return NotFound();
            return Ok(products);
        }

        /// <summary>
        /// - Cria um Produto
        /// </summary> 
        [HttpPost]
        [ProducesResponseType(typeof(CreateProductDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Create([FromForm] CreateProductDTO createProductDto)
        {
            if (createProductDto == null)
                return BadRequest();

            await _productServices.Create(createProductDto);

            return Ok("Produto Adicionado.");
        }

        /// <summary>
        /// - Atualiza uma Produto
        /// </summary> 
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(UpdateProductDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateProductDTO updateProductDto)
        {
            if (id != updateProductDto.Id)
                return BadRequest();

            await _productServices.Update(updateProductDto);

            return NoContent();
        }

        /// <summary>
        /// - Remove um Produto
        /// </summary> 
        /// <param name="Id"></param>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Remove(int id)
        {
            await _productServices.Remove(id);
            return NoContent();
        }

        /// <summary>
        /// - Cria um Cuponde Desconto para um Produto - Not Implement Yeat
        /// </summary> 
        /// <param name="productId"></param>
        /// <param name="cuponId"></param>
        [HttpPost("{productId}/cupons/{cuponId}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> AddCuponToProduct(int productId, int cuponId)
        {
            await _productServices.AddCuponToProduct(cuponId, productId);
            return NoContent();
        }

        /// <summary>
        /// - Remove um Cuponde Desconto para um Produto - Not Implement Yeat
        /// </summary> 
        /// <param name="productId"></param>
        /// <param name="cuponId"></param>
        [HttpDelete("{productId}/cupons/{cuponId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> RemoveCuponFromProduct(int productId, int cuponId)
        {
            await _productServices.RemoveCuponFromProduct(cuponId, productId);
            return NoContent();
        }
    }
}
