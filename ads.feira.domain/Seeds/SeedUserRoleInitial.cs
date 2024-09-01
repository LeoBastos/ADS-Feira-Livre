using ads.feira.domain.Entity.Accounts;
using Microsoft.AspNetCore.Identity;

namespace ads.feira.domain.Seeds
{
    public class SeedUserRoleInitial : ISeedUserRoleInitial
    {
        private readonly UserManager<Account> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SeedUserRoleInitial(UserManager<Account> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedRolesAsync()
        {
            if (!await _roleManager.RoleExistsAsync("Customer"))
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Customer";
                role.NormalizedName = "CUSTOMER";
                role.ConcurrencyStamp = Guid.NewGuid().ToString();

                IdentityResult roleResult = await _roleManager.CreateAsync(role);
            }

            if (!await _roleManager.RoleExistsAsync("StoreOwner"))
            {
                IdentityRole role = new IdentityRole();
                role.Name = "StoreOwner";
                role.NormalizedName = "STOREOWNER";
                role.ConcurrencyStamp = Guid.NewGuid().ToString();

                IdentityResult roleResult = await _roleManager.CreateAsync(role);
            }

            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Admin";
                role.NormalizedName = "ADMIN";
                role.ConcurrencyStamp = Guid.NewGuid().ToString();
                IdentityResult roleResult = await _roleManager.CreateAsync(role);
            }
        }

        public async Task SeedUsersAsync()
        {
            if (await _userManager.FindByEmailAsync("customer@localhost") == null)
            {
                Account user = new Account();
                user.UserName = "customer@localhost";
                user.Email = "customer@localhost";
                user.NormalizedUserName = "CUSTOMER@LOCALHOST";
                user.NormalizedEmail = "CUSTOMER@LOCALHOST";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.Name = "Customer";
                user.Assets = "~/images/noimage.png";
                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = await _userManager.CreateAsync(user, "Numsey#2023");

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Customer");
                }


            }

            if (await _userManager.FindByEmailAsync("storeOwner@localhost") == null)
            {
                Account user = new Account();
                user.UserName = "storeOwner@localhost";
                user.Email = "storeOwner@localhost";
                user.NormalizedUserName = "STOREOWNER@LOCALHOST";
                user.NormalizedEmail = "STOREOWNER@LOCALHOST";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.Name = "StoreOwner";
                user.Assets = "~/images/noimage.png";
                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = await _userManager.CreateAsync(user, "Numsey#2023");

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "StoreOwner");
                }
            }

            if (await _userManager.FindByEmailAsync("admin@localhost") == null)
            {
                Account user = new Account();
                user.UserName = "admin@localhost";
                user.Email = "admin@localhost";
                user.NormalizedUserName = "ADMIN@LOCALHOST";
                user.NormalizedEmail = "ADMIN@LOCALHOST";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.Name = "Administrador";
                user.Assets = "~/images/noimage.png";
                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = await _userManager.CreateAsync(user, "Numsey#2023");

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Admin");
                }
            }
        }
    }
}
