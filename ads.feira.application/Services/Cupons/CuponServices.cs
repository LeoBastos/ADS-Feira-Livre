using ads.feira.application.CQRS.Cupons.Commands;
using ads.feira.application.CQRS.Cupons.Queries;
using ads.feira.application.CQRS.Products.Commands;
using ads.feira.application.DTO.Cupons;
using ads.feira.application.Interfaces.Cupons;
using ads.feira.domain.Interfaces.Cupons;
using AutoMapper;
using MediatR;
using System.Linq.Expressions;

namespace ads.feira.application.Services.Cupons
{
    public class CuponServices : ICuponService
    {
        private readonly ICuponRepository _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public CuponServices(IMapper mapper, ICuponRepository context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<CuponDTO> GetById(int id)
        {
            var cuponQuery = new GetCuponByIdQuery(id);
            var result = await _mediator.Send(cuponQuery);

            if (result == null)
                throw new NullReferenceException("Cupon não encontrado com o id fornecido.");

            return _mapper.Map<CuponDTO>(result);
        }

        public async Task<IEnumerable<CuponDTO>> GetAll()
        {
            var cuponQuery = new GetAllCuponQuery();
            var result = await _mediator.Send(cuponQuery);
            return _mapper.Map<IEnumerable<CuponDTO>>(result);
        }

        public async Task Create(CreateCuponDTO cuponDTO)
        {
            var cuponCreateCommand = _mapper.Map<CuponCreateCommand>(cuponDTO);

            if (cuponCreateCommand == null)
            {
                throw new Exception("Entity could not be created.");
            }

            var createdCupon = await _mediator.Send(cuponCreateCommand);
        }

        public async Task Update(UpdateCuponDTO cuponDTO)
        {
            var cuponUpdateCommand = _mapper.Map<ProductUpdateCommand>(cuponDTO);

            if (cuponUpdateCommand == null)
            {
                throw new Exception("Entity could not be updated.");
            }

            await _mediator.Send(cuponUpdateCommand);
        }

        public async Task Remove(int id)
        {
            var cuponRemoveCommand = new CuponRemoveCommand(id);
            await _mediator.Send(cuponRemoveCommand);
        }

        public async Task AddProductToCupon(int cuponId, int productId)
        {
            var addProductCommand = new AddProductToCuponCommand(cuponId, productId);
            await _mediator.Send(addProductCommand);
        }

        public async Task RemoveProductFromCupon(int cuponId, int productId)
        {
            var removeProductCommand = new RemoveProductFromCuponCommand(cuponId, productId);
            await _mediator.Send(removeProductCommand);
        }

        public async Task AddStoreToCupon(int cuponId, int storeId)
        {
            var addStoreCommand = new AddStoreToCuponCommand(cuponId, storeId);
            await _mediator.Send(addStoreCommand);
        }

        public async Task RemoveStoreFromCupon(int cuponId, int storeId)
        {
            var removeStoreCommand = new RemoveStoreFromCuponCommand(cuponId, storeId);
            await _mediator.Send(removeStoreCommand);
        }
    }
}
