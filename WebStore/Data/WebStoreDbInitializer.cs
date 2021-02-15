using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DAL.Context;
using WebStore.Domain.Entities.Identity;

namespace WebStore.Data
{
    public class WebStoreDbInitializer
    {
        private readonly WebStoreDb _db;
        private readonly ILogger<WebStoreDbInitializer> _logger;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public WebStoreDbInitializer(
            WebStoreDb db,
            ILogger<WebStoreDbInitializer> logger,
            UserManager<User> userManager,
            RoleManager<Role> roleManager
            )
        {
            _db = db;
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Initialize()
        {
            _logger.LogInformation("Инициализация БД");
            //_db.Database.EnsureDeleted();
            //_db.Database.EnsureCreated();

            var db = _db.Database;

            if (db.GetPendingMigrations().Any())
            {
                _logger.LogInformation("Выполнение миграций...");
                db.Migrate();
                _logger.LogInformation("Миграции выполнены успешно");
            }
            else
            {
                _logger.LogInformation("БД находится в актуальном состоянии");
            }

            try
            {
                InitializeProducts();
                InitializeIdentityAsync().Wait();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при выполнении инициализации БД");
            }

            _logger.LogInformation("Инициализация БД выполнена успешно");
        }

        private void InitializeProducts()
        {
            if (_db.Products.Any())
            {
                _logger.LogInformation("Инициализации базы товарами не требуется");
                return;
            }            

            var products_sections = TestData.Sections.Join(
                TestData.Products,
                section => section.Id,
                product => product.SectionId,
                (section, product) => (section, product));

            foreach (var (section, product) in products_sections)
            {
                section.Products.Add(product);
            }

            var products_brands = TestData.Brands.Join(
                TestData.Products,
                brand => brand.Id,
                product => product.BrandId,
                (brand, product) => (brand, product));

            foreach (var (brand, product) in products_brands)
            {
                brand.Products.Add(product);
            }

            var sections_sections = TestData.Sections.Join(
                TestData.Sections,
                parent => parent.Id,
                child => child.ParentId,
                (parent, child) => (parent, child));

            foreach (var (parent, child) in sections_sections)
            {
                child.Parent = parent;
            }


            foreach (var x in TestData.Products)            
            {
                x.Id = 0;
                x.SectionId = 0;
                x.BrandId = null;
            }

            foreach (var x in TestData.Sections)            
            {
                x.Id = 0;
                x.ParentId = null;                
            }
            
            foreach (var x in TestData.Brands)            
            {
                x.Id = 0;                
            }


            _logger.LogInformation("Начало инициализации товаров");

            using (_db.Database.BeginTransaction())
            {
                _db.Products.AddRange(TestData.Products);
                _db.Sections.AddRange(TestData.Sections);
                _db.Brands.AddRange(TestData.Brands);

                _db.SaveChanges();
                _db.Database.CommitTransaction();
            }

            _logger.LogInformation("Товары успешно добавлены в БД");

            _logger.LogInformation("Инициализация товаров выполнена успешно");
        }

        private async Task InitializeIdentityAsync()
        {
            _logger.LogInformation("Инициализация системы Identity");

            async Task CheckRole(string roleName)
            {
                if (!await _roleManager.RoleExistsAsync(roleName))
                {
                    _logger.LogInformation("Роль {roleName} отсутствует. Создаю ...", roleName);
                    await _roleManager.CreateAsync(new Role { Name = roleName });
                    _logger.LogInformation("Роль {roleName} создана успешно", roleName);
                }
            }

            await CheckRole(Role.Administrator);
            await CheckRole(Role.User);

            if (await _userManager.FindByNameAsync(User.Administrator) is null)
            {
                _logger.LogInformation("Отсутствует учётная запись администратора. Создаю ...");
                var admin = new User
                {
                    UserName = User.Administrator
                };

                var creation_result = await _userManager.CreateAsync(admin, User.DefaultAdministratorPassword);
                if (creation_result.Succeeded)
                {
                    _logger.LogInformation("Учётная запись администратора создана");
                    await _userManager.AddToRoleAsync(admin, Role.Administrator);
                    _logger.LogInformation("Учётная запись администратора наделена ролью {roleName}", Role.Administrator);
                }
                else
                {
                    var errors = creation_result.Errors.Select(x => x.Description);
                    throw new InvalidOperationException($"Ошибка при созданиии учётной записи администратора: {string.Join(",", errors)}");
                }
            }

            _logger.LogInformation("Инициализация системы Identity выполнена успешно");
        }

    }
}
