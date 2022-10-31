using Belek.Services.Order.App.Services;
using Belek.Services.Order.Db;
using Belek.Services.Order.Domain.Models;
using Belek.Shared.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

var requireAuthorizePolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["IdentityServerURL"];
    options.Audience = "resource_order";
    options.RequireHttpsMetadata = false;
});

builder.Services.AddDbContext<OrderDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), configure =>
    {
        configure.MigrationsAssembly("Belek.Services.Order.Db");
    });
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ISharedIdentityService, SharedIdentityService>();

builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddControllers(opt =>
{
    opt.Filters.Add(new AuthorizeFilter());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

#region migration & mock

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;

    var applicationDbContext = serviceProvider.GetRequiredService<OrderDbContext>();

    applicationDbContext.Database.Migrate();

    var services = scope.ServiceProvider;
    var context = new OrderDbContext(services.GetRequiredService<DbContextOptions<OrderDbContext>>());

    if (!context.Orders.Any())
    {
        context.Orders.AddRange(
            new OrderModel
            {
                UserId = "cf3fcc37-3e01-4171-80f8-38e4d2dbc8d1",
                CreatedDate = DateTime.Now
            },
            new OrderModel
            {
                UserId = "cf3fcc37-3e01-4171-80f8-38e4d2dbc8d1",
                CreatedDate = DateTime.Now
            });
        context.SaveChanges();
    }

    if (!context.OrderItems.Any())
    {
        var order1 = context.Orders.First();
        context.OrderItems.AddRange(
            new OrderItemModel
            {
                Name = "Test 11",
                OrderId = order1.Id,
                Price = 499,
                Quantity = 1,
                CatalogId = 5
            },
            new OrderItemModel
            {
                Name = "Test AA",
                OrderId = order1.Id,
                Price = 1499,
                Quantity = 1,
                CatalogId = 1
            });
        context.SaveChanges();
    }
}

#endregion

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // allow any origin
    .AllowCredentials()); // allow credentials


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
