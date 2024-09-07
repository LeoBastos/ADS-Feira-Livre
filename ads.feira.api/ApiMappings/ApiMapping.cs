using ads.feira.api.Helpers;
using ads.feira.api.Models.Categories;
using ads.feira.api.Models.Cupons;
using ads.feira.api.Models.Products;
using ads.feira.api.Models.Reviews;
using ads.feira.api.Models.Stores;
using ads.feira.application.DTO.Categories;
using ads.feira.application.DTO.Cupons;
using ads.feira.application.DTO.Products;
using ads.feira.application.DTO.Reviews;
using ads.feira.application.DTO.Stores;
using AutoMapper;

namespace ads.feira.api.ApiMappings
{
    public class ApiMapping : Profile
    {
        public ApiMapping()
        {
            #region Category            

            #endregion

            #region Accounts


            #endregion

            #region Cupon
            CreateMap<CuponDTO, CuponViewModel>().ReverseMap();
            CreateMap<CreateCuponDTO, CreateCuponViewModel>().ReverseMap();
            CreateMap<UpdateCuponDTO, UpdateCuponViewModel>().ReverseMap();
            #endregion

            #region Product
            
            #endregion

            #region Stores
            CreateMap<StoreDTO, StoreViewModel>().ReverseMap();
            CreateMap<CreateStoreDTO, CreateStoreViewModel>().ReverseMap();
            CreateMap<UpdateStoreDTO, UpdateStoreViewModel>().ReverseMap();
            #endregion

            #region Reviews
            CreateMap<ReviewDTO, ReviewViewModel>().ReverseMap();
            CreateMap<CreateReviewDTO, CreateReviewViewModel>().ReverseMap();
            CreateMap<UpdateReviewDTO, UpdateReviewViewModel>().ReverseMap();

            CreateMap<CreateReviewViewModel, CreateReviewDTO>().ReverseMap();
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
