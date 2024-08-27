using ads.feira.domain.Entity.Categories;
using ads.feira.domain.Entity.Cupons;
using ads.feira.domain.Entity.Stores;
using ads.feira.domain.Enums.Cupons;
using ads.feira.domain.Validation;

namespace ads.feira.domain.Entity.Products
{
    public sealed class Product : BaseEntity
    {
        private Product(){}

        public Product(int id, string storeId, string categoryId, string name, string description, string assets, decimal price)
        {
            ValidateDomain(id, storeId, categoryId, name, description, assets, price);           
        }

        public string StoreId { get; private set; }
        public string CategoryId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Assets { get; private set; }
        public decimal Price { get; private set; }

        public Store Stores { get; set; }
        public Category Categories { get; set; }
        public ICollection<Cupon> AvailableCoupons { get; set; }  = new List<Cupon>();


        public static Product Create(int id, string storeId, string categoryId, string name, string description, string assets, decimal price)
        {
            return new Product(id, storeId, categoryId, name, description, assets, price);
        }

        public void Update(int id, string storeId, string categoryId, string name, string description, string assets, decimal price)
        {
            ValidateDomain(id, storeId, categoryId, name, description, assets, price);
        }

        public void Remove()
        {
            IsActive = false;
        }

        public void ApplyAvailableCoupons(Cupon coupon)
        {
            if (coupon == null)
            {
                throw new ArgumentNullException(nameof(coupon), "O cupom não pode ser nulo.");
            }

            if (coupon.Expiration < DateTime.UtcNow)
            {
                throw new DomainExceptionValidation("Data de expiração não pode ser menor do que a data atual.");
            }

            decimal discountAmount = 0;

            switch (coupon.DiscountType)
            {
                case DiscountTypeEnum.Percentage:
                    discountAmount = Price * (coupon.Discount / 100);
                    break;

                case DiscountTypeEnum.Fixed:
                    discountAmount = coupon.Discount;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(coupon.DiscountType), "Tipo de desconto inválido.");
            }

            if (discountAmount > Price)
            {
                throw new DomainExceptionValidation("O valor do desconto não pode ser maior que o preço do produto.");
            }

            Price -= discountAmount;

            if (Price < 0)
            {
                Price = 0;
            }
            
            AvailableCoupons.Add(coupon);
        }

        private void ValidateDomain(int id, string storeId, string categoryId, string name, string description, string assets, decimal price)
        {
            DomainExceptionValidation.When(id < 0, "Id inválido.");
            DomainExceptionValidation.When(string.IsNullOrEmpty(storeId), "StoreId não pode ser nulo.");
            DomainExceptionValidation.When(string.IsNullOrEmpty(categoryId), "CategoryId não pode ser nulo.");

            DomainExceptionValidation.When(string.IsNullOrEmpty(name), "Nome não pode ser nulo.");
            DomainExceptionValidation.When(name.Length < 3, "Minimo de 3 caracteres.");

            DomainExceptionValidation.When(string.IsNullOrEmpty(description), "Descrição não pode ser nulo.");
            DomainExceptionValidation.When(description.Length < 3, "Minimo de 3 caracteres.");

            DomainExceptionValidation.When(assets?.Length > 250, "Nome de imagem inválida, máximo 250 caracteres");

            DomainExceptionValidation.When(price < 0, "Valor de preço inválido");
            Id = id;
            StoreId = storeId;
            CategoryId = categoryId;           
            Name = name;
            Description = description;
            Assets = assets;
            Price = price;
        }
    }
}
