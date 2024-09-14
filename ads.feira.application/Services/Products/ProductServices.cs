using ads.feira.application.CQRS.Categories.Queries;
using ads.feira.application.CQRS.Products.Commands;
using ads.feira.application.CQRS.Products.Commands.ads.feira.application.CQRS.Categories.Commands;
using ads.feira.application.CQRS.Products.Queries;
using ads.feira.application.CQRS.Stores.Commands;
using ads.feira.application.DTO.Categories;
using ads.feira.application.DTO.Products;
using ads.feira.application.Interfaces.Products;
using ads.feira.domain.Interfaces.Products;
using ads.feira.domain.Paginated;
using AutoMapper;
using MediatR;

namespace ads.feira.application.Services.Products
{
    public class ProductServices : IProductServices
    {
        private readonly IProductRepository _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public ProductServices(IMapper mapper, IProductRepository context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
            _mapper = mapper;
        }

        #region Queries

        /// <summary>
        /// Busca Produto por Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Retorna uma LINQ Expression com um produto</returns>
        public async Task<ProductDTO> GetById(string id)
        {
            var productQuery = new GetProdructByIdQuery(id);
            var result = await _mediator.Send(productQuery);

            if (result == null)
                throw new NullReferenceException("Produto não encontrado com o id fornecido.");

            return _mapper.Map<ProductDTO>(result);           
        }

        /// <summary>
        /// Retorna todos produtos
        /// </summary>      
        /// <returns>Retorna uma LINQ Expression com todos produtos</returns>
        public async Task<PagedResult<ProductDTO>> GetAll(int pageNumber, int pageSize)
        {
            var query = new GetAllProductQuery
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            var result = await _mediator.Send(query);

            var dtos = _mapper.Map<IEnumerable<ProductDTO>>(result.Items);

            return new PagedResult<ProductDTO>
            {
                Items = dtos,
                TotalItems = result.TotalItems,
                PageNumber = result.PageNumber,
                PageSize = result.PageSize,
                TotalPages = result.TotalPages
            };

            //var productQuery = new GetAllProductQuery();
            //var result = await _mediator.Send(productQuery);
            //return _mapper.Map<IEnumerable<ProductDTO>>(result);
        }


        /// <summary>
        /// Retorna todos os produtos de uma Store
        /// </summary>
        /// <param name="storeId"></param>
        /// <returns>Retorna uma LINQ Expression com um produto</returns>
        public async Task<IEnumerable<ProductStoreDTO>> GetProductsForStoreId(string storeId)
        {
            var productForStoreIdQuery = new GetProductForStoreIdQuery(storeId);
            var result = await _mediator.Send(productForStoreIdQuery);
            return _mapper.Map<IEnumerable<ProductStoreDTO>>(result);
        }


        #endregion

        #region Commands

        /// <summary>
        /// Add a Entity
        /// </summary>
        /// <param name="entity">Produto</param>
        /// <returns></returns>
        public async Task Create(CreateProductDTO productDTO)
        {
            var productCreateCommand = _mapper.Map<ProductCreateCommand>(productDTO);

            if (productCreateCommand == null)
            {
                throw new Exception("Entity could not be created.");
            }
            productCreateCommand.Assets = productCreateCommand.AssetsPath;

            var createdProduct = await _mediator.Send(productCreateCommand);

        }

        /// <summary>
        /// Update a Entity
        /// </summary>
        /// <param name="entity">Produto</param>
        public async Task Update(UpdateProductDTO productDTO)
        {
            var productUpdateCommand = _mapper.Map<ProductUpdateCommand>(productDTO);

            if (productUpdateCommand == null)
            {
                throw new Exception("Entity could not be updated.");
            }

            await _mediator.Send(productUpdateCommand);
        }

        /// <summary>
        /// Remove a Entity
        /// </summary>
        /// <param name="entity">Produto</param>
        public async Task Remove(string id)
        {
            var productRemoveCommand = new ProductRemoveCommand(id);
            await _mediator.Send(productRemoveCommand);
        }

        #endregion


        //Métodos abaixo, Rever para melhorar e implementar futuramente
        public async Task AddCuponToProduct(string cuponId, string productId)
        {
            var addCuponCommand = new AddCuponToProductCommand
            {
                CuponId = cuponId,
                ProductId = productId
            };
            await _mediator.Send(addCuponCommand);
        }

        public async Task RemoveCuponFromProduct(string cuponId, string productId)
        {
            var removeCuponCommand = new RemoveCuponFromProductCommand
            {
                CuponId = cuponId,
                ProductId = productId
            };
            await _mediator.Send(removeCuponCommand);
        }
    }
}
