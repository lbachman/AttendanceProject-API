using AttendanceAPI_v3.AttendanceModels;
using AttendanceAPI_v3.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AttendanceAPI_v3
{
    /// <summary>
    /// main program class. represesents the app. 
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Registers DbContext for  authorization database
            var connectionString = builder.Configuration.GetConnectionString("AuthorizationConnection") ?? throw new InvalidOperationException("Connection string 'AuthorizationConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

            // Registers DbContext for AttendanceContext
            var attendanceConnectionString = builder.Configuration.GetConnectionString("AttendanceConnection") ?? throw new InvalidOperationException("Connection string 'AttendanceConnection' not found.");
            builder.Services.AddDbContext<AttendanceContext>(options =>
            {
                options.UseMySQL(attendanceConnectionString);
            });

            builder.Services.AddAuthorization();
            builder.Services.AddIdentityApiEndpoints<ApplicationUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            //app.UseDefaultFiles();
            //app.UseStaticFiles();
            app.MapIdentityApi<ApplicationUser>();

            // when you post this, it will log you out and remove the cookie. 
            app.MapPost("/logout", async (SignInManager<ApplicationUser> signInManager) =>
            {
                await signInManager.SignOutAsync();
                return Results.Ok();
            }).RequireAuthorization();

            // when this is called it returns the email of the currently logged in user. 
            app.MapGet("/pingauth", (ClaimsPrincipal user) =>
            {
                var email = user.FindFirstValue(ClaimTypes.Email); // get the user's email from the claim
                return Results.Json(new { Email = email }); ; // return the email as a plain text response
            }).RequireAuthorization();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
