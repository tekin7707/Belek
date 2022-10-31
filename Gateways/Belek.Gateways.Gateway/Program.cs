using Belek.Gateways.Gateway;
using Belek.Gateways.Gateway.Services;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Values;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddAuthentication().AddJwtBearer("GatewayAuthenticationScheme", options =>
//{
//    options.Authority = builder.Configuration["IdentityServerURL"];
//    options.Audience = "resource_gateway";
//    options.RequireHttpsMetadata = false;
//});
builder.Services.AddHttpClient<IClientCredentialTokenService, ClientCredentialTokenService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAccessTokenManagement();

builder.Services.AddHttpClient();
builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddOcelot().AddDelegatingHandler<RequestInspector>(); 
builder.Configuration.AddJsonFile($"Configuration.development.json");

builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
});

var app = builder.Build();


app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
    );

app.UseOcelot().Wait();
app.UseAuthentication();
app.UseAuthorization();


app.Run();