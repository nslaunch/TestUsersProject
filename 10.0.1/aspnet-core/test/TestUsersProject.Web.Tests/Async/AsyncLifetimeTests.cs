using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestUsersProject.EntityFrameworkCore;
using TestUsersProject.Tests;
using Xunit;

namespace TestUsersProject.Web.Tests.Async
{
    public class AsyncLifetimeTests : TestUsersProjectTestBase, IAsyncLifetime
    {
        private TestUsersProjectDbContext _testDbContext = null;
        [Fact]
        public void Collection()
        {
            var count = 1;
            var dbCount = _testDbContext.Employees.Count();
            Assert.Equal(count, dbCount);
        }

        public async Task InitializeAsync()
        {
            _testDbContext = Resolve<TestUsersProjectDbContext>();
            _testDbContext.Employees.AddRange(new List<Employee>() {
                new Employee(){
                    Email = "testuser@test.com",
                    CreateDate = DateTime.Now,
                    FullName = "test user",
                    Id = 1,
                    Status = true
                }
            });
            await _testDbContext.SaveChangesAsync();
        }

        public async Task DisposeAsync()
        {
            _testDbContext.DisposeAsync();
        }
    }
}
