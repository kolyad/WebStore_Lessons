using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Domain.Entities;

namespace WebStore.DAL.Context
{
    public class WebStoreDb : DbContext
    {
        public DbSet<Product> Products { get; set; }
        
        public DbSet<Brand> Brands { get; set; }

        public DbSet<Section> Sections { get; set; }

        public WebStoreDb(DbContextOptions options) : base(options) { }
    }
}
