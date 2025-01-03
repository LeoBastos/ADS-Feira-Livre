﻿using ads.feira.domain.Entity.Accounts;
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

        public Store(string id, string storeOwnerId, string name, string categoryId, string description, string assets, string storeNumber, bool hasDebt, string locations)
        {
            ValidateDomain(id, storeOwnerId, name, categoryId, description, assets, storeNumber, hasDebt, locations);
        }

        public string StoreOwnerId { get; private set; }
        public string Name { get; private set; }
        public string CategoryId { get; private set; }
        public string Description { get; private set; }
        public string Assets { get; private set; }
        public string StoreNumber { get; private set; }
        public bool HasDebt { get; private set; }
        public string Locations { get; private set; }

        public Category Category { get; set; }
        public Account StoreOwner { get; set; }
       
        public ICollection<Product> Products { get; private set; } = new List<Product>();
        public ICollection<Review> Reviews { get; private set; } = new List<Review>();
        public ICollection<Cupon> AvailableCupons { get; private set; } = new List<Cupon>();


        public static Store Create(string id, string storeOwnerId, string name, string categoryId, string description, string assets, string storeNumber, bool hasDebt, string locations)
        {
            return new Store(id, storeOwnerId, name, categoryId, description, assets, storeNumber, hasDebt, locations);
        }

        public void Update(string id, string storeOwnerId, string name, string categoryId, string description, string assets, string storeNumber, bool hasDebt, string locations)
        {
            ValidateDomain(id, storeOwnerId, name, categoryId, description, assets, storeNumber, hasDebt, locations);
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

        private void ValidateDomain(string id, string storeOwnerId, string name, string categoryId, string description, string assets, string storeNumber, bool hasDebt, string locations)
        {
           
            DomainExceptionValidation.When(storeOwnerId.ToString().Length < 3, "Minimo de 3 caracteres.");

            DomainExceptionValidation.When(string.IsNullOrEmpty(name), "Nome não pode ser nulo.");
            DomainExceptionValidation.When(name.Length < 3, "Minimo de 3 caracteres.");
            DomainExceptionValidation.When(string.IsNullOrEmpty(description), "Descrição não pode ser nulo.");
            DomainExceptionValidation.When(description.Length < 3, "Minimo de 3 caracteres.");

            DomainExceptionValidation.When(string.IsNullOrEmpty(storeNumber), "Número da Loja não pode ser nulo.");

            DomainExceptionValidation.When(assets?.Length > 250, "Nome de imagem inválida, máximo 250 caracteres");

            DomainExceptionValidation.When(string.IsNullOrEmpty(locations), "Local não pode ser nulo.");
            DomainExceptionValidation.When(locations.Length < 1, "Minimo de 3 caracteres.");

            DomainExceptionValidation.When(hasDebt == true, "Loja possui Débito.");

            Id = id;
            StoreOwnerId = storeOwnerId;
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
