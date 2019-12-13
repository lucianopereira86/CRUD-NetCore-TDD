using CRUD_NetCore3._1_TDD.Test.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Xunit;
using FluentAssertions;
using CRUD_NetCore3._1_TDD.Test.Validations;

namespace CRUD_NetCore3._1_TDD.Test.Tests
{
    public class DeleteUserTest
    {
        public DBContext ctx = new DBContext();
  
        #region THEORY
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void Theory_Delete_User_Validation_FluentAssertions(int Id)
        {
            var user = new User();
            user.Id = Id;
            user.Id.Should().Be(0);
        }
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void Theory_Delete_User_Validation_FluentValidation(int Id)
        {
            var user = new User();
            user.Id = Id;

            var val = new DeleteUserValidation().Validate(user);
            Assert.False(val.IsValid);
            bool hasError = val.Errors.Any(a => a.ErrorCode.Equals("103"));
            Assert.True(hasError);
        }

        #endregion

        #region FACT 
        [Fact]
        public void Fact_Delete_User()
        {
            // Add user
            ctx.User.Add(new User(1, "Luciano Pereira", 33));
            
            var user = new User();
            user.Id = 1;
            
            var val = new DeleteUserValidation().Validate(user);
            Assert.True(val.IsValid);

            ctx.User.RemoveAll(a => a.Id == user.Id);
            var users = ctx.User.ToList();
            users.Should().HaveCount(0);
        }
        #endregion
    }
}
