using ads.feira.domain.Entity.Products;
using ads.feira.domain.Entity.Stores;
using ads.feira.domain.Enums.Products;
using ads.feira.domain.Validation;

namespace ads.feira.domain.Entity.Categories
{
    public sealed class Category : BaseEntity, IEquatable<Category>
    {
        private Category() { }

        public Category(int id, string name, string description, string assets, int? parentCategoryId = null)
        {
            ValidateDomain(id, name, description, assets, parentCategoryId);
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Assets { get; private set; }
        public int? ParentCategoryId { get; private set; }

        public Category ParentCategory { get; private set; }
        public ICollection<Category> Subcategories { get; private set; } = new List<Category>();
        public ICollection<Product> Products { get; private set; } = new List<Product>();
        public ICollection<Store> Stores { get; private set; } = new List<Store>();

        public static Category Create(int id, string name, string description, string assets, int? parentCategoryId = null)
        {
            return new Category(id, name, description, assets, parentCategoryId);
        }

        public void Update(int id, string name, string description, string assets, int? parentCategoryId = null)
        {
            ValidateDomain(id, name, description, assets, parentCategoryId);
        }

        public void Remove()
        {
            IsActive = false;
        }

        public void AddSubcategory(Category subcategory)
        {
            DomainExceptionValidation.When(subcategory == null, "Subcategoria não pode ser nula.");
            Subcategories.Add(subcategory);
            subcategory.ParentCategoryId = this.Id;
        }

        public void RemoveSubcategory(Category subcategory)
        {
            DomainExceptionValidation.When(subcategory == null, "Subcategoria não pode ser nula.");
            Subcategories.Remove(subcategory);
            subcategory.ParentCategoryId = null;
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

        private void ValidateDomain(int id, string name, string description, string assets, int? parentCategoryId)
        {
            DomainExceptionValidation.When(id < 0, "Id inválido.");
            DomainExceptionValidation.When(string.IsNullOrEmpty(name), "Nome não pode ser nulo.");
            DomainExceptionValidation.When(name.Length < 3, "Minimo de 3 caracteres.");
            DomainExceptionValidation.When(string.IsNullOrEmpty(description), "Descrição não pode ser nulo.");
            DomainExceptionValidation.When(description.Length < 3, "Minimo de 3 caracteres.");
            DomainExceptionValidation.When(assets?.Length > 250, "Nome de imagem inválida, máximo 250 caracteres");
            DomainExceptionValidation.When(parentCategoryId.HasValue && parentCategoryId.Value < 0, "Id da categoria pai inválido.");

            Id = id;
            Name = name;
            Description = description;
            Assets = assets;
            ParentCategoryId = parentCategoryId;
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
