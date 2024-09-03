using ads.feira.api.Helpers;
using ads.feira.api.Models.Categories;
using ads.feira.api.Models.Cupons;
using ads.feira.api.Models.Products;
using ads.feira.api.Models.Stores;
using ads.feira.application.DTO.Categories;
using ads.feira.application.DTO.Cupons;
using ads.feira.application.DTO.Products;
using ads.feira.application.DTO.Stores;
using AutoMapper;

namespace ads.feira.api.ApiMappings
{
    public class ApiMapping : Profile
    {
        public ApiMapping()
        {
            #region Category
            CreateMap<CategoryDTO, CategoryViewModel>().ForMember(dest => dest.Assets, opt => opt.Ignore());
            CreateMap<CreateCategoryDTO, CreateCategoryViewModel>().ReverseMap();
            CreateMap<UpdateCategoryDTO, UpdateCategoryViewModel>().ReverseMap();

            CreateMap<UpdateCategoryViewModel, UpdateCategoryDTO>()
               .ForMember(dest => dest.Assets, opt => opt.MapFrom<UploadImageResolver>());

            #endregion

            #region Accounts


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

            CreateMap<CreateProductViewModel, CreateProductDTO>().ReverseMap();
            #endregion

            #region Stores
            CreateMap<StoreDTO, StoreViewModel>().ReverseMap();
            CreateMap<CreateStoreDTO, CreateStoreViewModel>().ReverseMap();
            CreateMap<UpdateStoreDTO, UpdateStoreViewModel>().ReverseMap();
            #endregion
        }

    }

    public class UploadImageResolver : IValueResolver<UpdateCategoryViewModel, UpdateCategoryDTO, string>
    {
        public string Resolve(UpdateCategoryViewModel source, UpdateCategoryDTO destination, string destMember, ResolutionContext context)
        {
            if (source.Assets != null)
            {

                return FilesExtensions.UploadImage(source.Assets).Result;
            }
            return null;
        }
    }
}
