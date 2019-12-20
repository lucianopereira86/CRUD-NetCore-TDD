using CRUD_NETCore_TDD.Infra.Models;
using CRUD_NETCore_TDD.Infra.Repositories;
using CRUD_NETCore_TDD.Infra.Validations;
using Newtonsoft.Json;
using Xunit;

namespace CRUD_NETCore_TDD.Test.Tests
{
    public class PutUserTest: PostUserTest
    {
        public PutUserTest()
        {
            Fact_PostUser();
        }
        #region THEORY
        [Theory]
        [InlineData(0, 103)]
        public void Theory_PutUser_Id(int Id, int ErrorCode)
        {
            var user = new User
            {
                Id = Id
            };

            CheckError(new PutUserValidator(), ErrorCode, user);
        }
        #endregion
        #region FACT
        //[Fact]
        //public void Fact_PutUser()
        //{
        //    // EXAMPLE
        //    var user = new User(1, "LUCIANO SOUSA", 30, false);

        //    // REPOSITORY
        //    ctx.User.Update(user);
        //    ctx.SaveChanges();

        //    // ASSERT
        //    var _user = ctx.User.Find(user.Id);
        //    Assert.Equal(JsonConvert.SerializeObject(user), JsonConvert.SerializeObject(_user));
        //}

        [Fact]
        public void Fact_PutUser_NoValidationNoRepository()
        {
            // EXAMPLE
            var user = new User(1, "LUCIANO SOUSA", 30, false);

            // VALIDATION
            var val = new PutUserValidator().Validate(user);
            Assert.True(val.IsValid);

            if (val.IsValid)
            {
                // REPOSITORY
                new UserRepository(ctx).Put(user);

                // ASSERT
                var _user = ctx.User.Find(user.Id);
                Assert.Equal(JsonConvert.SerializeObject(user), JsonConvert.SerializeObject(_user));
            }
        }
        #endregion
    }
}
