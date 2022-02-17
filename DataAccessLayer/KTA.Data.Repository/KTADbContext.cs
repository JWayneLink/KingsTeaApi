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
    }
}
