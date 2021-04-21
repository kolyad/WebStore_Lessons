using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using WebStore.DAL.Context;
using WebStore.Services.Data;

namespace WebStore.ServiceHosting
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IConfiguration _configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connStr = _configuration["ConnectionString"];

            switch (connStr)
            {
                case "SqlServer":
                    services.AddDbContext<WebStoreDb>(opt =>
                        opt.UseSqlServer(_configuration.GetConnectionString(connStr)));
                        //.UseLazyLoadingProxies());
                    break;

                case "Sqlite":
                    services.AddDbContext<WebStoreDb>(opt =>
                        opt.UseSqlite(_configuration.GetConnectionString(connStr), o => o.MigrationsAssembly("WebStore.DAL.Sqlite")));
                    break;

                default:
                    throw new Exception($"Неизвестная строка подключения: {connStr}");
            }
            services.AddTransient<WebStoreDbInitializer>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebStore.ServiceHosting", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebStore.ServiceHosting v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
