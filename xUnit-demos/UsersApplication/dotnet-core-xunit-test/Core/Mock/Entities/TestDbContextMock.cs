using UserApplication.Entities.TestDb;
using Microsoft.EntityFrameworkCore;
using System;

namespace UserApplication.Mock.Entities
{
    public partial class TestDbContextMock : TestDbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
                optionsBuilder.EnableSensitiveDataLogging();
            }
        }
    }
}
