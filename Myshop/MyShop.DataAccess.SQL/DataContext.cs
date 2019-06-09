using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.SQL
{
    public class DataContext : DbContext
    {
        public DataContext(): base("DefaultConnection")
        {
            
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCatagory> ProductCategories { get; set; }
        public DbSet<BasketItem> Basketitems { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<Customer> Customers { get; set; }



    }
}
