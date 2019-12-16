using CRUD_NETCore_TDD.Infra.Models;
using CRUD_NETCore_TDD.Infra.Repositories;
using Xunit;

namespace CRUD_NETCore_TDD.Test.Tests
{
    public class PostUserTest: BaseTest
    {
        #region THEORY
        #endregion
        #region FACT
        //[Fact]
        //public void Fact_PostUser_NoClassNoRepository ()
        //{
        //    // EXAMPLE
        //    var user = new User("LUCIANO PEREIRA", 33, true);

        //    // REPOSITORY
        //    ctx.User.Add(user);
        //    ctx.SaveChanges();

        //    // ASSERT 
        //    Assert.Equal(1, user.Id);
        //}

        //[Fact]
        //public void Fact_PostUser_NoRepository()
        //{
        //    // EXAMPLE
        //    var user = new User(0, "LUCIANO PEREIRA", 33, true);

        //    // REPOSITORY
        //    ctx.User.Add(user);
        //    ctx.SaveChanges();

        //    // ASSERT 
        //    Assert.Equal(1, user.Id);
        //}

        [Fact]
        public void Fact_PostUser()
        {
            // EXAMPLE
            var user = new User(0, "LUCIANO PEREIRA", 33, true);

            // REPOSITORY
            user = new UserRepository(ctx).Post(user);

            // ASSERT 
            Assert.Equal(1, user.Id);
        }
        #endregion
    }
}
