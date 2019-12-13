using CRUD_NetCore3._1_TDD.Test.Models;
using CRUD_NetCore3._1_TDD.Test.Validations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Sdk;
using FluentAssertions;

namespace CRUD_NetCore3._1_TDD.Test.Tests
{
    public class PostUserTest
    {
        public DBContext ctx = new DBContext();

        #region THEORY
        //[Theory]
        //[InlineData(null)]
        //[InlineData("Luciano Pereira")]
        //public void Theory_Post_User_Name_Validation_FluentAssertions(string Name)
        //{
        //    var user = new User();
        //    user.Name = Name;

        //    user.Name.Should().BeNullOrEmpty();

        //    ctx.User.Add(user);
        //}

        //[Theory]
        //[InlineData(0)]
        //[InlineData(33)]
        //public void Theory_Post_User_Age_Validation_FluentAssertions(int Age)
        //{
        //    var user = new User();
        //    user.Age = Age;

        //    user.Age.Should().Be(0);

        //    ctx.User.Add(user);
        //}

        //[Theory]
        //[InlineData(null, 33)]
        //[InlineData("Luciano Pereira", 0)]
        //public void Theory_Post_User_No_FluentValidation(string Name, int Age)
        //{
        //    var user = new User();
        //    user.Name = Name;
        //    user.Age = Age;

        //    var val = new PostUserValidation().Validate(user);
        //    Assert.False(val.IsValid);

        //    ctx.User.Add(user);
        //}

        //[Theory]
        //[InlineData(null, 33)]
        //[InlineData("Luciano Pereira", 0)]
        //public void Theory_Post_User_Validation_FluentValidation(string Name, int Age)
        //{
        //    var user = new User();
        //    user.Name = Name;
        //    user.Age = Age;

        //    var val = new PostUserValidation().Validate(user);
        //    Assert.False(val.IsValid);

        //    ctx.User.Add(user);
        //}

        [Theory]
        [InlineData(null)]
        [InlineData("Luciano Pereira")]
        public void Theory_Post_User_Name_Validation(string Name)
        {
            var user = new User();
            user.Name = Name;

            var val = new PostUserValidation().Validate(user);
            if (!val.IsValid)
            {
                bool hasError = val.Errors.Any(e => e.ErrorCode.Equals("101"));
                Assert.True(hasError);
            }

            ctx.User.Add(user);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(33)]
        public void Theory_Post_User_Age_Validation(int Age)
        {
            var user = new User();
            user.Age = Age;

            var val = new PostUserValidation().Validate(user);
            if (!val.IsValid)
            {
                bool hasError = val.Errors.Any(e => e.ErrorCode.Equals("102"));
                Assert.True(hasError);
            }

            ctx.User.Add(user);
        }
        #endregion

        #region FACT
        //[Fact]
        //public void Fact_Post_No_User_No_DbContext()
        //{
        //    // No USER class
        //    var user = new User();
        //    user.Name = "Luciano Pereira";
        //    user.Age = 33;

        //    // No DbContext class
        //    ctx.User.Add(user);
        //}

        //[Fact]
        //public void Fact_Post_User_No_DbContext()
        //{
        //    var user = new User();
        //    user.Name = "Luciano Pereira";
        //    user.Age = 33;

        //    // No DbContext class
        //    ctx.User.Add(user);
        //}

        //[Fact]
        //public void Fact_Post_User()
        //{
        //    var user = new User();
        //    user.Name = "Luciano Pereira";
        //    user.Age = 33;

        //    ctx.User.Add(user);
        //}

        [Fact]
        public void Fact_Post_User()
        {
            var user = new User();
            user.Name = "Luciano Pereira";
            user.Age = 33;

            var val = new PostUserValidation().Validate(user);
            Assert.True(val.IsValid);

            ctx.User.Add(user);
        }
        #endregion
    }
}
