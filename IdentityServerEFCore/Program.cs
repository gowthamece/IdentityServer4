using IdentityServer4.Configuration;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServerEFCore;
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

if(app.Environment.IsDevelopment())
{
    InitializeDatabase(app);
}
app.UseRouting();
app.UseIdentityServer();
app.Run();

static void InitializeDatabase(IApplicationBuilder app)
{
    using (var serviceScope =
            app.ApplicationServices
                .GetService<IServiceScopeFactory>().CreateScope())
    {
        serviceScope
            .ServiceProvider
                .GetRequiredService<PersistedGrantDbContext>()
                .Database.Migrate();

        var context =
            serviceScope.ServiceProvider
                .GetRequiredService<ConfigurationDbContext>();

        context.Database.Migrate();
        if (!context.Clients.Any())
        {
            foreach (var client in Config.Clients)
            {
                context.Clients.Add(client.ToEntity());
            }
            context.SaveChanges();
        }

        if (!context.IdentityResources.Any())
        {
            foreach (var resource in Config.Ids)
            {
                context.IdentityResources.Add(resource.ToEntity());
            }
            context.SaveChanges();
        }

        if (!context.ApiResources.Any())
        {
            foreach (var resource in Config.Apis)
            {
                context.ApiResources.Add(resource.ToEntity());
            }
            context.SaveChanges();
        }
    }
}
