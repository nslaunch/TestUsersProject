using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace TestUsersProject.EntityFrameworkCore;

public static class TestUsersProjectDbContextConfigurer
{
    public static void Configure(DbContextOptionsBuilder<TestUsersProjectDbContext> builder, string connectionString)
    {
        builder.UseSqlServer(connectionString);
    }

    public static void Configure(DbContextOptionsBuilder<TestUsersProjectDbContext> builder, DbConnection connection)
    {
        builder.UseSqlServer(connection);
    }
}
