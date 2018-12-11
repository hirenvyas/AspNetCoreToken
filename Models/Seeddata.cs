using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace CityInfo.API.Models
{
    //Create Seeddata in intail state for validate Authentication, in real world to connect with AspNetuser Table from database

    public class Seeddata
    {
        public static void Init(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            context.Database.EnsureCreatedAsync();
            var data = context.Users;
            if(!context.Users.Any())
            {
                ApplicationUser user = new ApplicationUser
                {
                    Email = "hiren@gmail.com",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = "Hiren"               
                };
                userManager.CreateAsync(user, "Test@123");
            }
        }
    }
}
