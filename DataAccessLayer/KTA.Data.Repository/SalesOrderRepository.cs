using KTA.Data.Entity;
using KTA.Data.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTA.Data.Repository
{
    public class SalesOrderRepository : ISalesOrderRepository
    {
        private readonly IDbContextFactory<KTADbContext> _ctx;

        public SalesOrderRepository(IDbContextFactory<KTADbContext> ctx)
        {
            _ctx = ctx;
        }

        public async Task<int> AddAsync(SalesOrderEntity item)
        {
            using (var context = _ctx.CreateDbContext())
            {
                context.Entry(item).State = EntityState.Added;

                return await context.SaveChangesAsync();
            }
        }

        public async Task<int> AddBulkAsync(List<SalesOrderEntity> items)
        {
            using (var context = _ctx.CreateDbContext())
            {
                await context.SalesOrder.AddRangeAsync(items);

                return await context.SaveChangesAsync();
            }
        }

        public async Task<int> DeleteAsync(SalesOrderEntity item)
        {
            using (var context = _ctx.CreateDbContext())
            {
                context.Entry(item).State = EntityState.Deleted;

                return await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<SalesOrderEntity>> GetAllItemsAsync()
        {
            using (var context = _ctx.CreateDbContext())
            {
                return await context.SalesOrder.ToListAsync();
            }
        }

        public async Task<SalesOrderEntity> GetSingleItemAsync(SalesOrderEntity item)
        {
            using (var context = _ctx.CreateDbContext())
            {
                return await context.SalesOrder.Where(x => x.SO.Equals(item.SO)).FirstOrDefaultAsync();
            }
        }

        public async Task<IEnumerable<SalesOrderEntity>> GetSingleItemAsync(string so)
        {
            using (var context = _ctx.CreateDbContext())
            {
                return await context.SalesOrder.Where(x => x.SO.Equals(so)).ToListAsync();
            }
        }

        public async Task<int> UpdateAsync(SalesOrderEntity item)
        {
            using (var context = _ctx.CreateDbContext())
            {
                context.Entry(item).State = EntityState.Modified;

                return await context.SaveChangesAsync();
            }
        }

        public async Task<List<string>> GetSalesOrderListAsync(string so)
        {
            using (var context = _ctx.CreateDbContext())
            {
                return await context.SalesOrder.Where(x => x.SO.StartsWith(so)).Select(x=>x.SO).Take(50).ToListAsync();
            }
        }
    }
}
