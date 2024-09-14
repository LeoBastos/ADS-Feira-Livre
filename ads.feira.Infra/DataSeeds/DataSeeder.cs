using ads.feira.domain.Entity.Accounts;
using ads.feira.domain.Entity.Categories;
using ads.feira.domain.Entity.Products;
using ads.feira.domain.Entity.Stores;
using Bogus;

namespace ads.feira.Infra.DataSeeds
{
    public class DataSeeder
    {
        public List<Account> GenerateAccounts(int count)
        {
            var accounts = new Faker<Account>()
                .RuleFor(a => a.Id, f => Guid.NewGuid().ToString())
                .RuleFor(a => a.Name, f => f.Name.FullName())
                .RuleFor(a => a.Email, f => f.Internet.Email())
                .RuleFor(a => a.UserName, (f, a) => a.Email)
                .RuleFor(a => a.Assets, f => f.Image.ToString())
                .RuleFor(a => a.TosAccept, f => true)
                .RuleFor(a => a.PrivacyAccept, f => true)
                .RuleFor(a => a.IsActive, f => true)
                .RuleFor(a => a.UserType, f => UserType.StoreOwner)
                .RuleFor(a => a.StoreOwnerPlan, f => f.PickRandom<StoreOwnerPlan>())
                .RuleFor(a => a.NormalizedUserName, (f, a) => a.UserName.ToUpper())
                .RuleFor(a => a.NormalizedEmail, (f, a) => a.Email.ToUpper())
                .RuleFor(a => a.EmailConfirmed, f => true)
                .RuleFor(a => a.LockoutEnabled, f => true)
                .RuleFor(a => a.SecurityStamp, f => Guid.NewGuid().ToString())
                .RuleFor(a => a.ConcurrencyStamp, f => Guid.NewGuid().ToString())
                .RuleFor(a => a.PasswordHash, f => Guid.NewGuid().ToString())
                .Generate(count);

            return accounts;
        }
        public List<Category> GenerateCategories(int count)
        {
            var categories = new List<Category>();
            var faker = new Faker();

            for (int i = 0; i < count; i++)
            {
                var id = Guid.NewGuid().ToString();
                var name = faker.Commerce.Categories(1)[0];
                var description = faker.Commerce.ProductDescription();
                var assets = faker.Image.PicsumUrl();

                var category = Category.Create(id, name, description, assets);
                categories.Add(category);
            }

            return categories;
        }

        public List<Store> GenerateStores(int count, List<Account> accounts, List<Category> categories)
        {
            var stores = new List<Store>();
            var faker = new Faker();

            //Filtrar accounts
            var storeOwners = accounts
                .Where(a => a.UserType == UserType.StoreOwner && a.IsActive)
                .DistinctBy(a => a.Id)
                .ToList();

            // Filtrar categorias ativas com IDs únicos
            var activeCategories = categories
                .Where(c => c.IsActive)
                .DistinctBy(c => c.Id)
                .ToList();

            // Garantir que temos StoreOwners suficientes
            int availableOwners = Math.Min(count, storeOwners.Count);

            for (int i = 0; i < availableOwners; i++)
            {  
                var id = Guid.NewGuid().ToString();
                var storeOwnerId = faker.PickRandom(accounts.Where(a => a.UserType == UserType.StoreOwner)).Id;
                var name = faker.Company.CompanyName();
                var categoryId = faker.PickRandom(categories).Id;
                var description = faker.Company.CatchPhrase();
                var assets = faker.Image.PicsumUrl();
                var storeNumber = faker.Random.Number(1, 1000).ToString();
                var hasDebt = faker.Random.Bool();
                var locations = faker.Address.FullAddress();

                var store = Store.Create(id, storeOwnerId, name, categoryId, description, assets, storeNumber, hasDebt, locations);

                stores.Add(store);
            }

            return stores;
        }


        public List<Product> GenerateProducts(int count, List<Category> categories, List<Store> stores, List<Account> accounts)
        {

            var products = new List<Product>();
            var faker = new Faker();

            for (int i = 0; i < count; i++)
            {
                var id = Guid.NewGuid().ToString();
                var storeId = faker.PickRandom(accounts.Where(a => a.UserType == UserType.StoreOwner)).Id;
                var categoryId = faker.PickRandom(categories).Id;
                var name = faker.Company.CompanyName();               
                var description = faker.Company.CatchPhrase();
                var assets = faker.Image.PicsumUrl();
                var price = faker.Random.Decimal(1, 1000);
                var discountedPrice = faker.Random.Decimal(1, 10);



                var product = Product.Create(id, storeId, categoryId, name, description, assets, price, discountedPrice);
                products.Add(product);
            }

            return products;          
        }
    }
}
