using ads.feira.application.CQRS.Categories.Queries;
using ads.feira.application.CQRS.Cupons.Commands;
using ads.feira.application.CQRS.Cupons.Queries;
using ads.feira.application.DTO.Categories;
using ads.feira.application.DTO.Cupons;
using ads.feira.application.Interfaces.Cupons;
using ads.feira.domain.Interfaces.Cupons;
using ads.feira.domain.Paginated;
using AutoMapper;
using MediatR;

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

        #region Queries

        /// <summary>
        /// Busca Cupon por Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Retorna uma LINQ Expression com um Cupon</returns>
        public async Task<CuponDTO> GetById(string id)
        {
            var cuponQuery = new GetCuponByIdQuery(id);
            var result = await _mediator.Send(cuponQuery);

            if (result == null)
                throw new NullReferenceException("Cupon não encontrado com o id fornecido.");

            return _mapper.Map<CuponDTO>(result);
        }

        /// <summary>
        /// Retorna todos cupons
        /// </summary>      
        /// <returns>Retorna uma LINQ Expression com todos cupons</returns>
        public async Task<PagedResult<CuponDTO>> GetAll(int pageNumber, int pageSize)
        {
            var query = new GetAllCuponQuery
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var result = await _mediator.Send(query);

            var dtos = _mapper.Map<IEnumerable<CuponDTO>>(result.Items);

            return new PagedResult<CuponDTO>
            {
                Items = dtos,
                TotalItems = result.TotalItems,
                PageNumber = result.PageNumber,
                PageSize = result.PageSize,
                TotalPages = result.TotalPages
            };

            //var cuponQuery = new GetAllCuponQuery();
            //var result = await _mediator.Send(cuponQuery);
            //return _mapper.Map<IEnumerable<CuponDTO>>(result);
        }

        #endregion

        #region Commands

        /// <summary>
        /// Add a Entity
        /// </summary>
        /// <param name="entity">Cupon</param>
        /// <returns></returns>
        public async Task Create(CreateCuponDTO cuponDTO)
        {
            var cuponCreateCommand = _mapper.Map<CuponCreateCommand>(cuponDTO);

            if (cuponCreateCommand == null)
            {
                throw new Exception("Entity could not be created.");
            }

            var createdCupon = await _mediator.Send(cuponCreateCommand);
        }

        /// <summary>
        /// Update a Entity
        /// </summary>
        /// <param name="entity">Cupon</param>
        public async Task Update(UpdateCuponDTO cuponDTO)
        {
            var cuponUpdateCommand = _mapper.Map<CuponUpdateCommand>(cuponDTO);

            if (cuponUpdateCommand == null)
            {
                throw new Exception("Entity could not be updated.");
            }

            await _mediator.Send(cuponUpdateCommand);
        }

        /// <summary>
        /// Remove a Entity
        /// </summary>
        /// <param name="entity">Remove</param>
        public async Task Remove(string id)
        {
            var cuponRemoveCommand = new CuponRemoveCommand(id);
            await _mediator.Send(cuponRemoveCommand);
        }

        #endregion

        //Métodos abaixo, Rever para melhorar e implementar futuramente

        public async Task AddProductToCupon(string cuponId, string productId)
        {
            var addProductCommand = new AddProductToCuponCommand(cuponId, productId);
            await _mediator.Send(addProductCommand);
        }

        public async Task RemoveProductFromCupon(string cuponId, string productId)
        {
            var removeProductCommand = new RemoveProductFromCuponCommand(cuponId, productId);
            await _mediator.Send(removeProductCommand);
        }

        public async Task AddStoreToCupon(string cuponId, string storeId)
        {
            var addStoreCommand = new AddStoreToCuponCommand(cuponId, storeId);
            await _mediator.Send(addStoreCommand);
        }

        public async Task RemoveStoreFromCupon(string cuponId, string storeId)
        {
            var removeStoreCommand = new RemoveStoreFromCuponCommand(cuponId, storeId);
            await _mediator.Send(removeStoreCommand);
        }
    }
}
