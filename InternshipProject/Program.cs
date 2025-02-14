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
using System.Configuration;
using Azure.Storage.Blobs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//DatabaseString
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//Database
builder.Services.AddDbContext<AppDbContext>(
        options => options.UseSqlServer(connectionString)
    );

//Cloud
builder.Services.AddSingleton(x => new BlobServiceClient(builder.Configuration.GetConnectionString("AzureBlobStorage")));

//Identity
builder.Services.AddIdentity<Employee, IdentityRole<int>>()
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6; // Minimum length of the password
    options.Password.RequiredUniqueChars = 0; // Number of unique characters required in the password
});

//Cookie
builder.Services.ConfigureApplicationCookie(config =>
{
    config.Cookie.Name = "MyCookieAuth";
    config.LoginPath = "/login";
    config.ExpireTimeSpan = TimeSpan.FromMinutes(10);
    config.Cookie.MaxAge = TimeSpan.FromMinutes(10);
    config.SlidingExpiration = true;
});

//DI
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped<IUserPositionRepository, UserPositionRepository>();
builder.Services.AddScoped<IUserRequestRepository, UserRequestRepository>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IUserPositionService, UserPositionService>();
builder.Services.AddScoped<IUserRequestService, UserRequestService>();
builder.Services.AddScoped<IContractRepository, ContractRepository>();
builder.Services.AddScoped<IContractService, ContractService>();
builder.Services.AddSingleton<IBlobService, BlobService>();

//HttpClient
builder.Services.AddHttpClient();

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
            name: "areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
          );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
