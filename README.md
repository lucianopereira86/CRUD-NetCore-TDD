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
      - [Red Step](#red-step)
      - [Green Step](#green-step)
      - [Refactor Step](#refactor-step)
    - [Post User • Theory](#post-user--theory)
      - [Red Step • Name](#red-step--name)
      - [Green Step • Name](#green-step--name)
      - [Refactor Step • Name](#refactor-step--name)
      - [Red Step • Age](#red-step--age)
      - [Green Step • Age](#green-step--age)
    - [Post User • Fact II](#post-user--fact-II)
      - [Refactor Step II](#refactor-step-II)
  - [Put User](#put-user)
    - [Put User • Fact](#put-user--fact)
      - [Red Step](#red-step)
      - [Green Step](#green-step)
    - [Put User • Theory](#put-user--theory)
      - [Green Step](#green-step)
  - [Get User](#get-user)
    - [Get User • Fact](#get-user--fact)
      - [Green Step](#green-step)
    - [Get User • Theory](#get-user--theory)
      - [Green Step • Id](#green-step--id)
      - [Green Step • Name](#green-step--name)
      - [Green Step • Age](#green-step--age)
      - [Green Step • IsActive](#green-step--isactive)
    - [Get User • Fact II](#get-user--fact-II)
      - [Refactor Step II](#refactor-step-II)

## TDD

What's TDD?  
It means [Test Driven Development](https://en.wikipedia.org/wiki/Test-driven_development) and it consists on programming unit tests for core functionalities of an application before creating classes, projects, validations and other layers.  
During the development, there will be a cycle of three steps: **Red**, **Green** and **Refactor**.  
The **Red** step needs the code to fail when executed, even by not compiling.  
The **Green** step consists of a successful compilation and exacly what the unit test was expecting as result.  
The **Refactor** step will be a refactoring of the code to become a new functionality like a new class or module.  
With this cycle you will have a clear understanding of all the failures and certainties that your program may have. The problem is that it requires a good amount of time to develop.

## Project Structure

Initially, our solution will have 3 layers: web API, Infra and Test.

The web API layer will contain the controllers.
The Infra layer will be responsible for the repositories and table models.
The Test layer will have the unit tests for each funcionality to be built in the other layers.

Now, let's create the solution!  
Open the Visual Studio 2019 and create a new .NET Core web application project and name it "CRUD-NETCore-TDD". Choose the API template and uncheck the HTTPS option:

![print02](/docs/print02.JPG)

The default .NET Core web API will be created.  
Delete the "Controllers/WeatherForecastController.cs" and "WeatherForecast.cs" files. Add a C# .NET Core class library project to the solution named "CRUD-NETCore-TDD.Infra". Delete the "Class1.cs" file as well.  
The solution will look like this:

![print03](/docs/print03.JPG)

Add a .NET Core xUnit Test Project to the solution and name it "CRUD-NETCore-TDD.Test". Delete the "UnitTest1.cs" file.  
The solution will look like this:

![print04](/docs/print04.JPG)

Add a reference from the Infra project to the web API project and a reference to the Test project from the other ones.  
The solution will look like this:

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

### Red Step

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

### Green Step

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
It will be necessary to install 3 packages through Nuget:

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

The "BaseTest" class is responsive for instantiating the "MyContext" class and make it run in memory without the need of a database previously created. All the write and read operations will work perfectly. If another "MyContext" instance exists, it will be passed as parameter to the constructor.

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

### Refactor Step

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

### Red Step • Name

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

### Green Step • Name

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

### Refactor Step • Name

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

### Red Step • Age

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

### Green Step • Age

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

### Refactor Step II

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

## Put User

Create a file named "PutUserTest.cs" inside the folder "Tests" with the code below:

```cs
using Xunit;

namespace CRUD_NETCore_TDD.Test.Tests
{
    public class PutUserTest: PostUserTest
    {
        public PutUserTest()
        {
            Fact_PostUser();
        }
        #region THEORY
        #endregion
        #region FACT
        [Fact]
        public void Fact_PutUser()
        {
        }
        #endregion
    }
}
```

In order to test the PUT method, a POST is required inside the constructor.

## Put User • Fact

The fisrt **Fact** must update all the attributes from the User entity successfully, so add this code:

```cs
[Fact]
public void Fact_PutUser()
{
    // EXAMPLE
    var user = new User(1, "LUCIANO SOUSA", 30, false);

    // REPOSITORY
    ctx.User.Update(user);
    ctx.SaveChanges();

    // ASSERT
    var _user = ctx.User.Find(user.Id);
    Assert.Equal(JsonConvert.SerializeObject(user), JsonConvert.SerializeObject(_user));
}
```

In the "ASSERT" section, the user is retrieved from the repository and compared to the entry object.

### Red Step

Copy the method, comment the original and rename the copy to "Fact_PutUser_NoValidationNoRepository". Modify the code as below:

```cs
 [Fact]
public void Fact_PutUser_NoValidationNoRepository()
{
    // EXAMPLE
    var user = new User(1, "LUCIANO SOUSA", 30, false);

    // VALIDATION
    var val = new PutUserValidator().Validate(user);
    Assert.True(val.IsValid);

    if (val.IsValid)
    {
        // REPOSITORY
        new UserRepository(ctx).Put(user);

        // ASSERT
        var _user = ctx.User.Find(user.Id);
        Assert.Equal(JsonConvert.SerializeObject(user), JsonConvert.SerializeObject(_user));
    }
}
```

We have accelerated the steps a bit by adding validation and a repository method.

### Green Step

Create the "PutUserValidator" class by implementing the "PostUserValidator" inside the "Validations" folder:

```cs
using FluentValidation;

namespace CRUD_NETCore_TDD.Infra.Validations
{
    public class PutUserValidator: PostUserValidator
    {
        public PutUserValidator()
        {
            RuleFor(x => x.Id)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThan(0)
                .WithErrorCode("103");
        }
    }
}

```

Only the user id will be validated because the others will be responsibility of the super class.  
Inside the "UserRepository" class, create the "Put" method with the content we have abstracted:

```cs
public void Put(User user)
{
    ctx.User.Update(user);
    ctx.SaveChanges();
}
```

If you test this **Fact**, it will happen this error:  
_"System.InvalidOperationException : The instance of entity type 'User' cannot be tracked because another instance with the same key value for {'Id'} is already being tracked. When attaching existing entities, ensure that only one entity instance with a given key value is attached. Consider using 'DbContextOptionsBuilder.EnableSensitiveDataLogging' to see the conflicting key values."_

Basically, you will have to detach the user model inserted before the update. Inside the "UserRepository" class, modify the "Post" method:

```cs
public User Post(User user)
{
    ctx.User.Add(user);
    ctx.SaveChanges();
    ctx.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
    return user;
}
```

Try it again and the update will be successful.
Copy the method, comment the original and rename the copy to "Fact_PutUser".

## Put User • Theory

Due to these changes, our **Theory** has become very straight forward:

### Green Step • Id

```cs
[Theory]
[InlineData(0, 103)]
public void Theory_PutUser_Id(int Id, int ErrorCode)
{
    var user = new User
    {
        Id = Id
    };

    CheckError(new PutUserValidator(), ErrorCode, user);
}
```

## Get User

Create a file named "GetUserTest.cs" inside the folder "Tests" with the code below:

```cs
using Xunit;

namespace CRUD_NETCore_TDD.Test.Tests
{
    public class GetUserTest: BaseTest
    {
        public GetUserTest()
        {
            new PostUserTest(ctx).Fact_PostUser();
        }
        #region THEORY
        #endregion
        #region FACT
        [Fact]
        public void Fact_GetUser()
        {

        }
        #endregion
    }
}

```

In order to test the GET method, a POST is required inside the constructor, but the "ctx" object must be passed as parameter to "PostUserTest"'s constructor, so modify the class:

```cs
public PostUserTest(MyContext ctx = null) : base(ctx)
{
}
```

## Get User • Fact

### Green Step

Apply filters for each User's attribute to return a list with only one result:

```cs
[Fact]
public void Fact_GetUser()
{
    var user = new User(1, "LUCIANO PEREIRA", 33, true);

    var users = ctx.User
                    .Where(w =>
                        w.Id == user.Id
                        &&
                        w.Name.Equals(user.Name)
                        &&
                        w.Age == user.Age
                        &&
                        w.IsActive == user.IsActive
                    )
                    .ToList();
    // ASSERT
    Assert.Equal(JsonConvert.SerializeObject(user), JsonConvert.SerializeObject(users.First()));
}
```

Test the method to check the result. It should be green.

## Get User • Theory

There is no validation for GET method but we will test positive results for each attribute anyway.

### Green Step • Id

```cs
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

```

Pay attention to this part of the code:

```
(user.Id == 0 || w.Id == user.Id)
```

The left side of the "OR" operator contains what I call "logic door". It's a great technique to dismiss a filter if the value is not informed. In this case, if "user.Id" be 0 by default, it won't filter the query. The same goes for the other attributes.

### Green Step • Name

```cs
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

```

### Green Step • Age

```cs
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
```

### Green Step • IsActive

```cs
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
```

Let's use the "logic door" applied to the filters inside the **Fact** as well:

## Get User • Fact II

### Refactor Step II

Copy the method, comment the original and modify the copy:

```cs
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
```

Test the "GetUserTest" class and everything should work.

## Delete User

Create a file named "DeleteUserTest.cs" inside the folder "Tests" with the code below:

```cs
using Xunit;

namespace CRUD_NETCore_TDD.Test.Tests
{
    public class DeleteUserTest: BaseTest
    {
        public DeleteUserTest()
        {
            new PostUserTest().Fact_PostUser();
        }
        #region THEORY
        #endregion
        #region FACT
        [Fact]
        public void Fact_DeleteUser ()
        {

        }
        #endregion
    }
}
```

In order to test the GET method, a POST is required inside the constructor.

## Delete User • Fact

Inside the **Fact** insert this code:

```cs
[Fact]
public void Fact_DeleteUser ()
{
    var user = new User
    {
        Id = 1
    };

    ctx.User.Remove(user);
    ctx.SaveChanges();

    // ASSERT
    var users = ctx.User.ToList();
    Assert.True(users.Count == 0);
}
```

Let's provide validation and repository access to the **Fact**, but first: copy the method, comment the original and rename the copy to "Fact_DeleteUser_ValidationNoRepository".

### Red Step

```cs
[Fact]
public void Fact_DeleteUser_NoValidationNoRepository()
{
    var user = new User
    {
        Id = 1
    };

    var val = new DeleteUserValidator().Validate(user);
    Assert.True(val.IsValid);

    if (val.IsValid)
    {
        new UserRepository(ctx).Delete(user);

        // ASSERT
        var users = ctx.User.ToList();
        Assert.True(users.Count == 0);
    }
}
```

### Green Step

Create the "DeleteUserValidator" class inside the "Validations" folder:

```cs
using CRUD_NETCore_TDD.Infra.Models;
using FluentValidation;

namespace CRUD_NETCore_TDD.Infra.Validations
{
    public class DeleteUserValidator : AbstractValidator<User>
    {
        public DeleteUserValidator()
        {
            RuleFor(x => x.Id)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThan(0)
                .WithErrorCode("103");
        }
    }
}

```

Create the "Delete" method inside the "UserRepository" class:

```cs
public void Delete(User user)
{
    ctx.User.Remove(user);
    ctx.SaveChanges();
}
```

Test the method to see if everything is alright.

## Delete User • Theory
