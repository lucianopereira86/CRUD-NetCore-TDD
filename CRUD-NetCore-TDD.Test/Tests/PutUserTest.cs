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
    public class PutUserTest
    {
        public DBContext ctx = new DBContext();
 
        #region THEORY
        //[Theory]
        //[InlineData(0)]
        //[InlineData(1)]
        //public void Theory_Put_User_Id_Validation_FluentAssertions(int Id)
        //{
        //    var user = new User(Id, "Paulo Silva", 30);

        //    user.Id.Should().Be(0);
        //}
        //[Theory]
        //[InlineData(null)]
        //[InlineData("Paulo Silva")]
        //public void Theory_Put_User_Name_Validation_FluentAssertions(string Name)
        //{
        //    var user = new User(1, Name, 30);

        //    user.Name.Should().BeNullOrEmpty();
        //}
        //[Theory]
        //[InlineData(0)]
        //[InlineData(33)]
        //public void Theory_Put_User_Age_Validation_FluentAssertions(int Age)
        //{
        //    var user = new User(1, "Paulo Silva", Age);

        //    user.Age.Should().Be(0);
        //}

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void Theory_Put_User_Id_Validation_FluentValidation(int Id)
        {
            var user = new User(Id, "Paulo Silva", 30);
            var val = new PutUserValidation().Validate(user);
            Assert.False(val.IsValid);
            bool hasError = val.Errors.Any(e => e.ErrorCode.Equals("103"));
            Assert.True(hasError);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("Paulo Silva")]
        public void Theory_Put_User_Name_Validation_FluentValidation(string Name)
        {
            var user = new User(1, Name, 30);
            var val = new PutUserValidation().Validate(user);
            Assert.False(val.IsValid);
            bool hasError = val.Errors.Any(e => e.ErrorCode.Equals("101"));
            Assert.True(hasError);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(30)]
        public void Theory_Put_User_Age_Validation_FluentValidation(int Age)
        {
            var user = new User(1, "Paulo Silva", Age);
            var val = new PutUserValidation().Validate(user);
            Assert.False(val.IsValid);
            bool hasError = val.Errors.Any(e => e.ErrorCode.Equals("102"));
            Assert.True(hasError);
        }
        #endregion

        #region FACT

        [Fact]
        public void Fact_Put_User()
        {
            // Add user
            ctx.User.Add(new User(1, "Luciano Pereira", 33));

            // Update fields
            var user = new User();
            user.Id = 1;
            user.Name = "Paulo Silva";
            user.Age = 30;

            var val = new PutUserValidation().Validate(user);
            Assert.True(val.IsValid);

            // Update user
            ctx.User.Where(x => x.Id == user.Id).ToList()
                .ForEach(e => { e.Name = user.Name; e.Age = user.Age; });

            var _user = ctx.User.Find(x => x.Id == user.Id);
            Assert.Equal(user.Name, _user.Name);
            Assert.Equal(user.Age, _user.Age);
        }
        #endregion
    }
}
