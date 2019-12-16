using CRUD_NETCore_TDD.Infra.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUD_NETCore_TDD.Infra.Repositories
{
    public class MyContext: DbContext
    {
        public DbSet<User> User { get; set; }
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>(e =>
            {
                e
                .ToTable("user")
                .HasKey(k => k.Id);

                e
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();
            });
        }
    }
}
