using DataLayer.Data;
using DataLayer.Models;
using DataLayer.Repositories.Interfaces;
using DataLayer.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Services;
using ServiceLayer.Services.Interfaces;
using DataLayer.DbInitializer;
using DataLayer.Repositories.GenericRepository.Interfaces;
using DataLayer.Models.Request;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using DataLayer.Repositories.GenericRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//DatabaseString
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//Database
builder.Services.AddDbContext<AppDbContext>(

        options => options.UseSqlServer(connectionString)
    );

//Identity
builder.Services.AddIdentity<Employee, IdentityRole<int>>()
    .AddEntityFrameworkStores<AppDbContext>();

//Cookie
builder.Services.ConfigureApplicationCookie(config =>
{
    config.Cookie.Name = "MyCookieAuth";
    config.LoginPath = "/login";
    config.ExpireTimeSpan = TimeSpan.FromSeconds(20);
    config.Cookie.MaxAge = TimeSpan.FromSeconds(20);
    config.SlidingExpiration = true;
});

//DI
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped<IPositionRepository, PositionRepository>();
builder.Services.AddScoped<IUserRequestRepository, UserRequestRepository>();
builder.Services.AddScoped<IAccountService, AccountService>();

var app = builder.Build();

//Seed data
await AppDbInitializer.Seed(app);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
