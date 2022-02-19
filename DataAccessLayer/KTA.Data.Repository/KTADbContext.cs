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
    }
}
