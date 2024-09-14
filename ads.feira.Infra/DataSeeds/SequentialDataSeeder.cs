using ads.feira.domain.Entity.Accounts;
using ads.feira.domain.Entity.Categories;
using ads.feira.domain.Entity.Products;
using ads.feira.domain.Entity.Stores;
using ads.feira.Infra.Context;
using Bogus;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ads.feira.Infra.DataSeeds
{
    public class SequentialDataSeeder
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Account> _userManager;
        private readonly ILogger<SequentialDataSeeder> _logger;
        private readonly Faker _faker;

        public SequentialDataSeeder(
            ApplicationDbContext context,
            UserManager<Account> userManager,
            ILogger<SequentialDataSeeder> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
            _faker = new Faker();
        }

        public async Task SeedDataSequentially(int totalAccounts = 150, int productsPerStore = 10)
        {
            for (int i = 0; i < totalAccounts; i++)
            {
                // 1. Criar Account
                var account = CreateAccount();
                var result = await _userManager.CreateAsync(account, "Password123!");
                if (!result.Succeeded)
                {
                    _logger.LogError($"Failed to create account for {account.Email}: {string.Join(", ", result.Errors)}");
                    continue;
                }

                // 2. Criar Category (uma nova a cada 5 iterações para não ter muitas categorias)
                if (i % 3 == 0)
                {
                    var category = CreateCategory();
                    await _context.Categories.AddAsync(category);
                    await _context.SaveChangesAsync();
                }

                // Pegar uma categoria aleatória existente
                var randomCategory = await _context.Categories.OrderBy(r => Guid.NewGuid()).FirstAsync();

                // 3. Criar Store (apenas se a Account for StoreOwner)
                if (account.UserType == UserType.StoreOwner)
                {
                    var store = CreateStore(account.Id, randomCategory.Id);
                    await _context.Stores.AddAsync(store);
                    await _context.SaveChangesAsync();

                    // 4. Criar Products para esta Store
                    for (int j = 0; j < productsPerStore; j++)
                    {
                        var product = CreateProduct(store.Id, randomCategory.Id);
                        await _context.Products.AddAsync(product);
                    }
                    await _context.SaveChangesAsync();
                }

                _logger.LogInformation($"Created Account {i + 1}/{totalAccounts}, potentially with Store and Products");
            }
        }

        private Account CreateAccount()
        {
            return new Account
            {
                Id = Guid.NewGuid().ToString(),
                Name = _faker.Name.FullName(),
                Email = _faker.Internet.Email(),
                UserName = _faker.Internet.UserName(),
                Assets = _faker.Image.PicsumUrl(),
                TosAccept = true,
                PrivacyAccept = true,
                IsActive = true,
                UserType = _faker.Random.Bool(0.3f) ? UserType.StoreOwner : UserType.Customer,
                //UserType = UserType.StoreOwner,
                StoreOwnerPlan = _faker.PickRandom<StoreOwnerPlan>()
            };
        }

        private Category CreateCategory()
        {
            return Category.Create(
                Guid.NewGuid().ToString(),
                _faker.Commerce.Categories(1)[0],
                _faker.Commerce.ProductDescription(),
                _faker.Image.PicsumUrl()
            );
        }

        private Store CreateStore(string storeOwnerId, string categoryId)
        {
            return Store.Create(
                Guid.NewGuid().ToString(),
                storeOwnerId,
                _faker.Company.CompanyName(),
                categoryId,
                _faker.Company.CatchPhrase(),
                _faker.Image.PicsumUrl(),
                _faker.Random.Number(1, 1000).ToString(),
                _faker.Random.Bool(0.1f),
                _faker.Address.FullAddress()
            );
        }

        private Product CreateProduct(string storeId, string categoryId)
        {
            return Product.Create(
                Guid.NewGuid().ToString(),
                storeId,
                categoryId,
                _faker.Commerce.ProductName(),
                _faker.Commerce.ProductDescription(),
                _faker.Image.PicsumUrl(),
                _faker.Random.Decimal(1, 1000),
                _faker.Random.Decimal(0, 10)
            );
        }
    }


}
