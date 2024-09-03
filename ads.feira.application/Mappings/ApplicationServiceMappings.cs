using ads.feira.application.CQRS.Categories.Commands;
using ads.feira.application.CQRS.Cupons.Commands;
using ads.feira.application.CQRS.Products.Commands;
using ads.feira.application.CQRS.Reviews.Commands;
using ads.feira.application.CQRS.Stores.Commands;
using ads.feira.application.DTO.Accounts;
using ads.feira.application.DTO.Categories;
using ads.feira.application.DTO.Cupons;
using ads.feira.application.DTO.Products;
using ads.feira.application.DTO.Reviews;
using ads.feira.application.DTO.Stores;
using ads.feira.domain.Entity.Accounts;
using ads.feira.domain.Entity.Categories;
using ads.feira.domain.Entity.Cupons;
using ads.feira.domain.Entity.Products;
using ads.feira.domain.Entity.Reviews;
using ads.feira.domain.Entity.Stores;
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
            CreateMap<CreateCategoryDTO, CategoryCreateCommand>().ReverseMap();
            CreateMap<UpdateCategoryDTO, CategoryUpdateCommand>().ReverseMap();         

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
            CreateMap<CreateCuponDTO, CuponCreateCommand>().ReverseMap();
            CreateMap<UpdateCuponDTO, CuponUpdateCommand>().ReverseMap();
            #endregion

            #region Products
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Product, CreateProductDTO>().ReverseMap();
            CreateMap<Product, UpdateProductDTO>().ReverseMap();
            CreateMap<CreateProductDTO, ProductCreateCommand>().ReverseMap();
            CreateMap<UpdateProductDTO, ProductUpdateCommand>().ReverseMap();
            #endregion

            #region Stores

            CreateMap<Store, StoreDTO>().ReverseMap();
            CreateMap<Store, CreateStoreDTO>().ReverseMap();
            CreateMap<Store, UpdateStoreDTO>().ReverseMap();
            CreateMap<CreateStoreDTO, StoreCreateCommand>().ReverseMap();
            CreateMap<UpdateStoreDTO, StoreCreateCommand>().ReverseMap();

            #endregion

            #region Reviews
            CreateMap<Review, ReviewDTO>().ReverseMap();
            CreateMap<Review, CreateReviewDTO>().ReverseMap();
            CreateMap<Review, UpdateReviewDTO>().ReverseMap();
            CreateMap<CreateReviewDTO, ReviewCreateCommand>().ReverseMap();
            CreateMap<UpdateReviewDTO, ReviewUpdateCommand>().ReverseMap();
            #endregion
        }
    }
}
