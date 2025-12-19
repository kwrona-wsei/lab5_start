using Microsoft.AspNetCore.Identity;

namespace lab5_start.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            string[] roleNames = ["Santa", "Elf"];
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            var santaUser = await userManager.FindByEmailAsync("santa@northpole.com");
            if (santaUser == null)
            {
                santaUser = new IdentityUser { UserName = "santa@northpole.com", Email = "santa@northpole.com", EmailConfirmed = true };
                await userManager.CreateAsync(santaUser, "HoHoHo123!");
                await userManager.AddToRoleAsync(santaUser, "Santa");
            }

            var elfUser = await userManager.FindByEmailAsync("buddy@elf.com");
            if (elfUser == null)
            {
                elfUser = new IdentityUser { UserName = "buddy@elf.com", Email = "buddy@elf.com", EmailConfirmed = true };
                await userManager.CreateAsync(elfUser, "ElfWork123!");
                await userManager.AddToRoleAsync(elfUser, "Elf");
            }
            
            var naughtyUser = await userManager.FindByEmailAsync("naughtykid@gmail.com");
            if (naughtyUser == null)
            {
                naughtyUser = new IdentityUser { UserName = "naughtykid@gmail.com", Email = "naughtykid@gmail.com", EmailConfirmed = true };
                await userManager.CreateAsync(naughtyUser, "IWantAllTheGifts123!");
            }

            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            if (!context.Gifts.Any())
            {
                context.Gifts.AddRange(
                    new Models.Gift { GiftName = "Toy Train", IsApproved = true },
                    new Models.Gift { GiftName = "Doll", IsApproved = true },
                    new Models.Gift { GiftName = "Bicycle", IsApproved = false }
                );
                await context.SaveChangesAsync();
            }
        }
    }
}
