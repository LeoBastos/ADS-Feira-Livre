using ads.feira.api.Models.Stores;
using ads.feira.application.DTO.Stores;
using ads.feira.application.Interfaces.Stores;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ads.feira.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IStoreServices _storeServices;
        private readonly IMapper _mapper;

        public StoreController(IStoreServices storeServices, IMapper mapper)
        {
            _storeServices = storeServices;
            _mapper = mapper;
        }


        /// <summary>
        /// - Retorna todas as Stores
        /// </summary>       
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [ProducesResponseType(typeof(StoreViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StoreViewModel>>> GetAll()
        {

            var stores = await _storeServices.GetAll();
            return Ok(_mapper.Map<IEnumerable<StoreDTO>>(stores));
        }

        /// <summary>
        /// - Busca uma Store por Id.
        /// </summary>
        /// <param name="Id"></param>
        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(StoreViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<StoreViewModel>> GetById(int id)
        {
            var store = await _storeServices.GetById(id);
            if (store == null)
                return NotFound();

            return Ok(_mapper.Map<StoreDTO>(store));
        }


        /// <summary>
        /// - Cria uma Store
        /// </summary> 
        [Authorize(Roles = "Admin, StoreOwner")]
        [HttpPost]
        [ProducesResponseType(typeof(CreateStoreViewModel), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Create([FromForm] CreateStoreViewModel storeViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Pega o ID do usuário logado
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User not authenticated.");
            }

            storeViewModel.StoreOwnerId = userId;

            var storeDTO = _mapper.Map<CreateStoreDTO>(storeViewModel);
            await _storeServices.Create(storeDTO);

            return Ok("Store Cadastrada");
        }

        /// <summary>
        /// - Atualiza uma Store
        /// </summary> 
        [Authorize(Roles = "Admin, StoreOwner")]
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(UpdateStoreViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Update(int id, [FromForm] UpdateStoreViewModel storeViewModel)
        {
            if (id == null)
                return BadRequest("ID mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var storeDTO = _mapper.Map<UpdateStoreDTO>(storeViewModel);
            await _storeServices.Update(storeDTO);

            return Ok("Categoria atualizada");
        }

        /// <summary>
        /// - Remove uma Store
        /// </summary> 
        /// <param name="Id"></param>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Remove(int id)
        {
            await _storeServices.Remove(id);
            return NoContent();
        }
    }
}
