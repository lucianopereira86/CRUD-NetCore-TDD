![titulo](/docs/titulo.JPG)

# CRUD-NETCore-TDD

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
      - [Post User • Theory • Red Step • Name](#post-user--theory--red-step--name)
      - [Post User • Theory • Green Step • Name](#post-user--theory--green-step--name)
      - [Post User • Theory • Refactor Step • Name](#post-user--theory--refactor-step--name)
      - [Post User • Theory • Red Step • Age](#post-user--theory--red-step--age)
      - [Post User • Theory • Green Step • Age](#post-user--theory--green-step--age)
    - [Post User • Fact II](#post-user--fact-II)
      - [Post User • Fact • Refactor Step II](#post-user--fact--refactor-step-II)

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

Just import the "UserRepository" class inside the "PostUserTest" file and it will compile.  
Run the test again:

![print07](/docs/print07.JPG)

Our refactoring is complete!  
If you have followed the instructions faithfully until here, then your project must be like this:

![print08](/docs/print08.JPG)

We are still far from finishing the POST tests. It's necessary to validate the user attributes before persist them in the database, so let's create our first **Theory**.

## Post User • Theory

### Post User • Theory • Red Step • Name

The **Theory** must be used to test the INVALID values from an entity. Some VALID values might be used as well but only to confirm that the test works perfectly for what it was designed for.  
Let's start testing the possible values for the User's "Name" attribute.  
Inside the "PostUserTest" class write the following code:

```cs
#region THEORY
[Theory]
[InlineData(null)]
[InlineData("")]
[InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
[InlineData("LUCIANO PEREIRA")]
public void Theory_PostUser_Name (string Name)
{
    var user = new User
    {
        Name = Name
    };
    Assert.Null(user.Name);
    Assert.Empty(user.Name);
    Assert.True(user.Name.Length > 20);
}

#endregion
```

The "Name" will be invalid if it be null, empty or exceeds 20 characters.
If you run the test, this will the result:

![print09](/docs/print09.JPG)

The errors happened because all the conditions weren't respected at once. They should be tested separetely in different methods, but as the User class has many attributes, it would consume lots of lines of code and, most important, time.  
To improve our development, we must use the "FluentValidation" package. It contains many functions that validate entire objects or attributes and return all the errors in a list.  
Before doing it, rename the "Theory_PostUser_Name" method to "Theory_PostUser_Name_NoValidation" and change the code a bit:

```cs
[Theory]
[InlineData(null)]
[InlineData("")]
[InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
[InlineData("LUCIANO PEREIRA")]
public void Theory_PostUser_Name_NoValidation (string Name)
{
    var user = new User
    {
        Name = Name
    };

    var val = new PostUserValidator().Validate(user);
    Assert.False(val.IsValid);
}
```

All the validations will be implemented inside the "PostUserValidator" class. If one condition be disrespected, than the "val.IsValid" attribute will be false. It's time to go to the **Green** step.

### Post User • Theory • Green Step • Name

Inside the Infra project, install the "FluentValidation" package through Nuget, create a folder named "Validations" and add into it a file named "PostUserValidator.cs". It will be responsible for validating only the "User" class attributes required in the POST method.

```cs
using CRUD_NETCore_TDD.Infra.Models;
using FluentValidation;

namespace CRUD_NETCore_TDD.Infra.Validations
{
    public class PostUserValidator: AbstractValidator<User>
    {
        public PostUserValidator()
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .MaximumLength(20);
        }
    }
}
```

Inside this code, the "Name" attribute must respect some rules.
The "Cascade(CascadeMode.StopOnFirstFailure)" means that if an error be encountered, it should return immediately from the field validation.  
"NotEmpty" means that it should not be null or empty.  
"MaximumLength" means that it should not have more than 20 characters.  
These are the same rules we have stablished before.

Now, go back to the "PostUserTest" class, copy the "Theory_PostUser_Name_NoValidation" method, comment the original, rename the copy to "Theory_PostUser_Name_Validation" and import the "PostUserValidator" class. The file will be able to compile.

Run the test again and this will be the result:

![print10](/docs/print10.JPG)

All the conditions have been met except for the one containing a valid value ("LUCIANO PEREIRA") which was to be expected. So we can comment its line for further tests.  
But there is still a problem... How to prove that an specific condition was met instead of another one?  
We will solve this but adding error codes for each one. Time for refactoring...

### Post User • Theory • Refactor Step • Name

Change the "PostUserValidator" constructor by adding these lines:

```cs
public PostUserValidator()
{
    RuleFor(x => x.Name)
        .Cascade(CascadeMode.StopOnFirstFailure)
        .NotEmpty()
        .WithErrorCode("100")
        .MaximumLength(20)
        .WithErrorCode("101");
}
```

For the null or empty condition, it will return the custom error code 100.  
For the maximum length condition, it will return the custom error code 101.

Inside the "PostUserTest" class, copy the "Theory_PostUser_Name_Validation", comment the original, rename the copy to "Theory_PostUser_Name" and refactor the code:

```cs
[Theory]
[InlineData(null, 100)]
[InlineData("", 100)]
[InlineData("LUCIANO PEREIRA", 100)]
[InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ", 101)]
[InlineData("LUCIANO PEREIRA", 101)]
public void Theory_PostUser_Name(string Name, int ErrorCode)
{
    var user = new User
    {
        Name = Name
    };

    var val = new PostUserValidator().Validate(user);
    Assert.False(val.IsValid);

    if(!val.IsValid)
    {
        bool hasError = val.Errors.Any(a => a.ErrorCode.Equals(ErrorCode.ToString()));
        Assert.True(hasError);
    }
}
```

For each possible value there is an error code. The successful result will happen when the expected error code exists in the error list.  
Notice that the value "LUCIANO PEREIRA" appears twice because it will be tested for each validation.
Run the test and see the result:

![print11](/docs/print11.JPG)

As we have expected, only the valid value didn't return the error code it was expecting. It means that our validation is working perfectly!

Let's improve our refactoring a little more by adding a generic method inside the "BaseTest" class that will be useful for all the other CRUD Test classes that will be created:

```cs
// Add inside BaseTest.cs
protected void CheckError<T>(AbstractValidator<T> validator, int ErrorCode, T vm)
{
    var val = validator.Validate(vm);
    Assert.False(val.IsValid);

    if (!val.IsValid)
    {
        bool hasError = val.Errors.Any(a => a.ErrorCode.Equals(ErrorCode.ToString()));
        Assert.True(hasError);
    }
}
```

The "Theory_PostUser_Name" inside the "PostUserTest" class must be changed as well:

```cs
[Theory]
[InlineData(null, 100)]
[InlineData("", 100)]
[InlineData("LUCIANO PEREIRA", 100)]
[InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ", 101)]
[InlineData("LUCIANO PEREIRA", 101)]
public void Theory_PostUser_Name(string Name, int ErrorCode)
{
    var user = new User
    {
        Name = Name
    };
    CheckError(new PostUserValidator(), ErrorCode, user);
}
```

### Post User • Theory • Red Step • Age

Let's do a **Theory** for the "Age" attribute.

```cs
[Theory]
[InlineData(0, 102)]
[InlineData(-1, 102)]
[InlineData(33, 102)]
public  void Theory_PostUser_Age(int Age, int ErrorCode)
{
    var user = new User
    {
        Age = Age
    };

    CheckError(new PostUserValidator(), ErrorCode, user);
}
```

If you run the tests, there will error for all the values because we have not implemented the validation for the age yet. It must be greater than zero.

### Post User • Theory • Green Step • Age

Inside the PostUserValidators constructor, add the following lines:

```cs
public PostUserValidator()
{
    RuleFor(x => x.Name)
        .Cascade(CascadeMode.StopOnFirstFailure)
        .NotEmpty()
        .WithErrorCode("100")
        .MaximumLength(20)
        .WithErrorCode("101");

    RuleFor(x => x.Age)
        .Cascade(CascadeMode.StopOnFirstFailure)
        .GreaterThan(0)
        .WithErrorCode("102");
}
```

Run the tests again and this will be the result:

![print12](/docs/print12.JPG)

Only the valid value (33) has not returned any error, so our tests are working correctly again!

## Post User • Fact II

### Post User • Fact • Refactor Step II

There is no need for another **Theory** for the "PostUserTest" but another **Fact** is required to validate the data. Rename the "Fact_PostUser" method to "Fact_PostUser_NoValidation" and create another one:

```cs
 [Fact]
public void Fact_PostUser()
{
    // EXAMPLE
    var user = new User(0, "LUCIANO PEREIRA", 33, true);

    var val = new PostUserValidator().Validate(user);

    // ASSERT
    Assert.True(val.IsValid);

    if (val.IsValid)
    {
        // REPOSITORY
        user = new UserRepository(ctx).Post(user);

        // ASSERT
        Assert.Equal(1, user.Id);
    }
}
```

This time we are using the "Assert" class to ensure that the validation will be true before accessing the repository. Run the test again.

![print13](/docs/print13.JPG)

This must be your project so far:

![print14](/docs/print14.JPG)

## Coming soon...

The PUT method will be the next to receive tests, repository and validation.
