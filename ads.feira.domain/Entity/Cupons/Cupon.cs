﻿using ads.feira.domain.Entity.Products;
using ads.feira.domain.Entity.Stores;
using ads.feira.domain.Enums.Cupons;
using ads.feira.domain.Validation;

namespace ads.feira.domain.Entity.Cupons
{
    public sealed class Coupon : BaseEntity
    {
        private Coupon() { }

        public Coupon(string name, string code, string description, DateTime expiration, decimal discount, DiscountTypeEnum discountType)
        {
            ValidateDomain(name, code, description, expiration, discount, discountType);
            Name = name;
            Code = code;
            Description = description;
            Expiration = expiration;
            Discount = discount;
            DiscountType = discountType;
        }

        public string Name { get; private set; }
        public string Code { get; private set; }
        public string Description { get; private set; }
        public DateTime Expiration { get; private set; }
        public decimal Discount { get; private set; }
        public DiscountTypeEnum DiscountType { get; private set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
        public ICollection<Store> Stores { get; set; } = new List<Store>();


        public void Update(string name, string code, string description, DateTime expiration, decimal discount, DiscountTypeEnum discountType)
        {
            ValidateDomain(name, code, description, expiration, discount, discountType);
        }

        public void Remove()
        {
            IsActive = false;
        }

        private void ValidateDomain(string name, string code, string description, DateTime expiration, decimal discount, DiscountTypeEnum discountType)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(name), "Nome não pode ser nulo.");
            DomainExceptionValidation.When(name.Length < 3, "Minimo de 3 caracteres.");

            DomainExceptionValidation.When(string.IsNullOrEmpty(code), "Código não pode ser nulo.");
            DomainExceptionValidation.When(code.Length < 2, "Minimo de 2 caracteres.");

            DomainExceptionValidation.When(string.IsNullOrEmpty(description), "Descrição não pode ser nulo.");
            DomainExceptionValidation.When(description.Length < 3, "Minimo de 3 caracteres.");

            DomainExceptionValidation.When(expiration < DateTime.UtcNow, "Data de expiração não pode ser menor do que a data atual");
            DomainExceptionValidation.When(discount <= 0, "Desconto não pode ser menor igual a zero");           

            Name = name;
            Code = code;
            Description = description;
            Expiration = expiration;
            Discount = discount;
            DiscountType = discountType;
        }
    }
}