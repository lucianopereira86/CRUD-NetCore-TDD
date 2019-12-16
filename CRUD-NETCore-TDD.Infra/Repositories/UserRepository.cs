using CRUD_NETCore_TDD.Infra.Models;

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
            return user;
        }
    }
}
