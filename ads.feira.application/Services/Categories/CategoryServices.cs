using ads.feira.application.CQRS.Categories.Commands;
using ads.feira.application.CQRS.Categories.Queries;
using ads.feira.application.DTO.Categories;
using ads.feira.application.Interfaces.Categories;
using ads.feira.domain.Interfaces.Categories;
using AutoMapper;
using MediatR;
using System.Linq.Expressions;

namespace ads.feira.application.Services.Categories
{
    public class CategoryServices : ICategoryServices
    {
        private readonly ICategoryRepository _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
               
        public CategoryServices(IMapper mapper, ICategoryRepository context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
            _mapper = mapper;
        }

        #region Queries

        /// <summary>
        /// Busca Categoria por Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Retorna uma LINQ Expression com um categoria</returns>
        public async Task<CategoryDTO> GetById(int id)
        {
            var categoryQuery = new GetCategoryByIdQuery(id);
            var result = await _mediator.Send(categoryQuery);

            if (result == null)
                throw new NullReferenceException("Categoria não encontrada com o id fornecido.");

            return _mapper.Map<CategoryDTO>(result);
        }

        /// <summary>
        /// Retorna todas categorias
        /// </summary>      
        /// <returns>Retorna uma LINQ Expression com todas categorias</returns>
        public async Task<IEnumerable<CategoryDTO>> GetAll()
        {
            var categoryQuery = new GetAllCategoryQuery();
            var result = await _mediator.Send(categoryQuery);
            return _mapper.Map<IEnumerable<CategoryDTO>>(result);
        }

        #endregion

        #region Commands

        /// <summary>
        /// Add a Entity
        /// </summary>
        /// <param name="entity">Category</param>
        /// <returns></returns>
        public async Task Create(CreateCategoryDTO categoryDTO)
        {
            var categoryCreateCommand = _mapper.Map<CategoryCreateCommand>(categoryDTO);

            if (categoryCreateCommand == null)
            {
                throw new Exception("Entity could not be created.");
            }

            var createdCategory = await _mediator.Send(categoryCreateCommand);

            //if (productIds != null && productIds.Any())
            //{
            //    foreach (var productId in productIds)
            //    {
            //        var addProductCommand = new AddProductToCategoryCommand
            //        {
            //            CategoryId = createdCategory.Id,
            //            ProductId = productId
            //        };
            //        await _mediator.Send(addProductCommand);
            //    }
            //}
        }

        /// <summary>
        /// Update a Entity
        /// </summary>
        /// <param name="entity">Category</param>
        public async Task Update(UpdateCategoryDTO categoryDTO)
        {
            var categoryUpdateCommand = _mapper.Map<CategoryUpdateCommand>(categoryDTO);

            if (categoryUpdateCommand == null)
            {
                throw new Exception("Entity could not be updated.");
            }

            await _mediator.Send(categoryUpdateCommand);
        }

        /// <summary>
        /// Remove a Entity
        /// </summary>
        /// <param name="entity">Category</param>
        public async Task Remove(int id)
        {
            var categoryRemoveCommand = new CategoryRemoveCommand(id);
            await _mediator.Send(categoryRemoveCommand);
        }

        /// <summary>
        /// Método para Adicionar um produto a Categoria
        /// </summary>
        /// <param name="categoryId, productId"></param>
        /// <returns>Retorna uma LINQ Expression com um produto inserido na categoria</returns>
        public async Task AddProductToCategory(int categoryId, int productId)
        {
            var addProductCommand = new AddProductToCategoryCommand
            {
                CategoryId = categoryId,
                ProductId = productId
            };
            await _mediator.Send(addProductCommand);
        }

        /// <summary>
        /// Método para Remover um produto da Categoria
        /// </summary>
        /// <param name="categoryId, productId"></param>
        /// <returns>Retorna uma LINQ Expression com um produto removido na categoria</returns>
        public async Task RemoveProductFromCategory(int categoryId, int productId)
        {
            var removeProductCommand = new RemoveProductFromCategoryCommand
            {
                CategoryId = categoryId,
                ProductId = productId
            };
            await _mediator.Send(removeProductCommand);
        }
        
        #endregion
    }
}
