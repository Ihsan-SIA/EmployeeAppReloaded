using Data.Context;
using Data.Model;
using MySql.EntityFrameworkCore.Extensions;
using Application;
using Microsoft.AspNetCore.Identity;
using System.Runtime.CompilerServices;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        builder.Services.AddMySQLServer<EmployeeAppDbContext>(
            builder.Configuration.GetConnectionString("DefaultConnection")!);

        builder.Services.AddServices();
        builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequiredLength = 0;
            }
            )
            .AddEntityFrameworkStores<EmployeeAppDbContext>()
            .AddDefaultTokenProviders();

        builder.Services.ConfigureApplicationCookie(options =>
        {
            options.ExpireTimeSpan = TimeSpan.FromDays(30);
            options.SlidingExpiration = false;
            options.LoginPath = "/Account/Login";
            options.LogoutPath = "/Account/Logout";
            options.AccessDeniedPath = "/Account/AccessDenied";
        }
        );
        builder.Services.AddAuthentication();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapStaticAssets();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}")
            .WithStaticAssets();


        app.Run();
    }
}