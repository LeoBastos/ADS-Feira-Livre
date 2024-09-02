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

       
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateCuponDTO cuponDto)
        {
            if (cuponDto == null)
                return BadRequest("Invalid Data");

            await _cuponService.Create(cuponDto);

            return new CreatedAtRouteResult("GetCuponById", new { id = cuponDto.Id }, cuponDto);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromForm] UpdateCuponDTO cuponDto)
        {
            if (cuponDto == null)
                return BadRequest("Invalid Data");

            await _cuponService.Update(cuponDto);

            return Ok(cuponDto);
        }

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

        [HttpPost("addProduct")]
        public async Task<ActionResult> AddProductToCupon([FromForm] AddProductToCuponViewModel model)
        {
            await _cuponService.AddProductToCupon(model.CuponId, model.ProductId);
            return Ok();
        }

        [HttpPost("removeProduct")]
        public async Task<ActionResult> RemoveProductFromCupon([FromForm] RemoveProductFromCuponViewModel model)
        {
            await _cuponService.RemoveProductFromCupon(model.CuponId, model.ProductId);
            return Ok();
        }

        [HttpPost("addStore")]
        public async Task<ActionResult> AddStoreToCupon([FromForm] AddStoreToCuponViewModel model)
        {
            await _cuponService.AddStoreToCupon(model.CuponId, model.StoreId);
            return Ok();
        }

        [HttpPost("removeStore")]
        public async Task<ActionResult> RemoveStoreFromCupon([FromForm] RemoveStoreFromCuponViewModel model)
        {
            await _cuponService.RemoveStoreFromCupon(model.CuponId, model.StoreId);
            return Ok();
        }
    }
}
