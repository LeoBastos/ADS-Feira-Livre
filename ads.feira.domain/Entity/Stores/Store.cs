using ads.feira.domain.Entity.Categories;
using ads.feira.domain.Entity.Cupons;
using ads.feira.domain.Entity.Identity;
using ads.feira.domain.Entity.Products;
using ads.feira.domain.Entity.Reviews;
using ads.feira.domain.Validation;

namespace ads.feira.domain.Entity.Stores
{
    public class Store : BaseEntity
    {
        private Store(){}

        public Store(int id, string storeOwner, string name, int categoryId, string description, string assets, string storeNumber, bool hasDebt, string locations)
        {
            ValidateDomain(id, storeOwner, name, categoryId, description, assets, storeNumber, hasDebt, locations);           
        }

        public string StoreOwner { get; private set; }
        public string Name { get; private set; }
        public int CategoryId { get; private set; }
        public string Description { get; private set; }
        public string Assets { get; private set; }
        public string StoreNumber { get; private set; }
        public bool HasDebt { get; private set; }
        public string Locations { get; private set; }

        public Category Category { get; set; }

        public ICollection<Product> Products { get; private set; } = new List<Product>();
        public ICollection<Review> Reviews { get; private set; } = new List<Review>();
        public ICollection<Cupon> AvailableCupons { get; private set; } = new List<Cupon>();
        public ICollection<CognitoUser> Users { get; private set; } = new List<CognitoUser>();       


        public static Store Create(int id, string storeOwner, string name, int categoryId, string description, string assets, string storeNumber, bool hasDebt, string locations)
        {
            return new Store(id, storeOwner, name, categoryId, description, assets, storeNumber, hasDebt, locations);
        }

        public void Update(int id, string storeOwner, string name, int categoryId, string description, string assets, string storeNumber, bool hasDebt, string locations)
        {
            ValidateDomain(id, storeOwner, name, categoryId, description, assets, storeNumber, hasDebt, locations);
        }

        public void Remove()
        {
            IsActive = false;
        }

        private void ValidateDomain(int id, string storeOwner, string name, int categoryId, string description, string assets, string storeNumber, bool hasDebt, string locations)
        {
            DomainExceptionValidation.When(id < 0, "Id inválido.");

            DomainExceptionValidation.When(string.IsNullOrEmpty(storeOwner), "Lojista não pode ser nulo.");
            DomainExceptionValidation.When(storeOwner.Length < 3, "Minimo de 3 caracteres.");

            DomainExceptionValidation.When(string.IsNullOrEmpty(name), "Nome não pode ser nulo.");
            DomainExceptionValidation.When(name.Length < 3, "Minimo de 3 caracteres.");

            DomainExceptionValidation.When(categoryId < 0, "Id inválido.");

            DomainExceptionValidation.When(string.IsNullOrEmpty(description), "Descrição não pode ser nulo.");
            DomainExceptionValidation.When(description.Length < 3, "Minimo de 3 caracteres.");

            DomainExceptionValidation.When(string.IsNullOrEmpty(storeNumber), "Número da Loja não pode ser nulo.");

            DomainExceptionValidation.When(assets?.Length > 250, "Nome de imagem inválida, máximo 250 caracteres");

            DomainExceptionValidation.When(string.IsNullOrEmpty(locations), "Local não pode ser nulo.");
            DomainExceptionValidation.When(locations.Length < 1, "Minimo de 3 caracteres.");

            Id = id;
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
