using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebStore.Clients.Clients;
using WebStore.Clients.Values;
using WebStore.DAL.Context;
using WebStore.Domain.Entities.Identity;
using WebStore.Interfaces.Services;
using WebStore.Interfaces.TestAPI;
using WebStore.Services.Data;
using WebStore.Services.InCookies;

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
            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<WebStoreDb>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(opt =>
            {
#if DEBUG
                opt.Password.RequiredLength = 3;
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredUniqueChars = 3;
#endif
                opt.User.RequireUniqueEmail = false;
                opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

                opt.Lockout.AllowedForNewUsers = false;
                opt.Lockout.MaxFailedAccessAttempts = 10;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
            });

            services.ConfigureApplicationCookie(opt =>
            {
                opt.Cookie.Name = "WebStore.GB";
                opt.Cookie.HttpOnly = true;
                opt.ExpireTimeSpan = TimeSpan.FromDays(10);

                opt.LoginPath = "/Account/Login";
                opt.LogoutPath = "/Account/Logout";
                opt.AccessDeniedPath = "/Account/AccessDenied";

                opt.SlidingExpiration = true;

            });

            var connStr = _configuration["ConnectionString"];

            switch (connStr)
            {
                case "SqlServer":
                    services.AddDbContext<WebStoreDb>(opt =>
                        opt.UseSqlServer(_configuration.GetConnectionString(connStr))
                        .UseLazyLoadingProxies());
                    break;

                case "Sqlite":
                    services.AddDbContext<WebStoreDb>(opt =>
                        opt.UseSqlite(_configuration.GetConnectionString(connStr), o => o.MigrationsAssembly("WebStore.DAL.Sqlite")));
                    break;

                default:
                    throw new Exception($"Неизвестная строка подключения: {connStr}");
            }

            services.AddTransient<WebStoreDbInitializer>();

            services.AddTransient<IEmployeesData, EmloyeesClient>();

            services.AddTransient<IProductData, ProductsClient>();

            services.AddTransient<ICartService, InCookiesCartService>();

            services.AddTransient<IOrderService, OrdersClient>();

            services.AddTransient<IValuesService, ValuesClient>();

            services.AddControllersWithViews()
                .AddRazorRuntimeCompilation();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, WebStoreDbInitializer db)
        {
            db.Initialize();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseWelcomePage("/welcome");


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                    );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{Id?}");
            });
        }
    }
}
