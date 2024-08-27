using ads.feira.domain.Entity.Identity;
using ads.feira.domain.Entity.Products;
using ads.feira.domain.Entity.Stores;
using ads.feira.domain.Enums.Products;
using ads.feira.domain.Validation;

namespace ads.feira.domain.Entity.Categories
{
    public sealed class Category : BaseEntity
    {
        private Category() { }

        public Category(string name, TypeCategoryEnum type, ApplicationUser createdBy, string description, string assets)
        {
            ValidateDomain(name, type, createdBy, description, assets);
        }

        public string Name { get; private set; }
        public TypeCategoryEnum Type { get; private set; }
        public string Description { get; private set; }
        public string Assets { get; private set; }

        public ApplicationUser CreatedBy { get; private set; }
        public ICollection<Store> Stores { get; private set; } = new List<Store>();
        public ICollection<Product> Products { get; private set; } = new List<Product>();

        public void Update(string name, TypeCategoryEnum type, ApplicationUser createdBy, string description, string assets)
        {
            ValidateDomain(name, type, createdBy, description, assets);
        }

        public void Remove()
        {
            IsActive = false;
        }

        private void ValidateDomain(string name, TypeCategoryEnum type, ApplicationUser createdBy, string description, string assets)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(name), "Nome não pode ser nulo.");
            DomainExceptionValidation.When(name.Length < 3, "Minimo de 3 caracteres.");

            DomainExceptionValidation.When(createdBy == null, "O usuário que criou a categoria não pode ser nulo.");

            DomainExceptionValidation.When(string.IsNullOrEmpty(description), "Descrição não pode ser nulo.");
            DomainExceptionValidation.When(description.Length < 3, "Minimo de 3 caracteres.");

            DomainExceptionValidation.When(assets?.Length > 250, "Nome de imagem inválida, máximo 250 caracteres");

            Name = name;
            Type = type;
            CreatedBy = createdBy;
            Description = description;
            Assets = assets;
        }
    }
}
