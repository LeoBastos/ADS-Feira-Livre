using ads.feira.api.Helpers.Images;
using ads.feira.application.DTO.Stores;
using ads.feira.application.Interfaces.Stores;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using System.Security.Claims;

namespace ads.feira.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IStoreServices _storeServices;
        //private readonly S3Service _s3Service;

        public StoreController(IStoreServices storeServices/*, S3Service s3Service*/)
        {
            _storeServices = storeServices;
            //_s3Service = s3Service;
        }


        /// <summary>
        /// - Retorna todas as Stores
        /// </summary>       
        [Authorize(Roles = "Admin, StoreOwner, Customer")]
        [HttpGet]
        [OutputCache(Duration = 15)]
        [ProducesResponseType(typeof(StoreDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StoreDTO>>> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20)
        {

            var stores = await _storeServices.GetAll(pageNumber, pageSize);
            return Ok(stores);
        }

        /// <summary>
        /// - Busca uma Store por Id.
        /// </summary>
        /// <param name="Id"></param>
        [Authorize(Roles = "Admin, StoreOwner, Customer")]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(StoreDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<StoreDTO>> GetById(string id)
        {
            var store = await _storeServices.GetById(id);
            if (store == null)
                return NotFound();

            return Ok(store);
        }


        /// <summary>
        /// - Cria uma Store
        /// </summary> 
        [Authorize(Roles = "Admin, StoreOwner")]
        [HttpPost]
        [ProducesResponseType(typeof(CreateStoreDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Create([FromForm] CreateStoreDTO createStoreDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Pega o ID do usuário logado
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User not authenticated.");
            }

            if (createStoreDTO.Assets != null)
            {
                createStoreDTO.AssetsPath = await FilesExtensions.UploadImage(createStoreDTO.Assets);
            }

            createStoreDTO.StoreOwnerId = userId;

            await _storeServices.Create(createStoreDTO);

            return Ok("Store Cadastrada");
        }

        /// <summary>
        /// - Atualiza uma Store
        /// </summary> 
        [Authorize(Roles = "Admin, StoreOwner")]
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(UpdateStoreDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Update(string id, [FromForm] UpdateStoreDTO storeDto)
        {
            if (id == null)
                return BadRequest("ID mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (storeDto.Assets != null)
            {
                storeDto.AssetsPath = await FilesExtensions.UploadImage(storeDto.Assets);
            }

            await _storeServices.Update(storeDto);

            return Ok("Categoria atualizada");
        }

        /// <summary>
        /// - Remove uma Store
        /// </summary> 
        /// <param name="Id"></param>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Remove(string id)
        {
            await _storeServices.Remove(id);
            return NoContent();
        }
    }
}
