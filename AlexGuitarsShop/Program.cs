using AlexGuitarsShop.DAL.Repositories;
using AlexGuitarsShop.DAL.Interfaces;
using AlexGuitarsShop.Domain.Models;
using AlexGuitarsShop.Service.Interfaces;
using AlexGuitarsShop.Service.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = new PathString("/Account/Login");
        options.AccessDeniedPath = new PathString("/Account/Login");
    });

builder.Services.AddTransient<IRepository<Guitar>, GuitarRepository>(provider => 
    new GuitarRepository(connectionString));
builder.Services.AddTransient<IUserRepository, UserRepository>(provider => 
    new UserRepository(connectionString));

builder.Services.AddMemoryCache();
builder.Services.AddSession();
builder.Services.AddScoped(sp => new Cart(sp));

builder.Services.AddTransient<ICartItemRepository, CartItemRepository>(provider => 
    new CartItemRepository(connectionString, provider.GetService<Cart>()));

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddTransient<IGuitarService, GuitarService>();
builder.Services.AddTransient<IAccountService, AccountService>();
builder.Services.AddTransient<ICartItemService, CartItemService>();

builder.Services.AddMvc();

var app = builder.Build();

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
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();