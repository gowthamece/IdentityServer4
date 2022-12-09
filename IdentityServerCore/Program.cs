using IdentityServer4.NCache.Options;
using IdentityServerCore;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
//builder.Configuration.get;
builder.Services.AddIdentityServer()

    .AddInMemoryClients(IdentityConfiguration.Clients)
    .AddInMemoryIdentityResources(IdentityConfiguration.IdentityResources)
    .AddInMemoryApiResources(IdentityConfiguration.ApiResources)
    .AddInMemoryApiScopes(IdentityConfiguration.ApiScopes)
    .AddTestUsers(IdentityConfiguration.TestUsers)
    .AddDeveloperSigningCredential();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");



app.UseRouting();
app.UseIdentityServer();
//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapGet("/", async context =>
//    {
//        await context.Response.WriteAsync("Hello World!");
//    });
//});

app.Run();
