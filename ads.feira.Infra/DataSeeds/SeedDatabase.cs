using ads.feira.domain.Entity.Accounts;
using ads.feira.Infra.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ads.feira.Infra.DataSeeds
{
    public class SeedDatabase : ISeedDatabase
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<SeedDatabase> _logger;

        public SeedDatabase(IServiceProvider serviceProvider, ILogger<SeedDatabase> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task SeedDataDB(bool forceReseed = true)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Account>>();

                if (!forceReseed && await context.Accounts.AnyAsync())
                {
                    _logger.LogInformation("Database already contains accounts. Skipping seeding.");
                    return;
                }

                _logger.LogInformation("Starting sequential database seeding...");

                var sequentialSeeder = new SequentialDataSeeder(
                    context,
                    userManager,
                    scope.ServiceProvider.GetRequiredService<ILogger<SequentialDataSeeder>>()
                );

                await sequentialSeeder.SeedDataSequentially(150, 10);

                _logger.LogInformation("Sequential database seeding completed successfully.");
            }

            //public async Task SeedDataDB(bool forceReseed = true)
            //{
            //    using (var scope = _serviceProvider.CreateScope())
            //    {
            //        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            //        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Account>>();

            //        if (!forceReseed && await context.Accounts.AnyAsync())
            //        {
            //            _logger.LogInformation("Database already contains accounts. Skipping seeding.");
            //            return;
            //        }

            //        _logger.LogInformation("Starting database seeding...");

            //        var seeder = new DataSeeder();

            //        // Gerar dados
            //        var accounts = seeder.GenerateAccounts(5);
            //        var categories = seeder.GenerateCategories(5);


            //        // Adicionar e salvar contas
            //        foreach (var account in accounts)
            //        {
            //            var result = await userManager.CreateAsync(account, "Password123!"); // Senha padrão
            //            if (!result.Succeeded)
            //            {
            //                _logger.LogError($"Failed to create account for {account.Email}: {string.Join(", ", result.Errors)}");
            //            }
            //        }

            //        // Adicionar e salvar categorias
            //        await context.Categories.AddRangeAsync(categories);
            //        await context.SaveChangesAsync();

            //        // Gerar e salvar lojas
            //        var stores = seeder.GenerateStores(5, accounts, categories);
            //        await context.Stores.AddRangeAsync(stores);
            //        await context.SaveChangesAsync();

            //        _logger.LogInformation($"Added {stores.Count} stores to the context.");

            //        // Verificar se as lojas foram realmente salvas
            //        var storeCount = await context.Stores.CountAsync();
            //        _logger.LogInformation($"Number of stores in the database after saving: {storeCount}");

            //        if (storeCount > 0)
            //        {
            //            // Gerar e salvar produtos
            //            var products = seeder.GenerateProducts(5, categories, stores, accounts);
            //            await context.Products.AddRangeAsync(products);
            //            await context.SaveChangesAsync();

            //            _logger.LogInformation($"Added {products.Count} products to the context.");
            //        }
            //        else
            //        {
            //            _logger.LogError("No stores were saved to the database. Skipping product generation.");
            //        }

            //        _logger.LogInformation("Database seeding completed successfully.");
            //    }
            //}
        }
    }
}
