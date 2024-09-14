using ads.feira.domain.Entity.Products;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace ads.feira.application.CQRS.Products.Commands
{
    public abstract class ProductCommand : IRequest<Product>
    {
        public string? Id { get; set; }
        public string StoreId { get; set; }       
        public string CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Assets { get; set; }
        public string? AssetsPath { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountedPrice { get; set; }
    }
}
