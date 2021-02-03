using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using WebStore.DAL.Context;
using WebStore.Infrastructure.Interfaces;
using WebStore.Infrastructure.Services;

namespace WebStore
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
     
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<WebStoreDb>(opt => opt.UseSqlServer(_configuration.GetConnectionString("Default")));

            services.AddTransient<IEmployeesData, InMemoryEmployeesData>();

            services.AddTransient<IProductData, InMemoryProductData>();

            services.AddControllersWithViews()
                .AddRazorRuntimeCompilation();
        }
       
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();                
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseWelcomePage("/welcome");


            app.UseEndpoints(endpoints =>
            {
                // Русский комментарий
                // Русский комментарий
                endpoints.MapControllerRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{Id?}");
            });
        }
    }
}
