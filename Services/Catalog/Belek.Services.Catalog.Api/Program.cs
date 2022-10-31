using Belek.Services.Catalog.Domain.Enums;
using Belek.Services.Catalog.Domain.Models;
using Belek.Services.Catalog.App.Services;
using FreeCourse.Services.Order.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//var requireAuthorizePolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["IdentityServerURL"];
    options.Audience = "resource_catalog";
    options.RequireHttpsMetadata = false;
});

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICatalogService, CatalogService>();

builder.Services.AddControllers(opt =>
{
    opt.Filters.Add(new AuthorizeFilter());
});
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddDbContext<CatalogDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), configure =>
    {
        configure.MigrationsAssembly("Belek.Services.Catalog.Db");
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

#region migration & mock

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;

    var applicationDbContext = serviceProvider.GetRequiredService<CatalogDbContext>();

    applicationDbContext.Database.Migrate();

    var services = scope.ServiceProvider;
    var context = new CatalogDbContext(services.GetRequiredService<DbContextOptions<CatalogDbContext>>());

    if (!context.Categories.Any())
    {
        context.Categories.AddRange(
            new Category
            {
                Name = "Category 11",
                CreatedDate = DateTime.Now,
                Status = StatusEnum.Active
            },
            new Category
            {
                Name = "Category 21",
                CreatedDate = DateTime.Now,
                Status = StatusEnum.Active
            });
        context.SaveChanges();
    }

    if (!context.Catalogs.Any())
    {
        var cat1 = context.Categories.First();
        context.Catalogs.AddRange(
            new Catalog
            {
                Name = "Test 11",
                CategoryId = cat1.Id,
                Price = 0,
                Status = StatusEnum.Active,
                Description = "",
                UserId = "",
                CreatedDate = DateTime.Now
            },
            new Catalog
            {
                Name = "Test 21",
                CategoryId = cat1.Id,
                Price = 0,
                Status = StatusEnum.Active,
                Description = "",
                UserId = "",
                CreatedDate = DateTime.Now
            },
            new Catalog
            {
                Name = "Test 31",
                CategoryId = cat1.Id,
                Price = 0,
                Status = StatusEnum.Active,
                Description = "",
                UserId = "",
                CreatedDate = DateTime.Now
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
