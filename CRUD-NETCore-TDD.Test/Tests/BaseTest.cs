using CRUD_NETCore_TDD.Infra.Models;
using CRUD_NETCore_TDD.Infra.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Xunit;

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

        protected void CheckError<T>(AbstractValidator<T> validator, int ErrorCode, T vm)
        {
            var val = validator.Validate(vm);
            Assert.False(val.IsValid);

            if (!val.IsValid)
            {
                bool hasError = val.Errors.Any(a => a.ErrorCode.Equals(ErrorCode.ToString()));
                Assert.True(hasError);
            }
        }
    }
}
