using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Serilog;
using Microsoft.Extensions.HealthChecks;
using Microsoft.OpenApi.Models;
using Pitstop.Infrastructure.Messaging.Configuration;
using Playground.Nl.OrderManagementAPI.Nl.Context;
using Playground.Nl.OrderManagementAPI.Nl.Extensions;
using Playground.Nl.OrderManagementAPI.Nl.Helpers;

namespace Playground.Nl.OrderManagementAPI.Nl
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCustomServices();
            
            // add DBContext
            var sqlConnectionString = _configuration.GetConnectionString("OrderManagementCN");
            services.AddDbContext<OrderManagementDbContext>(options => options.UseSqlServer(sqlConnectionString));
            
            // add messagepublisher
            services.UseRabbitMQMessagePublisher(_configuration);
            
            // add messagehandler
            services.UseRabbitMQMessageHandler(_configuration);
            
            services.AddScoped<IUserPrincipalAccessor, UserPrincipalAccessor>();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Add framework services.
            services
                .AddMvc(options => options.EnableEndpointRouting = false)
                .AddNewtonsoftJson();

            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "OrderManagement API", Version = "v1" });
            });
            
            services.AddHealthChecks(checks =>
            {
                checks.WithDefaultCacheDuration(TimeSpan.FromSeconds(1));
                checks.AddSqlCheck("OrderManagementCN", _configuration.GetConnectionString("OrderManagementCN"));
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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "OrderManagement API - v1");
            });

            // auto migrate db
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                scope.ServiceProvider.GetService<OrderManagementDbContext>().MigrateDb();
            }
        }
    }
}