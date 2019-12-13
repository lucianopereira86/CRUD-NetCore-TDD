using CRUD_NetCore3._1_TDD.Test.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Xunit;
using FluentAssertions;

namespace CRUD_NetCore3._1_TDD.Test.Tests
{
    public class GetUserTest
    {
        public DBContext ctx = new DBContext();
        private void AddUser()
        {
            ctx.User.Add(new User(1, "Luciano Pereira", 33));
        }
        #region THEORY
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void Theory_Get_User_By_Id(int Id)
        {
            AddUser();
            var users = ctx.User.Where(w => w.Id == Id).ToList();
            users.Should().HaveCount(1);
        }
        [Theory]
        [InlineData(null)]
        [InlineData("Luciano Pereira")]
        public void Theory_Get_User_By_Name(string Name)
        {
            AddUser();
            var users = ctx.User.Where(w => w.Name.Equals(Name)).ToList();
            users.Should().HaveCount(1);
        }
        [Theory]
        [InlineData(0)]
        [InlineData(33)]
        public void Theory_Get_User_By_Age(int Age)
        {
            AddUser();
            var users = ctx.User.Where(w => w.Age == Age).ToList();
            users.Should().HaveCount(1);
        }
        [Theory]
        [InlineData(0, null, 0)]
        [InlineData(1, "Luciano Pereira", 33)]
        public void Theory_Get_User(int Id, string Name, int Age)
        {
            AddUser();
            var users = ctx.User.Where(w => 
                (Id == 0 || w.Id == Id)
                &&
                (Name == null || w.Name.Equals(Name))
                &&
                (Age == 0 || w.Age == Age)
            ).ToList();
            users.Should().HaveCount(1);
        }
        #endregion

        #region FACT 
        [Fact]
        public void Fact_Get_User()
        {
            AddUser();
            var user = new User(1, "Luciano Pereira", 33);
            var users = ctx.User.Where(w =>
                w.Id == user.Id
                &&
                w.Name.Equals(user.Name) 
                && 
                w.Age == user.Age
            ).ToList();
            users.Should().HaveCount(1);
        }
        #endregion
    }
}
