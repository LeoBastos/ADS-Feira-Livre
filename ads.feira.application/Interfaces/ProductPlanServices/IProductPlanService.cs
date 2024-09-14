namespace ads.feira.application.Interfaces.ProductPlanServices
{
    public interface IProductPlanService
    {
        Task<bool> CanAddProduct(string storeOwnerId);
    }
}
