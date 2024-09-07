using ads.feira.api.Models.Categories;
using ads.feira.api.Models.Cupons;
using ads.feira.application.DTO.Cupons;
using ads.feira.application.Interfaces.Cupons;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace ads.feira.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuponController : ControllerBase
    {
        private readonly ICuponService _cuponService;
        private readonly IMapper _mapper;

        public CuponController(ICuponService cuponService, IMapper mapper)
        {
            _cuponService = cuponService;
            _mapper = mapper;
        }

        /// <summary>
        /// - Retorna todos os cupons
        /// </summary>    
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CuponDTO>>> GetAllCupons()
        {
            var cupons = await _cuponService.GetAll();
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
        public async Task<ActionResult<CuponDTO>> GetCuponById(int id)
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
        public async Task<ActionResult<CreateCuponViewModel>> Create([FromBody] CreateCuponViewModel cuponViewModel)
        {
            if (cuponViewModel == null)
                return BadRequest("Invalid Data");

            var cuponDto = _mapper.Map<CreateCuponDTO>(cuponViewModel);
            await _cuponService.Create(cuponDto);

            return Ok("Cupon cadastrado.");
        }

        /// <summary>
        /// - Atualiza um Cupon
        /// </summary> 
        [HttpPut]
        [HttpPut("{id}")]
        public async Task<ActionResult<UpdateCuponViewModel>> Update([FromBody] UpdateCuponViewModel cuponViewModel)
        {
            if (cuponViewModel == null)
                return BadRequest("Invalid Data");

            var cuponDto = _mapper.Map<UpdateCuponDTO>(cuponViewModel);
            await _cuponService.Update(cuponDto);

            return Ok(cuponDto);
        }


        /// <summary>
        /// - Remove um Cupon
        /// </summary> 
        /// <param name="Id"></param>
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
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
