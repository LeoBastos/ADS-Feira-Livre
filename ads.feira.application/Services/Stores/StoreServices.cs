using ads.feira.application.CQRS.Stores.Commands;
using ads.feira.application.CQRS.Stores.Queries;
using ads.feira.application.DTO.Stores;
using ads.feira.application.Interfaces.Stores;
using ads.feira.domain.Interfaces.Stores;
using AutoMapper;
using MediatR;

namespace ads.feira.application.Services.Stores
{
    public class StoreServices : IStoreServices
    {
        private readonly IStoreRepository _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public StoreServices(IMapper mapper, IStoreRepository context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
            _mapper = mapper;
        }

        #region Queries

        /// <summary>
        /// Busca Store por Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Retorna uma LINQ Expression com uma store</returns>
        public async Task<StoreDTO> GetById(int id)
        {
            var storeQuery = new GetStoreByIdQuery(id);
            var result = await _mediator.Send(storeQuery);

            if (result == null)
                throw new NullReferenceException("Store não encontrada com o id fornecido.");

            return _mapper.Map<StoreDTO>(result);
        }

        /// <summary>
        /// Retorna todas as Stores
        /// </summary>      
        /// <returns>Retorna uma LINQ Expression com todas stores</returns>
        public async Task<IEnumerable<StoreDTO>> GetAll()
        {
            var storeQuery = new GetAllStoreQuery();
            var result = await _mediator.Send(storeQuery);
            return _mapper.Map<IEnumerable<StoreDTO>>(result);
        }      

        #endregion

        #region Commands

        /// <summary>
        /// Add a Entity
        /// </summary>
        /// <param name="entity">Store</param>
        /// <returns></returns>
        public async Task Create(CreateStoreDTO StoreDTO)
        {
            var storeCreateCommand = _mapper.Map<StoreCreateCommand>(StoreDTO);

            if (storeCreateCommand == null)
            {
                throw new Exception("Entity could not be created.");
            }

            var createdStore = await _mediator.Send(storeCreateCommand);
        }

        /// <summary>
        /// Update a Entity
        /// </summary>
        /// <param name="entity">Store</param>
        public async Task Update(UpdateStoreDTO StoreDTO)
        {
            var storeUpdateCommand = _mapper.Map<StoreUpdateCommand>(StoreDTO);

            if (storeUpdateCommand == null)
            {
                throw new Exception("Entity could not be updated.");
            }

            await _mediator.Send(storeUpdateCommand);
        }

        /// <summary>
        /// Remove a Entity
        /// </summary>
        /// <param name="entity">Store</param>
        public async Task Remove(int id)
        {
            var storeRemoveCommand = new StoreRemoveCommand(id);
            await _mediator.Send(storeRemoveCommand);
        }

        #endregion
    }
}
