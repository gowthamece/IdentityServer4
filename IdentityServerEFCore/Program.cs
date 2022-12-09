using IdentityServer4.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("localdb");

var migrationsAssembly = typeof(Program).Assembly.GetName().Name;

builder.Services.AddIdentityServer()
.AddConfigurationStore(options =>
{
    options.ConfigureDbContext = b => b.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly));
})
.AddOperationalStore(options =>
{
    options.ConfigureDbContext = b => b.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly));
    options.EnableTokenCleanup = true;
});

var app = builder.Build();

app.MapGet("/", () => "Hello World!");



app.UseRouting();
app.UseIdentityServer();
app.Run();
