using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.HealthChecks;
using Microsoft.OpenApi.Models;
using Pitstop.Infrastructure.Messaging.Configuration;
using Playground.Nl.CustomerManagementAPI.Database.Context;
using Playground.Nl.CustomerManagementAPI.Database.Models;
using Playground.Nl.CustomerManagementAPI.Nl.Helpers;
using Playground.Nl.CustomerManagementAPI.Services.Extensions;
using Playground.Nl.CustomerManagementAPI.Services.Helpers;
using Serilog;

namespace Playground.Nl.CustomerManagementAPI.Nl
{
    public class Startup
    {
        private IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // add DBContext
            var sqlConnectionString = _configuration.GetConnectionString("CustomerManagementCN");
            services.AddDbContext<CustomerManagementDBContext>(options => 
                options.UseSqlServer(
                    sqlConnectionString, 
                    o => o.MigrationsAssembly("Playground.Nl.CustomerManagementAPI.Database")));
            
            services.AddIdentity<Customer, IdentityRole>()
                .AddEntityFrameworkStores<CustomerManagementDBContext>()
                .AddDefaultTokenProviders();

            // add messagepublisher
            services.UseRabbitMQMessagePublisher(_configuration);
            
            services.AddScoped<IUserPrincipalAccessor, UserPrincipalAccessor>();

            // Add framework services
            services
                .AddMvc(options => options.EnableEndpointRouting = false)
                .AddNewtonsoftJson();

            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CustomerManagement API", Version = "v1" });
            });
            
            services.AddCustomServices();
            
            services.AddHealthChecks(checks =>
            {
                checks.WithDefaultCacheDuration(TimeSpan.FromSeconds(1));
                checks.AddSqlCheck("CustomerManagementCN", _configuration.GetConnectionString("CustomerManagementCN"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(_configuration)
                .Enrich.WithMachineName()
                .CreateLogger();

            app.UseMvc();
            app.UseDefaultFiles();
            app.UseStaticFiles();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CustomerManagement API - v1");
            });

            // auto migrate db
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                scope.ServiceProvider.GetService<CustomerManagementDBContext>().MigrateDb();
            }
        }
    }
}