using ads.feira.application.CQRS.Products.Commands;
using ads.feira.application.CQRS.Products.Commands.ads.feira.application.CQRS.Categories.Commands;
using ads.feira.application.CQRS.Products.Queries;
using ads.feira.application.DTO.Products;
using ads.feira.application.Interfaces.Products;
using ads.feira.domain.Interfaces.Products;
using AutoMapper;
using MediatR;
using System.Linq.Expressions;

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

        public async Task<ProductDTO> GetById(int id)
        {
            var productQuery = new GetProdructByIdQuery(id);
            var result = await _mediator.Send(productQuery);

            if (result == null)
                throw new NullReferenceException("Produto não encontrado com o id fornecido.");

            return _mapper.Map<ProductDTO>(result);
        }

        public async Task<IEnumerable<ProductDTO>> Find(Expression<Func<ProductDTO, bool>> predicate)
        {
            var findProductQuery = new FindProductQuery(predicate);
            var result = await _mediator.Send(findProductQuery);
            return _mapper.Map<IEnumerable<ProductDTO>>(result);
        }

        public async Task<IEnumerable<ProductDTO>> GetAll()
        {
            var productQuery = new GetAllProductQuery();
            var result = await _mediator.Send(productQuery);
            return _mapper.Map<IEnumerable<ProductDTO>>(result);
        }

        public async Task Create(CreateProductDTO productDTO)
        {
            var productCreateCommand = _mapper.Map<ProductCreateCommand>(productDTO);

            if (productCreateCommand == null)
            {
                throw new Exception("Entity could not be created.");
            }

            var createdProduct = await _mediator.Send(productCreateCommand);

        }

        public async Task Update(UpdateProductDTO productDTO)
        {
            var productUpdateCommand = _mapper.Map<ProductUpdateCommand>(productDTO);

            if (productUpdateCommand == null)
            {
                throw new Exception("Entity could not be updated.");
            }

            await _mediator.Send(productUpdateCommand);
        }

        public async Task Remove(int id)
        {
            var productRemoveCommand = new ProductRemoveCommand(id);
            await _mediator.Send(productRemoveCommand); ;
        }

        public async Task AddCuponToProduct(int cuponId, int productId)
        {
            var addCuponCommand = new AddCuponToProductCommand
            {
                CuponId = cuponId,
                ProductId = productId
            };
            await _mediator.Send(addCuponCommand);
        }

        public async Task RemoveCuponFromProduct(int cuponId, int productId)
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
