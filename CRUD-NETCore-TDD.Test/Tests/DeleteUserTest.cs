using CRUD_NETCore_TDD.Infra.Models;
using CRUD_NETCore_TDD.Infra.Repositories;
using CRUD_NETCore_TDD.Infra.Validations;
using System.Linq;
using Xunit;

namespace CRUD_NETCore_TDD.Test.Tests
{
    public class DeleteUserTest : BaseTest
    {
        public DeleteUserTest()
        {
            new PostUserTest(ctx).Fact_PostUser();
        }
        #region THEORY
        #endregion
        #region FACT
        //[Fact]
        //public void Fact_DeleteUser ()
        //{
        //    var user = new User
        //    {
        //        Id = 1
        //    };

        //    ctx.User.Remove(user);
        //    ctx.SaveChanges();

        //    // ASSERT
        //    var users = ctx.User.ToList();
        //    Assert.True(users.Count == 0);
        //}
        [Fact]
        public void Fact_DeleteUser_NoValidationNoRepository()
        {
            var user = new User
            {
                Id = 1
            };

            var val = new DeleteUserValidator().Validate(user);
            Assert.True(val.IsValid);

            if (val.IsValid)
            {
                new UserRepository(ctx).Delete(user);

                // ASSERT
                var users = ctx.User.ToList();
                Assert.True(users.Count == 0);
            }
        }
        #endregion
    }
}
