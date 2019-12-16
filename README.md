![titulo](/docs/titulo.JPG)

# CRUD-NetCore-TDD

Building a .NET Core web API with TDD.

## Technologies

- [Visual Studio 2019](https://visualstudio.microsoft.com/pt-br/downloads/)
- [.NET Core 3.1.0](https://dotnet.microsoft.com/download/dotnet-core/3.1)
- [xUnit 2.4.0](https://www.nuget.org/packages/xunit/2.4.0)
- [Microsoft.EntityFrameworkCore 3.1.0](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore/3.1.0)
- [FluentValidation 8.6.0](https://www.nuget.org/packages/fluentvalidation)

## Objective

Let's build a simple user CRUD web API by following the TDD steps with xUnit and simulate the database connection with runtime memory.

## Topics

- [TDD](#tdd)
- [Project Structure](#project-structure)
- [Test Project](#test-project)
  - [Post User](#post-user)
    - [Post User • Fact](#post-user--fact)
      - [Post User • Fact • Red Step](#post-user--fact--red-step)
      - [Post User • Fact • Green Step](#post-user--fact--green-step)
      - [Post User • Fact • Refactor Step](#post-user--fact--refactor-step)
    - [Post User • Theory](#post-user--theory)

## TDD

What's TDD?  
It means [Test Driven Development](https://medium.com/tableless/tdd-test-driven-development-71ad9a69d465) and it consists in programming unit tests of core functionalities of a application before creating classes, projects, validations and other layers.  
During the development, there will be a cycle of three steps: **Red**, **Green** and **Refactor**.  
The **Red** step needs the code goes wrong when executed, even by not compiling.
The **Green** step consists of a successful compilation and exacly what the unit test was expecting as result.  
The **Refactor** step will be a refactoring of the code to become a new functionality like a new class or module.
With this cycle you will have a clear understanding of all the failures and certainties that your program may have. The problem is that it requires a good amount of time to develop.

## Project Structure

Our solution will have 3 layers: web API, Infra and Test, as shown below:

![print01](/docs/print01.JPG)

The web API layer will contain only one folder with Controllers.
The Infra layer will be responsible for the repositories and table models.
The Test layer will have the unit tests for each funcionality to be built in the other layers.

Now, let's create a solution!  
Open the Visual Studio 2019 and create a new .NET Core web application project and name it "CRUD-NETCore-TDD". Choose the API template and uncheck the HTTPS option:

![print02](/docs/print02.JPG)

The default .NET Core web API will be created.  
Delete the "Controllers/WeatherForecastController.cs" and "WeatherForecast.cs" files.
Add a C# .NET Core class library project to the solution named "CRUD-NETCore-TDD.Infra".  
Delete the "Class1.cs" file.

![print03](/docs/print03.JPG)

Add a .NET Core xUnit Test Project to the solution and name it "CRUD-NETCore-TDD.Test".
Delete the "UnitTest1.cs" file.

![print04](/docs/print04.JPG)

Add a reference from the Infra project to the web API project.  
Add a reference to the Test project from the other projects.

![print05](/docs/print05.JPG)

## Test Project

It's time to begin the fun!  
We will build tests for each CRUD method of the User entity by using Entity Framework Core structure.
There will be two types of unit tests: **Fact** and **Theory**.  
**Fact** is a method with a unique result without parameters.  
**Theory** allow multiple parameters expecting for different results.

## Post User

Add a folder named "Tests" to the Test Project with a file named "PostUserTest.cs" with the code below:

```cs
using Xunit;

namespace CRUD_NETCore_TDD.Test.Tests
{
    public class PostUserTest
    {
        #region THEORY
        #endregion
        #region FACT
        [Fact]
        public void Fact_PostUser ()
        {

        }
        #endregion
    }
}

```

## Post User • Fact

### Post User • Fact • Red Step

Our first test will run what we really want: to register a new user to the database. Write this code inside the **Fact_PostUser** method:

```cs
[Fact]
public void Fact_PostUser ()
{
    // EXAMPLE
    var user = new User("LUCIANO PEREIRA", 33, true);

    // REPOSITORY
    ctx.User.Add(user);
    ctx.SaveChanges();

    // ASSERT
    Assert.Equal(1, user.Id);
}
```

At first, there are no **User** class. Also, the "ctx" object should be an instance of the **DbContext** class, but there is no EF Core library installed yet to make the code to compile.
That is the **Red** step. We know what we want and what we have to do.  
Before going to the **Green** step, change the method's name from "Fact_PostUser" to "Fact_PostUser_NoModelNoRepository", so it becomes clear what is missing for the method to run.

### Post User • Fact • Green Step

Create a folder named "Models" inside the Infra project with a file named "User.cs".  
Add the following code into it:

```cs
namespace CRUD_NETCore_TDD.Infra.Models
{
    public class User
    {
        public User()
        {

        }
        public User(int Id, string Name, int Age, bool IsActive)
        {
            this.Id = Id;
            this.Name = Name;
            this.Age = Age;
            this.IsActive = IsActive;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public bool IsActive { get; set; }
    }
}

```

The "User" model is completed. Inside the "PostUserTest.cs" file, copy and paste the "Fact_PostUser_NoModelNoRepository" method and comment the original. Change the copy's name to
"Fact_PostUser_NoRepository" and import the "User" class. The full code will be like this:

```cs
using CRUD_NETCore_TDD.Infra.Models;
using Xunit;

namespace CRUD_NETCore_TDD.Test.Tests
{
    public class PostUserTest
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

        [Fact]
        public void Fact_PostUser_NoRepository()
        {
            // EXAMPLE
            var user = new User(0, "LUCIANO PEREIRA", 33, true);

            // REPOSITORY
            ctx.User.Add(user);
            ctx.SaveChanges();

            // ASSERT
            Assert.Equal(1, user.Id);
        }
        #endregion
    }
}
```

Now, it is only missing the "ctx" object to make the code compile.  
These will be need to install 3 packages:

- "Microsoft.EntityFrameworkCore" and "Microsoft.EntityFrameworkCore.SqlServer" packages inside the Infra project;
- "Microsoft.EntityFrameworkCore.InMemory" package inside the Test project.

Create a new folder named "Repositories" inside the Infra project containing the "MyContext.cs" file with the following code:

```cs
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

```

What we have here is the basics for the EF Core to work in our project. The "user" table does not need to exist yet because will make the database to run in runtime memory.
Inside the Test project, create another file inside the "Tests" folder named "BaseTest.cs". It will be a super class containing the following code:

```cs
using CRUD_NETCore_TDD.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CRUD_NETCore_TDD.Test.Tests
{
    public class BaseTest
    {
        protected MyContext ctx;
        public BaseTest(MyContext ctx = null)
        {
            this.ctx = ctx ?? GetInMemoryDBContext();
        }
        protected MyContext GetInMemoryDBContext()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<MyContext>();
            var options = builder.UseInMemoryDatabase("test").UseInternalServiceProvider(serviceProvider).Options;

            MyContext dbContext = new MyContext(options);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
            return dbContext;
        }
    }
}

```

The "BaseTest" class is responsive for instantiating the "MyContext" class and make it run in memory without the need of a database previously created. All the write and read operations will work perfectly. If another DbContext instance exists, it will be passed as parameter to the constructor.

Make the "PostUserTest" class to implement the "BaseTest" class and the code will be finally be able to compile. Copy and paste the "Fact_PostUser_NoRepository" method, comment the original and rename the copy to "Fact_PostUser". The code will be like this:

```cs
using CRUD_NETCore_TDD.Infra.Models;
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
            ctx.User.Add(user);
            ctx.SaveChanges();

            // ASSERT
            Assert.Equal(1, user.Id);
        }
        #endregion
    }
}
```

Run the tests with the Test Manager to see the result:

![print06](/docs/print06.JPG)

Finally, our **Green** step is done!  
Time to refactor the code.

### Post User • Fact • Refactor Step

We will concentrate the database operations inside a repository class for the user entity.  
Firstly, modify the "Fact_PostUser" method like this:

```cs
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
```

As you can see, it must be created a "UserRepository" class with a "Post" method that must execute that same operations from before.  
Inside the Infra project, create another file inside the "Repositories" folder named "UserRepository.cs" with the following code:

```cs
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
```

Just import the "UserRepository" class inside the "PostUserTest" file and it will compiled.  
Run the test again:

![print07](/docs/print07.JPG)

Our refactoring is complete!  
But we are far from finishing POST tests. It's necessary to validate the user attributes before persist them in the database, so let's create our first **Theory**.
