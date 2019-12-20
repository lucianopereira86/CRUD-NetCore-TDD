using CRUD_NETCore_TDD.Infra.Models;
using System;

namespace CRUD_NETCore_TDD.Infra.Repositories
{
    public class UserRepository
    {
        private readonly MyContext ctx;
        public UserRepository(MyContext ctx)
        {
            this.ctx = ctx;
        }
        public User Post(User user)
        {
            ctx.User.Add(user);
            ctx.SaveChanges();
            ctx.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            return user;
        }

        public void Put(User user)
        {
            ctx.User.Update(user);
            ctx.SaveChanges();
        }

        public void Delete(User user)
        {
            ctx.User.Remove(user);
            ctx.SaveChanges();
        }
    }
}
