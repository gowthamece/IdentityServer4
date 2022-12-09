using IdentityServer4;
using IdentityServer4.NCache.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerCore3
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var builder = services.AddIdentityServer()
                .AddTestUsers(IdentityConfiguration.TestUsers)
                .AddNCacheConfigurationStore(options =>
                {
                    options.CacheId = _configuration["CacheId"];

                    var serverList = _configuration["Servers"].Split(',')
                                            .Select(x => x.Trim())
                                            .ToList()
                                            .Select(y =>
                                                new NCacheServerInfo(y, 9800))
                                            .ToList();
                    options.ConnectionOptions = new NCacheConnectionOptions
                    {
                        ServerList = serverList,
                        EnableClientLogs = true,
                        LogLevel = NCacheLogLevel.Debug
                    };
                })
                .AddNCachePersistedGrantStore(options =>
                {
                    options.CacheId = _configuration["CacheId"];

                    var serverList = _configuration["Servers"].Split(',')
                                            .Select(x => x.Trim())
                                            .ToList()
                                            .Select(y =>
                                                new NCacheServerInfo(y, 9800))
                                            .ToList();
                    options.ConnectionOptions = new NCacheConnectionOptions
                    {
                        ServerList = serverList,
                        EnableClientLogs = true,
                        LogLevel = NCacheLogLevel.Debug
                    };
                })
                .AddNCacheDeviceCodeStore(options =>
                {
                    options.CacheId = _configuration["CacheId"];

                    var serverList = _configuration["Servers"].Split(',')
                                            .Select(x => x.Trim())
                                            .ToList()
                                            .Select(y =>
                                                new NCacheServerInfo(y, 9800))
                                            .ToList();
                    options.ConnectionOptions = new NCacheConnectionOptions
                    {
                        ServerList = serverList,
                        EnableClientLogs = true,
                        LogLevel = NCacheLogLevel.Debug
                    };
                });


            builder.AddDeveloperSigningCredential();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
