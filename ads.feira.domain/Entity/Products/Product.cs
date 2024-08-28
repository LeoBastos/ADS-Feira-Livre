using ads.feira.domain.Entity.Categories;
using ads.feira.domain.Entity.Cupons;
using ads.feira.domain.Entity.Stores;
using ads.feira.domain.Enums.Cupons;
using ads.feira.domain.Validation;

namespace ads.feira.domain.Entity.Products
{
    public sealed class Product : BaseEntity, IEquatable<Product>
    {
        private Product(){}

        public Product(int id, string storeId, string categoryId, string name, string description, string assets, decimal price, decimal? discountedPrice)
        {
            ValidateDomain(id, storeId, categoryId, name, description, assets, price, discountedPrice);           
        }

        public string StoreId { get; private set; }
        public string CategoryId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Assets { get; private set; }
        public decimal Price { get; private set; }
        public decimal? DiscountedPrice { get; private set; }

        public Store Store { get; set; }
        public Category Category { get; set; }
        public ICollection<Cupon> AvailableCoupons { get; set; }  = new List<Cupon>();


        public static Product Create(int id, string storeId, string categoryId, string name, string description, string assets, decimal price, decimal? discountedPrice)
        {
            return new Product(id, storeId, categoryId, name, description, assets, price, discountedPrice);
        }

        public void Update(int id, string storeId, string categoryId, string name, string description, string assets, decimal price, decimal? discountedPrice)
        {
            ValidateDomain(id, storeId, categoryId, name, description, assets, price, discountedPrice);
        }

        public void Remove()
        {
            IsActive = false;
        }

        public void AddCoupon(Cupon coupon)
        {
            DomainExceptionValidation.When(coupon == null, "O cupom não pode ser nulo.");
            DomainExceptionValidation.When(coupon.Expiration < DateTime.UtcNow, "O cupom não é válido ou está expirado.");
            AvailableCoupons.Add(coupon);
            UpdateDiscountedPrice();
        }

        public void RemoveCoupon(Cupon coupon)
        {
            DomainExceptionValidation.When(coupon == null, "O cupom não pode ser nulo.");
            AvailableCoupons.Remove(coupon);
            UpdateDiscountedPrice();
        }

        public void UpdateDiscountedPrice()
        {
            decimal discountedPrice = Price;

            foreach (var coupon in AvailableCoupons.Where(c => c.IsValid()))
            {
                switch (coupon.DiscountType)
                {
                    case DiscountTypeEnum.Percentage:
                        discountedPrice -= discountedPrice * (coupon.Discount / 100);
                        break;
                    case DiscountTypeEnum.Fixed:
                        discountedPrice -= coupon.Discount;
                        break;
                }
            }

            DiscountedPrice = Math.Max(discountedPrice, 0);
        }

        private void ValidateDomain(int id, string storeId, string categoryId, string name, string description, string assets, decimal price, decimal? discountedPrice)
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
            DiscountedPrice = discountedPrice;
        }

        public bool Equals(Product other)
        {
            if (other is null)
                return false;

            return Id == other.Id && Name == other.Name && StoreId == other.StoreId;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Product);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, StoreId);
        }
    }
}
