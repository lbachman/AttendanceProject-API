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
- https://www.youtube.com/watch?v=9lRva0L12wI

## Building the Context and Models

- Open a package manager console, then navigate to the directory that the project resides in. Then run the following command.
- Create Models and Context file: `Scaffold-DbContext "Server=127.0.0.1;Port=3306;User=root;Database=attendance;Pwd=Basscrunch@808;" MySql.EntityFrameworkCore -OutputDir Models -f`
- *ensure that all the reqirements are installed and the connection string is properly configured or this command will fail*
- *ensure that the project compiles successfully first or the build will fail*

## Commands
- `Get-Package` list out installed packages in the package manager console.
- `Add-Migration {add migration name here}` 
- `Update-Database` applys migrations to instalize a database
- `Remove-Migration -context {context name}` removes migration
- `dotnet tool install --global ef` installs entity framework command line package. 


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

## Creating Controllers
1. Right-click controller folder
2. Click new scaffold item
3. Click MVC controller with views using Entity Framework or API with read/write endpoints, using Entity Framework
4. Pick a model class 

## Setting Up Identity Framework (Method One)
- Official Docs: https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-8.0&tabs=visual-studio
- One way to do it is when creating project, select authenticate by individual user accounts. This option is unavalible if using the "Web API" option
### Scaffold Identity Thing
    1. right click project file and click Add Scaffolded Item
    2. select identity
    3. select features you want to add. 
    4. Create a new context class. 
    5. create new user class

- next add the properties you want in the user model in the data folder
- update the context file to configure these properties.

- after this run `Add-Migration {desired name}`
- this should auto generate the migration file

## Method Two
1. add dependencies
    - Microsoft.AspNetCore.Identity.EntityFrameworkCore
    - Microsoft.EntityFrameworkCore
    - Microsoft.EntityFrameworkCore.Design
    - Microsift.EntityFrameworkCore.SqlServer
    - Microsoft.EntityFrameworkCore.Tools

2. create data folder
    - add ApplicationDbContext class
                   
    - add ApplicationUser class
        - extend identity user object `public class ApplicationUser : IdentityUser`
        - add `using Microsoft.AspNetCore.Identity;`

3. run this when switching machines to update the database `Update-Database -Context ApplicationDbContext`

## Testing the authentication features
- after adding the swagger oauth stuff, login to an account copy and paste the "access token" in the response body, and paste this into the lock thing typing "Bearer" first. 

## Cookies
- under the login request set "useCookies" to true
- open devtools and navigate to 