# orm-lightning
  LIGHTNING TALK - ORMs - they haven't gone away.

ORM ->  Object Relational Mapping

[SLIDE DECK](https://trendoid.github.io/orm-lightning/)

For this lightning talk we are going to just focus on one stack... .NET Core and SQL.  

There are dozens of other stacks that have ORMs that work with SQL in Javascript, Java, Ruby, Python, etc.

There are other frameworks that can improve performance in web applications and APIs with cacheing but this is code is straight data to web.

This code doesn't even cover OData or other ways to connect systems with your data on the internet.

It's a lightning talk.

#### STEP 1:
Create a new .NET Core Solution

```
dotnet new sln --name Lightning
dotnet new webapi --name API --output API
dotnet new classlib --name Data --output Data
dotnet new classlib --name EFData --output EFData
dotnet new classlib --name DapperData --output DapperData
dotnet new mstest --name Tests --output Tests
dotnet sln add .\API\API.csproj
dotnet sln add .\Data\Data.csproj
dotnet sln add .\EFData\EFData.csproj
dotnet sln add .\DapperData\DapperData.csproj
dotnet sln add .\Tests\Tests.csproj
dotnet add .\API\API.csproj reference .\Data\Data.csproj  .\EFData\EFData.csproj  .\DapperData\DapperData.csproj
dotnet add .\Tests\Tests.csproj reference .\API\API.csproj 
dotnet restore
dotnet build Lightning.sln
```

#### STEP 2: 
Add extension libraries that we'll need to make development go faster. 
(not necessarily make our queries perform better, just code less)

```
dotnet add .\API\API.csproj package NSwag.AspNetCore
dotnet add .\Data\Data.csproj package System.Data.SqlClient 
dotnet add .\EFData\EFData.csproj package Microsoft.EntityFrameworkCore.SqlServer 
dotnet add .\EFData\EFData.csproj package Microsoft.EntityFrameworkCore.Design
dotnet add .\DapperData\DapperData.csproj package Dapper
```

This is all done for you if you use this code.  

#### STEP 3:
Write some code.

#### STEP 4: 
Create the database.
```
dotnet ef migrations add InitialCreate
dotnet ef database update
```

#### STEP 5:
Write more code, migrate, test, code, migrate...

#### STEP 6: 
Reverse engineer existing database
```
cd \EFData
```

```
dotnet ef dbcontext scaffold "Server=tcp:localhost;Database=Northwind;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models
```

#### References:

.NET Core
https://docs.microsoft.com/en-us/aspnet/core/getting-started

Swagger with NSwag
https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-nswag

.NET Core Data Access
https://blogs.msdn.microsoft.com/dotnet/2016/11/09/net-core-data-access/ 

Entity Framework
https://docs.microsoft.com/en-us/ef/core/get-started/aspnetcore/new-db

Dapper
https://github.com/StackExchange/Dapper 

#### Performance:

ASP.NET Benchmarks
https://github.com/aspnet/benchmarks

Cool Power BI visuals on Performance
https://aka.ms/aspnet/benchmarks 

Tech Empower
https://www.techempower.com/benchmarks/#section=data-r17&hw=ph&test=fortune&p=hweg3v-qt65vu-u8f9xc-5m9q&d=b&f=0-0-0-0-0-0-0-1s-74
