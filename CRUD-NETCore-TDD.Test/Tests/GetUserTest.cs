using CRUD_NETCore_TDD.Infra.Models;
using Newtonsoft.Json;
using System.Linq;
using Xunit;

namespace CRUD_NETCore_TDD.Test.Tests
{
    public class GetUserTest : BaseTest
    {
        public GetUserTest()
        {
            new PostUserTest(ctx).Fact_PostUser();
        }
        #region THEORY
        [Theory]
        [InlineData(1)]
        public void Theory_GetUser_Id(int Id)
        {
            var user = new User
            {
                Id = Id
            };

            var users = ctx.User
                           .Where(w =>
                               (user.Id == 0 || w.Id == user.Id)
                            )
                           .ToList();
            // ASSERT
            Assert.True(users.Count > 0);
        }
        [Theory]
        [InlineData("LUCIANO PEREIRA")]
        public void Theory_GetUser_Name(string Name)
        {
            var user = new User
            {
                Name = Name
            };

            var users = ctx.User
                           .Where(w =>
                               (string.IsNullOrEmpty(user.Name) || w.Name.Equals(user.Name))
                            )
                           .ToList();
            // ASSERT
            Assert.True(users.Count > 0);
        }
        [Theory]
        [InlineData(33)]
        public void Theory_GetUser_Age(int Age)
        {
            var user = new User
            {
                Age = Age
            };

            var users = ctx.User
                           .Where(w =>
                               (user.Age == 0 || w.Age == user.Age)
                            )
                           .ToList();
            // ASSERT
            Assert.True(users.Count > 0);
        }
        [Theory]
        [InlineData(true)]
        public void Theory_GetUser_IsActive(bool IsActive)
        {
            var user = new User
            {
                IsActive = IsActive
            };

            var users = ctx.User
                           .Where(w =>
                               w.IsActive == user.IsActive
                            )
                           .ToList();
            // ASSERT
            Assert.True(users.Count > 0);
        }
        #endregion
        #region FACT
        //[Fact]
        //public void Fact_GetUser()
        //{
        //    var user = new User(1, "LUCIANO PEREIRA", 33, true);

        //    var users = ctx.User
        //                    .Where(w =>
        //                        w.Id == user.Id
        //                        &&
        //                        w.Name.Equals(user.Name)
        //                        &&
        //                        w.Age == user.Age
        //                        &&
        //                        w.IsActive == user.IsActive
        //                    )
        //                    .ToList();
        //    // ASSERT
        //    Assert.Equal(JsonConvert.SerializeObject(user), JsonConvert.SerializeObject(users.First()));
        //}

        [Fact]
        public void Fact_GetUser()
        {
            var user = new User(1, "LUCIANO PEREIRA", 33, true);

            var users = ctx.User
                            .Where(w =>
                                (user.Id == 0 || w.Id == user.Id)
                                &&
                                (string.IsNullOrEmpty(user.Name) || w.Name.Equals(user.Name))
                                &&
                                (user.Age == 0 || w.Age == user.Age)
                                &&
                                w.IsActive == user.IsActive
                            )
                            .ToList();
            // ASSERT
            Assert.Equal(JsonConvert.SerializeObject(user), JsonConvert.SerializeObject(users.First()));
        }
        #endregion
    }
}
