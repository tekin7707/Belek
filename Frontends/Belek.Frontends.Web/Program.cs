using Belek.Frontends.Web.Extensions;
using Belek.Frontends.Web.Handler;
using Belek.Frontends.Web.Models;
using Belek.Shared.Services;
using FluentValidation.AspNetCore;
using Belek.Frontends.Web.Validators;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Configuration;
using Belek.Frontends.Web.Services;
using Belek.Frontends.Web.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<ClientSettings>(builder.Configuration.GetSection("ClientSettings"));
builder.Services.Configure<ServiceApiSettings>(builder.Configuration.GetSection("ServiceApiSettings"));
builder.Services.Configure<ServiceApi>(builder.Configuration.GetSection("MyCustomCost"));
builder.Services.AddHttpContextAccessor();
builder.Services.AddAccessTokenManagement();

builder.Services.AddHttpClient<IIdentityService, IdentityService>();
builder.Services.AddScoped<ISharedIdentityService, SharedIdentityService>();

var serviceApiSettings = builder.Configuration.GetSection("ServiceApiSettings").Get<ServiceApiSettings>();

builder.Services.AddHttpClient<ICategoryService, CategoryService>(opt =>
{
    opt.BaseAddress = new Uri($"{serviceApiSettings.GatewayBaseUri}/{serviceApiSettings.Catalog.Path}");
    //opt.BaseAddress = new Uri($"http://localhost:5011/api/");
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();


builder.Services.AddHttpClient<ICatalogService, CatalogService>(opt =>
{
    opt.BaseAddress = new Uri($"{serviceApiSettings.GatewayBaseUri}/{serviceApiSettings.Catalog.Path}");
    //opt.BaseAddress = new Uri($"http://localhost:5011/api/");
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();



builder.Services.AddHttpClient<IBasketService, BasketService>(opt =>
{
    opt.BaseAddress = new Uri($"{serviceApiSettings.GatewayBaseUri}/{serviceApiSettings.Basket.Path}");
    //opt.BaseAddress = new Uri($"http://localhost:5013/api/");
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

builder.Services.AddHttpClient<IOrderService, OrderService>(opt =>
{
    opt.BaseAddress = new Uri($"{serviceApiSettings.GatewayBaseUri}/{serviceApiSettings.Order.Path}");
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();


builder.Services.AddScoped<ResourceOwnerPasswordTokenHandler>();
builder.Services.AddScoped<ClientCredentialTokenHandler>();

builder.Services.AddHttpClientServices(builder.Configuration);
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, opts =>
{
    opts.LoginPath = "/Auth/SignIn";
    opts.ExpireTimeSpan = TimeSpan.FromDays(2);
    opts.SlidingExpiration = true;
    opts.Cookie.Name = "belekwebcookie";
});

builder.Services.AddControllersWithViews().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ProductCreateInputValidator>());

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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
