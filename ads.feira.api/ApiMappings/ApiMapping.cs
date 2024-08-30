using ads.feira.api.Models.Accounts;
using ads.feira.api.Models.Categories;
using ads.feira.api.Models.Cupons;
using ads.feira.api.Models.Products;
using ads.feira.application.DTO.Accounts;
using ads.feira.application.DTO.Categories;
using ads.feira.application.DTO.Cupons;
using ads.feira.application.DTO.Products;
using AutoMapper;

namespace ads.feira.api.ApiMappings
{
    public class ApiMapping : Profile
    {
        public ApiMapping()
        {
            #region Category
            CreateMap<CategoryDTO, CategoryViewModel>().ReverseMap();
            CreateMap<CreateCategoryDTO, CreateCategoryViewModel>().ReverseMap();
            CreateMap<UpdateCategoryDTO, UpdateCategoryViewModel>().ReverseMap();
            #endregion

            #region CognitoUser
            CreateMap<CognitoUserDTO, UpdateCognitorUserViewModel>().ReverseMap();
            CreateMap<CreateCognitoUserDTO, RegisterViewModel>().ReverseMap(); 
            CreateMap<CreateCognitoUserDTO, RegisterInternalUserViewModel>().ReverseMap();
            #endregion

            #region Cupon
            CreateMap<CuponDTO, CuponViewModel>().ReverseMap();
            CreateMap<CreateCuponDTO, CreateCuponViewModel>().ReverseMap();
            CreateMap<UpdateCuponDTO, UpdateCuponViewModel>().ReverseMap();
            #endregion

            #region Product
            CreateMap<ProductDTO, ProductViewModel>().ReverseMap();
            CreateMap<CreateProductDTO, CreateProductViewModel>().ReverseMap();
            CreateMap<UpdateProductDTO, UpdateProductViewModel>().ReverseMap();
            #endregion


        }
    }
}
