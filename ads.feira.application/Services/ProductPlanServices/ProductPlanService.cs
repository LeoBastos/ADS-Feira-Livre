using ads.feira.application.Interfaces.ProductPlanServices;
using ads.feira.domain.Entity.Accounts;
using ads.feira.domain.Interfaces.Accounts;
using ads.feira.domain.Interfaces.Products;

namespace ads.feira.application.Services.ProductPlanServices
{
    public class ProductPlanService : IProductPlanService
    {
        private readonly IProductRepository _productRepository;
        private readonly IAccountRepository _accountRepository;

        public ProductPlanService(IProductRepository productRepository, IAccountRepository accountRepository)
        {
            _productRepository = productRepository;
            _accountRepository = accountRepository;
        }

        public async Task<bool> CanAddProduct(string storeOwnerId)
        {
            var storeOwner = await _accountRepository.GetByIdAsync(storeOwnerId);
            if (storeOwner == null || storeOwner.UserType != UserType.StoreOwner)
            {
                throw new InvalidOperationException("Invalid store owner.");
            }

            int currentProductCount = await _productRepository.GetProductCountByStoreOwnerAsync(storeOwnerId);
            int limit = GetProductLimit(storeOwner.StoreOwnerPlan);

            return currentProductCount < limit;
        }

        private int GetProductLimit(StoreOwnerPlan plan)
        {
            return plan switch
            {
                StoreOwnerPlan.Bronze => 10,
                StoreOwnerPlan.Silver => 15,
                StoreOwnerPlan.Gold => 20,
                StoreOwnerPlan.Platinum => 30,
                _ => throw new ArgumentException("Invalid plan type")
            };
        }
    }
}
