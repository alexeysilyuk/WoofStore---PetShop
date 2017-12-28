using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using mvc_project.Models;

namespace mvc_project.DAL
{
    public class ShopItemDAL : DbContext
    {

        public DbSet<ShopItem> ShopItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ShopItem>().ToTable("ShopItem");
        }

    }
}