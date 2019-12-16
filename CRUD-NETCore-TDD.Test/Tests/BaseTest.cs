using CRUD_NETCore_TDD.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CRUD_NETCore_TDD.Test.Tests
{
    public class BaseTest
    {
        protected MyContext ctx;
        public BaseTest(MyContext ctx = null)
        {
            this.ctx = ctx ?? GetInMemoryDBContext();
        }
        protected MyContext GetInMemoryDBContext()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();
            var builder = new DbContextOptionsBuilder<MyContext>();
            var options = builder.UseInMemoryDatabase("test").UseInternalServiceProvider(serviceProvider).Options;
            MyContext dbContext = new MyContext(options);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
            return dbContext;
        }
    }
}
