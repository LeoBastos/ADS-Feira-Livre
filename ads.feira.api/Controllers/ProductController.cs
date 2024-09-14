using ads.feira.api.Helpers.Images;
using ads.feira.application.DTO.Products;
using ads.feira.application.Interfaces.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace ads.feira.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductServices _productServices;
        //private readonly S3Service _s3Service;

        public ProductController(IProductServices productServices /*S3Service s3Service*/)
        {
            _productServices = productServices;
            //_s3Service = s3Service;
        }

        /// <summary>
        /// - Retorna todos Produtos
        /// </summary>  
        [HttpGet]
        [OutputCache(Duration = 15)]
        [ProducesResponseType(typeof(ProductDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20)
        {
            var products = await _productServices.GetAll(pageNumber, pageSize);
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
        public async Task<ActionResult<ProductDTO>> GetById(string id)
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
        public async Task<ActionResult<IEnumerable<ProductStoreDTO>>> GetProductByStoresId(string storeId)
        {
            var products = await _productServices.GetProductsForStoreId(storeId);
            if (products == null || !products.Any())
                return NotFound();
            return Ok(products);
        }

        /// <summary>
        /// - Cria um Produto
        /// </summary> 
        [Authorize(Roles = "Admin, StoreOwner")]
        [HttpPost]
        [ProducesResponseType(typeof(CreateProductDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Create([FromForm] CreateProductDTO createProductDto)
        {
            if (createProductDto == null)
                return BadRequest();

            if (createProductDto.Assets != null)
            {
                createProductDto.AssetsPath = await FilesExtensions.UploadImage(createProductDto.Assets); //await _s3Service.UploadImageAsync(createProductDto.Assets);
            }

            await _productServices.Create(createProductDto);

            return Ok("Produto Adicionado.");
        }

        /// <summary>
        /// - Atualiza uma Produto
        /// </summary> 
        [Authorize(Roles = "Admin, StoreOwner")]
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(UpdateProductDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Update(string id, [FromBody] UpdateProductDTO updateProductDto)
        {
            if (id != updateProductDto.Id)
                return BadRequest();


            if (updateProductDto.Assets != null)
            {
                updateProductDto.AssetsPath = await FilesExtensions.UploadImage(updateProductDto.Assets);
            }

            await _productServices.Update(updateProductDto);

            return NoContent();
        }

        /// <summary>
        /// - Remove um Produto
        /// </summary> 
        /// <param name="Id"></param>
        [Authorize(Roles = "Admin, StoreOwner")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Remove(string id)
        {
            await _productServices.Remove(id);
            return NoContent();
        }

        /// <summary>
        /// - Cria um Cuponde Desconto para um Produto - Not Implement Yeat
        /// </summary> 
        /// <param name="productId"></param>
        /// <param name="cuponId"></param>
        [Authorize(Roles = "Admin, StoreOwner")]
        [HttpPost("{productId}/cupons/{cuponId}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> AddCuponToProduct(string productId, string cuponId)
        {
            await _productServices.AddCuponToProduct(cuponId, productId);
            return NoContent();
        }

        /// <summary>
        /// - Remove um Cuponde Desconto para um Produto - Not Implement Yeat
        /// </summary> 
        /// <param name="productId"></param>
        /// <param name="cuponId"></param>
        [Authorize(Roles = "Admin, StoreOwner")]
        [HttpDelete("{productId}/cupons/{cuponId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> RemoveCuponFromProduct(string productId, string cuponId)
        {
            await _productServices.RemoveCuponFromProduct(cuponId, productId);
            return NoContent();
        }
    }
}
