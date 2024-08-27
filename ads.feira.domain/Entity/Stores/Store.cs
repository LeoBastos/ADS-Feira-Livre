using ads.feira.domain.Entity.Categories;
using ads.feira.domain.Entity.Cupons;
using ads.feira.domain.Entity.Products;
using ads.feira.domain.Entity.Reviews;
using ads.feira.domain.Validation;

namespace ads.feira.domain.Entity.Stores
{
    public class Store : BaseEntity
    {
        private Store() { }

        public Store(string storeOwner, string name, string categoryId, string description, string assets, string storeNumber, bool hasDebt, string locations)
        {
            ValidateDomain(storeOwner, name, categoryId, description, assets, storeNumber, hasDebt, locations);
        }

        public string StoreOwner { get; set; }
        public string Name { get; set; }
        public string CategoryId { get; set; }
        public string Description { get; set; }
        public string Assets { get; set; }
        public string StoreNumber { get; set; }
        public bool HasDebt { get; set; }
        public string Locations { get; set; }

        public Category Category { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<Coupon> AvailableCoupons { get; set; } = new List<Coupon>();
        public ICollection<User> Users { get; set; } = new List<User>();

        public void Update(string storeOwner, string name, string categoryId, string description, string assets, string storeNumber, bool hasDebt, string locations)
        {
            ValidateDomain(storeOwner, name, categoryId, description, assets, storeNumber, hasDebt, locations);
        }

        public void Remove()
        {
            IsActive = false;
        }

        private void ValidateDomain(string storeOwner, string name, string categoryId, string description, string assets, string storeNumber, bool hasDebt, string locations)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(storeOwner), "Lojista não pode ser nulo.");
            DomainExceptionValidation.When(storeOwner.Length < 3, "Minimo de 3 caracteres.");

            DomainExceptionValidation.When(string.IsNullOrEmpty(name), "Nome não pode ser nulo.");
            DomainExceptionValidation.When(name.Length < 3, "Minimo de 3 caracteres.");

            DomainExceptionValidation.When(string.IsNullOrEmpty(categoryId), "Categoria Id não pode ser nulo.");

            DomainExceptionValidation.When(string.IsNullOrEmpty(description), "Descrição não pode ser nulo.");
            DomainExceptionValidation.When(description.Length < 3, "Minimo de 3 caracteres.");

            DomainExceptionValidation.When(string.IsNullOrEmpty(storeNumber), "Número da Loja não pode ser nulo.");

            DomainExceptionValidation.When(assets?.Length > 250, "Nome de imagem inválida, máximo 250 caracteres");

            DomainExceptionValidation.When(string.IsNullOrEmpty(locations), "Local não pode ser nulo.");
            DomainExceptionValidation.When(locations.Length < 1, "Minimo de 3 caracteres.");


            StoreOwner = storeOwner;
            Name = name;
            CategoryId = categoryId;
            Description = description;
            Assets = assets;
            StoreNumber = storeNumber;
            HasDebt = hasDebt;
            Locations = locations;
        }
    }
}
