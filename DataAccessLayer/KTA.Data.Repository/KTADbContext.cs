using KTA.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTA.Data.Repository
{
    public class KTADbContext : DbContext
    {
        public KTADbContext(DbContextOptions<KTADbContext> options)
        : base(options)
        { }

        public DbSet<AccountEntity> Account { get; set; }
        public DbSet<ProductEntity> Product { get; set; }
        public DbSet<SalesOrderEntity> SalesOrder { get; set; }
        public DbSet<CustomerEntity> Customer { get; set; }


        // Fluent API: 建立MSSQL對應主表 PK & FK 
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<AccountEntity>().HasKey(k => k.Account);
        //    modelBuilder.Entity<CustomerEntity>().HasKey(k => k.CustId);
        //    modelBuilder.Entity<ProductEntity>().HasKey(k => k.Pn);
        //    modelBuilder.Entity<SalesOrderEntity>(s =>
        //    {
        //        s.HasKey(k => k.SO);
        //    });
        //}
    }
}
