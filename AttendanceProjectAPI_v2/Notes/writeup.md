# Getting Started 
- Install a Visual Studio project using the template ASP.net Web API

# Requirements 
- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.Tools
- MySql.EntityFrameworkCore 


# Links
- [Create Connection String in MySql](https://dev.mysql.com/doc/connector-net/en/connector-net-connections-string.html)
- [MySQL Connector/NET Entity Framework Core Scaffold Example](https://dev.mysql.com/doc/connector-net/en/connector-net-entityframework-core-scaffold-example.html)
-  [ASP.NET Core Identity - User Security Essentials](https://learning.oreilly.com/videos/asp-net-core-identity/10000DIVC2022123/)

# Building the Context and Models
- Create Models and Context file: `Scaffold-DbContext "Server=127.0.0.1;Port=3306;User=root;Database=attendance;Pwd=Basscrunch@808;" MySql.EntityFrameworkCore -OutputDir Models -f`

## Commands

- *Ensure that you are in the project directory when loading the package manager console*
- *If you are using a connector version earlier than Connector/NET 8.0.23, replace MySql.EntityFrameworkCore with MySql.Data.EntityFrameworkCore*
- `Get-Package` list out installed packages in the package manager console.


# Database 
Ensure that this code is in the `Program.cs` or `Startup.cs` file
```csharp
// Add services to the container.
builder.Services.AddControllers();

// Register DbContext
builder.Services.AddDbContext<AttendanceContext>(options =>
{
    options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection"));
});
```
Ensure that this code is in the `appsettings.json` folder
```json
"ConnectionStrings": {
"DefaultConnection": "Server=127.0.0.1;Port=3306;Database=attendance;Uid=root;Pwd=Basscrunch@808;"
```

## Connection strings
- `Server=127.0.0.1;Port=3306;User=root;Database=attendance;Pwd=Basscrunch@808;`
- `Server=127.0.0.1;Port=3306;User=root;Database=attendance;Pwd=110494;`

## Add Controller
1. Right-click controller folder
2. Click new scaffold item
3. Click MVC controller with views using Entity Framework or API with read/write endpoints, using Entity Framework
4. Pick a model class 
