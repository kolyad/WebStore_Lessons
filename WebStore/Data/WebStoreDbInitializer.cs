using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using WebStore.DAL.Context;

namespace WebStore.Data
{
    public class WebStoreDbInitializer
    {
        private readonly WebStoreDb _db;
        private readonly ILogger<WebStoreDbInitializer> _logger;

        public WebStoreDbInitializer(
            WebStoreDb db,
            ILogger<WebStoreDbInitializer> logger)
        {
            _db = db;
            _logger = logger;
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
            _logger.LogInformation("Начало инициализации товаров");


            if (!_db.Sections.Any())
            {
                _logger.LogInformation("Добавление секций");
                using (_db.Database.BeginTransaction())
                {
                    _db.Sections.AddRange(TestData.Sections);

                    _db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Sections] ON");
                    _db.SaveChanges();
                    _db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Sections] OFF");

                    _db.Database.CommitTransaction();
                }
                _logger.LogInformation("Cекции успешно добавлены в БД");
            }

            if (!_db.Brands.Any())
            {
                _logger.LogInformation("Добавление брендов");
                using (_db.Database.BeginTransaction())
                {
                    _db.Brands.AddRange(TestData.Brands);

                    _db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Brands] ON");
                    _db.SaveChanges();
                    _db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Brands] OFF");

                    _db.Database.CommitTransaction();
                }
                _logger.LogInformation("Бренды успешно добавлены в БД");
            }

            _logger.LogInformation("Добавление товаров");
            using (_db.Database.BeginTransaction())
            {
                _db.Products.AddRange(TestData.Products);

                _db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Products] ON");
                _db.SaveChanges();
                _db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Products] OFF");

                _db.Database.CommitTransaction();
            }
            _logger.LogInformation("Товары успешно добавлены в БД");

            _logger.LogInformation("Инициализация товаров выполнена успешно");
        }
    }
}
