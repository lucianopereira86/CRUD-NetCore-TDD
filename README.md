# CRUD-NetCore-TDD

Building a .Net Core web API with TDD.

## Technologies

- [.Net Core 3.1.0](https://dotnet.microsoft.com/download/dotnet-core/3.1)
- [xUnit 2.4.0](https://www.nuget.org/packages/xunit/2.4.0)
- [Microsoft.EntityFrameworkCore 3.1.0](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore/3.1.0)
- [FluentValidation 8.6.0](https://www.nuget.org/packages/fluentvalidation)

## Objective

Let's build a simple CRUD .Net Core web API by following the TDD steps with xUnit and simulate the database connection with runtime memory.

## Topics

- [TDD](#tdd)
- [Project Structure](#project-structure)
- [Test Project](#test-project)

### TDD

What's TDD?  
It means _Test Driven Development_ and it consists in programming unit tests of core functionalities of a application before creating classes, projects, validations and other layers.  
During the development, there will be two types of refactoring: RED and GREEN.  
The RED refactoring needs the code goes wrong when executed, even by not compiling.
The GREEN refactoring consists of a successful compilation and exacly what the unit test was expecting as result.  
It's important to follow this rule: RED before GREEN.  
With this bottom-up approach you will have a clear understanding of all the failures and certainties that your program may have. The problem is that it requires a good amount of time to develop.

### Project Structure

Our solution will have in the end 3 layers: web API, Infra and Test.

[print01](/docs/print01.JPG)
