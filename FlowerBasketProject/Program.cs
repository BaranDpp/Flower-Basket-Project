using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using System;
using FlowerBasketProject.Models.Entity;
using FlowerBasketProject.Models.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";  // Eriþim engellendiðinde yönlendirme yapýlacak sayfa
});

var app = builder.Build();

// Seed the database.
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<ApplicationDbContext>();
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

    try
    {
        // Apply any pending migrations and create the database if it doesn't exist.

        // Seed the database with initial data.

        dbContext.Database.EnsureDeleted();
        dbContext.Database.EnsureCreated();
        await ApplicationDbContextSeed.SeedAsync(dbContext, userManager, roleManager);
    }
    catch (Exception ex)
    {
        // Log any errors that occur during migration or seeding.
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred seeding the database.");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Default}/{action=Index}/{id?}");
app.MapControllerRoute(
       name: "orders",
       pattern: "orders",
       defaults: new { controller = "Order", action = "Index" });

app.Run();