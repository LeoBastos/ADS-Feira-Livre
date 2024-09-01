using ads.feira.application.CQRS.Categories.Commands;
using ads.feira.application.CQRS.Cupons.Commands;
using ads.feira.application.DTO.Accounts;
using ads.feira.application.DTO.Categories;
using ads.feira.application.DTO.Cupons;
using ads.feira.application.DTO.Products;
using ads.feira.domain.Entity.Accounts;
using ads.feira.domain.Entity.Categories;
using ads.feira.domain.Entity.Cupons;
using ads.feira.domain.Entity.Products;
using AutoMapper;

namespace ads.feira.application.Mappings
{
    public class ApplicationServiceMappings : Profile
    {
        public ApplicationServiceMappings()
        {
            #region Category
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Category, CreateCategoryDTO>().ReverseMap();
            CreateMap<Category, UpdateCategoryDTO>().ReverseMap();
            CreateMap<Category, CategoryCreateCommand>().ReverseMap();
            CreateMap<Category, CategoryUpdateCommand>().ReverseMap();

            CreateMap<CreateCategoryDTO, CategoryCreateCommand>()
            .ForMember(dest => dest.CreatedById, opt => opt.MapFrom(src => src.CreatedById.GetHashCode() & int.MaxValue))
            .ReverseMap();

            #endregion

            #region Accounts
            CreateMap<Account, AccountDTO>().ReverseMap();
            CreateMap<Account, AccountUpdateDTO>().ReverseMap();
            CreateMap<Account, AccountResponseDTO>().ReverseMap();
            #endregion

            #region Cupon
            CreateMap<Cupon, CuponDTO>().ReverseMap();
            CreateMap<Cupon, CreateCuponDTO>().ReverseMap();
            CreateMap<Cupon, UpdateCuponDTO>().ReverseMap();
            CreateMap<Cupon, CuponCreateCommand>().ReverseMap();
            CreateMap<Cupon, CuponUpdateCommand>().ReverseMap();

            CreateMap<CuponDTO, CuponCreateCommand>().ReverseMap();
            CreateMap<CuponCreateCommand, CuponDTO>().ReverseMap();
            CreateMap<CreateCuponDTO, CuponCreateCommand>().ReverseMap(); // Adicione esta linha
            #endregion

            #region Products
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Product, CreateProductDTO>().ReverseMap();
            CreateMap<Product, UpdateProductDTO>().ReverseMap();
            CreateMap<Product, CategoryCreateCommand>().ReverseMap();
            CreateMap<Product, CategoryUpdateCommand>().ReverseMap();
            #endregion
        }
    }
}
