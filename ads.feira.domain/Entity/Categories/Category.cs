using ads.feira.domain.Entity.Products;
using ads.feira.domain.Entity.Stores;
using ads.feira.domain.Validation;

namespace ads.feira.domain.Entity.Categories
{
    public sealed class Category : BaseEntity, IEquatable<Category>
    {
        private Category(){ }

        public Category(string id, string name, string description, string assets)
        {
            ValidateDomain(id, name, description, assets);
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Assets { get; private set; }

        public ICollection<Product> Products { get; private set; } = new List<Product>();
        public ICollection<Store> Stores { get; private set; } = new List<Store>();

        public static Category Create(string id, string name, string description, string assets)
        {
            return new Category(id, name, description, assets);
        }

        public void Update(string id, string name, string description, string assets)
        {
            ValidateDomain(id, name, description, assets);
        }

        public void Remove()
        {
            IsActive = false;
        }

        public void AddProduct(Product product)
        {
            DomainExceptionValidation.When(product == null, "Produto não pode ser nulo.");
            Products.Add(product);
        }

        public void RemoveProduct(Product product)
        {
            DomainExceptionValidation.When(product == null, "Produto não pode ser nulo.");
            Products.Remove(product);
        }

        private void ValidateDomain(string id, string name, string description, string assets)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(name), "Nome não pode ser nulo.");
            DomainExceptionValidation.When(name.Length < 3, "Minimo de 3 caracteres.");
            DomainExceptionValidation.When(string.IsNullOrEmpty(description), "Descrição não pode ser nulo.");
            DomainExceptionValidation.When(description.Length < 3, "Minimo de 3 caracteres.");
            DomainExceptionValidation.When(assets?.Length > 250, "Nome de imagem inválida, máximo 250 caracteres");

            Id = id;
            Name = name;
            Description = description;
            Assets = assets;
        }

        public bool Equals(Category other)
        {
            if (other is null)
                return false;

            return Id == other.Id && Name == other.Name;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Category);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name);
        }
    }
}
