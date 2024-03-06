using Data;
using Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebAuction.Constants;

namespace WebAuction.Service
{
    public static class Seeder
    {
        public static void SeedData(this WebApplication app)
        {


            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

                try
                {
                    logger.LogInformation("seeding database");
                    var context = scope.ServiceProvider.GetRequiredService<AppEFContext>();
                    context.Database.Migrate();

                    SeedRole(services);//сидим роли
                }
                catch (Exception ex)
                {

                }
            }


        }

        private static void SeedRole(IServiceProvider service)
        {
            var roleManeger = service.GetRequiredService<RoleManager<AppRole>>();
            var userManeger = service.GetRequiredService<UserManager<AppUser>>();

            if (!roleManeger.Roles.Any())
            {
                var rez = roleManeger.CreateAsync(new AppRole
                {
                    Name = Roles.Admin
                }).Result;
                rez = roleManeger.CreateAsync(new AppRole
                {
                    Name = Roles.User
                }).Result;
            }

            if (!userManeger.Users.Any())
            {
                var user = new AppUser
                {
                    Email = "admin@gmail.com",
                    UserName = "admin@gmail.com",
                    Name = "Петро",
                   Avatar = "4mipbu5v.ktk.jpg"
                };
                var result = userManeger.CreateAsync(user, "12345").Result;
                if (result.Succeeded)
                {
                    result = userManeger.AddToRoleAsync(user, Roles.Admin).Result;
                }
            }

        }
    }
}
