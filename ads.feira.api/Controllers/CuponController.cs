using ads.feira.api.Models.Cupons;
using ads.feira.application.DTO.Cupons;
using ads.feira.application.Interfaces.Cupons;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace ads.feira.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuponController : ControllerBase
    {
        private readonly ICuponService _cuponService;        

        public CuponController(ICuponService cuponService)
        {
            _cuponService = cuponService;           
        }

        /// <summary>
        /// - Retorna todos os cupons
        /// </summary>    
        [HttpGet]
        [OutputCache(Duration = 15)]
        [ProducesResponseType(typeof(CuponDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<CuponDTO>>> GetAllCupons([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20)
        {
            var cupons = await _cuponService.GetAll(pageNumber, pageSize);
            if (cupons == null)
            {
                return NotFound("Cupons not found");
            }
            return Ok(cupons);
        }


        /// <summary>
        /// - Busca um cupon por Id.
        /// </summary>
        /// <param name="Id"></param>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CuponDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CuponDTO>> GetCuponById(string id)
        {
            var cupon = await _cuponService.GetById(id);
            if (cupon == null)
            {
                return NotFound("Cupon not found");
            }
            return Ok(cupon);
        }

        /// <summary>
        /// - Cria um Cupon
        /// </summary> 
        [HttpPost]
        [Authorize(Roles = "Admin, StoreOwner")]
        [ProducesResponseType(typeof(CreateCuponDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CreateCuponDTO>> Create([FromBody] CreateCuponDTO cuponDTO)
        {
            if (cuponDTO == null)
                return BadRequest("Invalid Data");

            await _cuponService.Create(cuponDTO);

            return Ok("Cupon cadastrado.");
        }

        /// <summary>
        /// - Atualiza um Cupon
        /// </summary>   
        [Authorize(Roles = "Admin, StoreOwner")]
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(UpdateCuponDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UpdateCuponDTO>> Update([FromBody] UpdateCuponDTO cuponDTO)
        {
            if (cuponDTO == null)
                return BadRequest("Invalid Data");


            await _cuponService.Update(cuponDTO);

            return Ok(cuponDTO);
        }


        /// <summary>
        /// - Remove um Cupon
        /// </summary> 
        /// <param name="Id"></param>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(string id)
        {
            var cupon = await _cuponService.GetById(id);
            if (cupon == null)
            {
                return NotFound("Cupon not found");
            }

            await _cuponService.Remove(id);

            return Ok(cupon);
        }


        /// <summary>
        /// - Not Implemment Yeat
        /// </summary> 
        /// <param name="Id"></param>
        [HttpPost("addProduct")]
        public async Task<ActionResult<AddProductToCuponViewModel>> AddProductToCupon([FromBody] AddProductToCuponViewModel model)
        {
            await _cuponService.AddProductToCupon(model.CuponId, model.ProductId);
            return Ok();
        }

        /// <summary>
        /// - Not Implemment Yeat
        /// </summary> 
        /// <param name="Id"></param>
        [HttpPost("removeProduct")]
        public async Task<ActionResult> RemoveProductFromCupon([FromForm] RemoveProductFromCuponViewModel model)
        {
            await _cuponService.RemoveProductFromCupon(model.CuponId, model.ProductId);
            return Ok();
        }
        /// <summary>
        /// - Not Implemment Yeat
        /// </summary> 
        /// <param name="Id"></param>
        [HttpPost("addStore")]
        public async Task<ActionResult<AddStoreToCuponViewModel>> AddStoreToCupon([FromBody] AddStoreToCuponViewModel model)
        {
            await _cuponService.AddStoreToCupon(model.CuponId, model.StoreId);
            return Ok();
        }
        /// <summary>
        /// - Not Implemment Yeat
        /// </summary> 
        /// <param name="Id"></param>
        [HttpPost("removeStore")]
        public async Task<ActionResult> RemoveStoreFromCupon([FromForm] RemoveStoreFromCuponViewModel model)
        {
            await _cuponService.RemoveStoreFromCupon(model.CuponId, model.StoreId);
            return Ok();
        }
    }
}
