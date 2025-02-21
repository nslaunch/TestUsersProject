using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static UserApplication.Dtos.UserDto;

namespace UserApplication.Entities.TestDb
{
    public static class DataSeeder
    {
        public static void SeedData(this IApplicationBuilder app)
        {
            using (IServiceScope serviceScope = app.ApplicationServices.CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetService<TestDbContext>();

                var users = new Users[]
                {
                    new Users() {Id = 1, FullName = "Nitin S", Email = "test@testmail.com", CreateDate = DateTime.Now,  Status = true },
                    new Users() {Id = 2, FullName = "Tejas S", Email = "test2@testmail.com", CreateDate = DateTime.Now,  Status = true },
                };

                db.Users.AddRange(users);

                db.SaveChanges();
            }
        }
    }
}
