# Welcome to DAS.GoT
The software within this solution is the result of an assignment for DAS.

The current license is 'Creative Common'.

The main project is the WebApi project; where the Behaviour and Types are supporting class libraries.

The solution also contains a SqlLite database instance: WebApi.db.

## Getting Started
Before one can actual run the solution, one should check if a network connection exist.
The reason for this, is that the WebApi project does a continuous polling on 'a so called' server; by default located at www.anapioficeandfire.com.

Building the WebApi project is obviously simple; simply run 'dotnet build' within the WebApi folder. 
Note that all files 'under the bin/Debug/net8.0 folder' have been removed before creating the zip-file of the solution.
Hence, a first build of the WebApi project is mandatory.

>The steps written below are ***only necessary if one should want to alter the existing database*** instance (e.g. from current SqLite to Sql Server).

If one should choose to alter the existing database; eg. remove the existing SqlLite instance and add a SqlServer instance, one should do the following:
1. remove all three files starting with WebApi.db
2. remove the Migrations folder within the Behaviour class library
3. alter the connection string (currently only set within appsettings.Development.json)

Since the WebApi uses EF Core, one should alter the middleware registration - within Program.cs - as well.

As an example, below you can see the default registration using SqlLite.

```c#
var withConnString = builder.Configuration.GetConnectionString("DevDb");
Action<DbContextOptionsBuilder> withOptionsBuilder = ob => ob.UseSqlite(withConnString);
```

The string literal 'DevDb' references the connection string value within the app-settings.
Hence, only change this, if you have altered this within the app-settings file (see former step 3).

The options builder should be changed if one should choose to use another SQL-database provider (e.g. SQL Server).
Note that changing the SQL-database provider, means that one should load another nuget-dependency within the Behaviour project.

As an example, below you can see the default nuget registration within the Behaviour's project file

```xml
<PackageReference Include="Microsoft.EntityFrameworkCore.Sqlite" Version="8.0.11" />
```

After completing all these steps, one should be ready to actually create an initial migration, and create a new database instance.

As an example, below you can see how to do this using the dotnet CLI; the example below assumes that one is located within the solutions root directory.

```PowerShell
dotnet tool install --global dotnet-ef
dotnet ef migrations add InitialCreate --project Behaviour --startup-project WebApi
dotnet ef database update --project Behaviour --startup-project WebApi
```

## Current State
It is vital that one should know that the current code is ***NOT production ready!***

Since it is the result of an assessment, the code is written within a 'time constraint'. 
Therefor, it contains several ***to do's***. However, these to do's are ***by no means complete!***

Please also note that the WebApi uses only 'in process' messaging. 
However - using MassTransit - it is fairly easy to change the message-bus instance to RabbitMQ or Azure Service Bus.
Actually, that's the beauty of using MassTransist, it perfectly abstract the complexity of "what's under the hood".

Please also note that the current code ***lacks any unit tests***; this is unfortunately, but simply due to lack of time...

In addition to the lack of unit tests, most of the code within the consumers, filters and services have low readability and lack of testability.
For improving the readability and testability, I would have preferred to use extension methods, but again: lack of time.

However, an initial approach has been made by adding the static class HttpRequestFunctions within the Behaviour project. 
The static functions of this class are very much testable and readable. 
Hence, by extending this approach, one can also improve the overall testability and readability, say for instance of the background service.

The best way - for now however - is getting in touch with the original developer => <giovanni.scheepers@outlook.com>

## High Level Code Flow
At start-up of the WebApi project, a background service is instantiated, which will poll the 'so called server' for every 12 minutes.

The code below shows the registration of this hosted service, and it's dependency, an instance of ICoreStore.

```c#
builder.Services.AddSingleton<ICoreStore, CoreStore>();
builder.Services.AddHostedService<DataBackgroundService>();
```

The background service will contact the 'so called server', and load the retrieved characters both in the database, as well as in the ICoreStore instance.

The reason for storing characters also 'in memory' (ICoreStore), is that usually a portal will first list a paged- overview of all characters.
Because for this, one only needs the most essential properties, the ICoreStore-instance loads instances of CharacterCore.

A CharacterCore is a small subset of a Character; only it contains only the crucial properties without exposing any database-id.

After the background service is actually loaded - which takes only a 'couple of seconds' - the WebApi is ready to handle API request.

The current CharacterController handles these request. Using the current 'launch settings', one can use Swagger for this.