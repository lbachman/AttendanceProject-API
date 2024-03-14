# Creating a web API Using the Entity Framework and the "Database First Approach"

## Getting Started 
- Install a Visual Studio project using the template ASP.net Web API
- If using the identity framework to authenticate, from the authentication type dropdown menu select individual user acounts from dropdown menu. 

## Requirements 
- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.Tools
- MySql.EntityFrameworkCore 


## Useful Links
- [Create Connection String in MySql](https://dev.mysql.com/doc/connector-net/en/connector-net-connections-string.html)
- [MySQL Connector/NET Entity Framework Core Scaffold Example](https://dev.mysql.com/doc/connector-net/en/connector-net-entityframework-core-scaffold-example.html)
-  [ASP.NET Core Identity - User Security Essentials](https://learning.oreilly.com/videos/asp-net-core-identity/10000DIVC2022123/)

## Building the Context and Models
- Open a package manager console, then navigate to the directory that the project resides in. Then run the following command.
- Create Models and Context file: `Scaffold-DbContext "Server=127.0.0.1;Port=3306;User=root;Database=attendance;Pwd=Basscrunch@808;" MySql.EntityFrameworkCore -OutputDir Models -f`
- *ensure that all the reqirements are installed and the connection string is properly configured or this command will fail*

## Commands
- `Get-Package` list out installed packages in the package manager console.
- `Update-Database` applys migrations to instalize a database


## Database 
Ensure that this code is in the `Program.cs` or `Startup.cs` file. This registers the context to the database file. 
```csharp
 // Registers DbContext for  authorization database
            var connectionString = builder.Configuration.GetConnectionString("AuthorizationConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddControllersWithViews();
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

## Setting Up Identity Framework
- Make sure when creating project, select authenticate by individual user accounts
- Official Docs: https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-8.0&tabs=visual-studio
- 
