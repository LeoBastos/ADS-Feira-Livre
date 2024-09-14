using ads.feira.domain.Entity.Categories;
using ads.feira.domain.Entity.Stores;
using MediatR;

namespace ads.feira.application.CQRS.Stores.Commands
{
    public abstract class StoreCommand : IRequest<Store>
    {
        public string? Id { get; set; }
        public string Name { get; set; }
        public string CategoryId { get; set; }
        public string StoreOwnerId { get; set; }
        public string Description { get; set; }
        public string Assets { get; set; }
        public string? AssetsPath { get; set; }
        public string StoreNumber { get; set; }
        public bool HasDebt { get; set; }
        public string Locations { get; set; }

        public Category Category { get; set; }
    }

}
